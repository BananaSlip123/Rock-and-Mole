using PlayerComponents;
using UnityEngine;

public class PlayerModelActualizer : MonoBehaviour
{
    [SerializeField] Transform t_modelTransform;
    [SerializeField] GameObject go_currentModel;

    PlayerAttackComponent playerAttackComponent;
    PlayerMovementComponent playerMovementComponent;
    DamageableComponent playerDamageableComponent;

    GameObject go_currentHelmetModel = null;
    GameObject go_currentPickAxeModel = null;
    ChestClothGetter chestClothGetter = null;

    public enum PickaxePosition { hand, back }

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

        PickaxePositionProperty = PickaxePosition.back;
    }

    private void OnEnable()
    {
        OnEquipmentChange();

        EquipmentManager.OnCurrentChestClothChange += OnEquipmentChange;
        EquipmentManager.OnCurrentHelmetChange += OnHelmetChange;
        EquipmentManager.OnPickaxeLevelChange += OnPickAxeChange;

        playerAttackComponent.onIsAttackingChange += OnPickAxePositionChange;
        playerDamageableComponent.OnDamageReceive += OnDamageReceived;
    }
    private void OnDisable()
    {
        EquipmentManager.OnCurrentChestClothChange -= OnEquipmentChange;
        EquipmentManager.OnCurrentHelmetChange -= OnHelmetChange;
        EquipmentManager.OnPickaxeLevelChange -= OnPickAxeChange;

        playerAttackComponent.onIsAttackingChange -= OnPickAxePositionChange;
    }
    void OnDamageReceived()
    {
        go_currentModel.GetComponent<MaterialChanger>().AssignTemporalMaterial();
        go_currentHelmetModel?.GetComponent<MaterialChanger>().AssignTemporalMaterial();
    }
    void OnPickAxePositionChange(bool isAttacking)
    {
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
        else
            parentToAssign = chestClothGetter.bone_PickAxeBack;

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
