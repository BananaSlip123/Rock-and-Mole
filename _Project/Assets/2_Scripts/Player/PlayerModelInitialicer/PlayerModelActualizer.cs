using PlayerComponents;
using UnityEngine;

public class PlayerModelActualizer : MonoBehaviour
{
    [SerializeField] Transform t_modelTransform;
    [SerializeField] Transform t_shootTransform;
    [SerializeField] GameObject go_currentModel;

    PlayerAttackComponent playerAttackComponent;
    PlayerShootComponent playerShootComponent;
    PlayerMovementComponent playerMovementComponent;
    DamageableComponent playerDamageableComponent;

    GameObject go_currentHelmetModel = null;
    GameObject go_currentPickAxeModel = null;
    ChestClothGetter chestClothGetter = null;

    bool isAttacking;
    bool isShooting;

    public enum PickaxePosition { hand, back, throwing }

    PickaxePosition _pickaxePosition;
    public PickaxePosition PickaxePositionProperty
    {
        private get => _pickaxePosition;
        set
        {
            _pickaxePosition = value;
            UpdatePickaxePosition();
        }
    }
    private void Awake()
    {
        playerAttackComponent = GetComponent<PlayerAttackComponent>();
        playerMovementComponent = GetComponent<PlayerMovementComponent>();
        playerDamageableComponent = GetComponent<DamageableComponent>();
        playerShootComponent = GetComponent<PlayerShootComponent>();

        PickaxePositionProperty = PickaxePosition.back;
    }

    private void OnEnable()
    {
        OnEquipmentChange();

        EquipmentManager.OnCurrentChestClothChange += OnEquipmentChange;
        EquipmentManager.OnCurrentHelmetChange += OnHelmetChange;
        EquipmentManager.OnPickaxeLevelChange += OnPickAxeChange;

        playerAttackComponent.onIsAttackingChange += OnIsAttackingChange;
        playerDamageableComponent.OnDamageReceive += OnDamageReceived;
        playerShootComponent.onIsShootingChange += OnIsShootingChange;
    }
    private void OnDisable()
    {
        EquipmentManager.OnCurrentChestClothChange -= OnEquipmentChange;
        EquipmentManager.OnCurrentHelmetChange -= OnHelmetChange;
        EquipmentManager.OnPickaxeLevelChange -= OnPickAxeChange;

        playerAttackComponent.onIsAttackingChange -= OnIsAttackingChange;
        playerShootComponent.onIsShootingChange -= OnIsShootingChange;
    }
    void OnDamageReceived()
    {
        go_currentModel.GetComponent<MaterialChanger>().AssignTemporalMaterial();
        go_currentHelmetModel?.GetComponent<MaterialChanger>().AssignTemporalMaterial();
    }
    void OnIsAttackingChange(bool newValue)
    {
        isAttacking = newValue;
        OnPickAxePositionChange();
    }
    void OnIsShootingChange(bool newValue)
    {
        isShooting = newValue;
        OnPickAxePositionChange();
    }
    void OnPickAxePositionChange()
    {
        if (isShooting)
        {
            PickaxePositionProperty = PickaxePosition.throwing;
            return;
        }
            
        if (isAttacking)
            PickaxePositionProperty = PickaxePosition.hand;
        else
            PickaxePositionProperty = PickaxePosition.back;
    }

    private void OnEquipmentChange()
    {
        Destroy(go_currentModel);
        
        //go_currentModel = Instantiate(EquipmentManager.CurrentChestCloth.model, new Vector3(), new Quaternion(), t_modelTransform);
        go_currentModel = Instantiate(EquipmentManager.CurrentChestCloth.model);
        AssignParent(go_currentModel.transform, t_modelTransform);


        chestClothGetter = go_currentModel.GetComponent<ChestClothGetter>();

        playerAttackComponent.animator = chestClothGetter.playerAnimator;
        playerMovementComponent.animator = chestClothGetter.playerAnimator;

        OnPickAxeChange();
        OnHelmetChange();
    }
    private void OnHelmetChange()
    {
        if (go_currentHelmetModel != null) Destroy(go_currentHelmetModel);

        go_currentHelmetModel = Instantiate(EquipmentManager.CurrentHelmet.model);
        Transform parentToAssign = chestClothGetter.bone_Helmet;

        AssignParent(go_currentHelmetModel.transform, parentToAssign);
    }
    private void OnPickAxeChange()
    {
        if (go_currentPickAxeModel != null) Destroy(go_currentPickAxeModel);

        go_currentPickAxeModel = Instantiate(EquipmentManager.CurrentPickaxe.model);
        UpdatePickaxePosition();
    }

    void UpdatePickaxePosition()
    {
        bool hasPickaxe = go_currentPickAxeModel != null;
        if (!hasPickaxe) return;

        Transform parentToAssign;

        if (_pickaxePosition == PickaxePosition.hand)
            parentToAssign = chestClothGetter.bone_PickAxeHand;
        else if(_pickaxePosition == PickaxePosition.back)
            parentToAssign = chestClothGetter.bone_PickAxeBack;
        else
            parentToAssign = t_shootTransform;

        AssignParent(go_currentPickAxeModel.transform, parentToAssign);
    }
    void AssignParent(Transform objectTransform,  Transform parentTransform)
    {
        objectTransform.SetParent(parentTransform);
        objectTransform.localPosition = new Vector3();
        objectTransform.localEulerAngles = new Vector3();
        objectTransform.localScale = new Vector3(1, 1, 1);
    }
}
