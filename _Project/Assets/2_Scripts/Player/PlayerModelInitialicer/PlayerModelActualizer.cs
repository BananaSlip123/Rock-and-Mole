using PlayerComponents;
using UnityEngine;

public class PlayerModelActualizer : MonoBehaviour
{
    [SerializeField] Transform t_modelTransform;
    [SerializeField] GameObject go_currentModel;

    PlayerAttackComponent playerAttackComponent;
    PlayerMovementComponent playerMovementComponent;

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

        PickaxePositionProperty = PickaxePosition.back;
    }

    private void OnEnable()
    {
        OnEquipmentChange();

        EquipmentManager.OnCurrentChestClothChange += OnEquipmentChange;
        EquipmentManager.OnCurrentHelmetChange += OnHelmetChange;
        EquipmentManager.OnPickaxeLevelChange += OnPickAxeChange;

        playerAttackComponent.onIsAttackingChange += OnPickAxePositionChange;
    }
    private void OnDisable()
    {
        EquipmentManager.OnCurrentChestClothChange += OnEquipmentChange;
        EquipmentManager.OnCurrentHelmetChange += OnHelmetChange;
        EquipmentManager.OnPickaxeLevelChange += OnPickAxeChange;

        playerAttackComponent.onIsAttackingChange -= OnPickAxePositionChange;
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
        go_currentModel = Instantiate(EquipmentManager.CurrentChestCloth.model, t_modelTransform);

        chestClothGetter = go_currentModel.GetComponent<ChestClothGetter>();

        playerAttackComponent.animator = chestClothGetter.playerAnimator;
        playerMovementComponent.animator = chestClothGetter.playerAnimator;

        OnPickAxeChange();
        OnHelmetChange();
    }
    private void OnHelmetChange()
    {
        if (go_currentHelmetModel != null) Destroy(go_currentHelmetModel);

        Transform parentToAssign = chestClothGetter.bone_Helmet;
        go_currentHelmetModel = Instantiate(EquipmentManager.CurrentHelmet.model, parentToAssign);
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

        MovePickaxe(parentToAssign);
    }
    void MovePickaxe(Transform newParent)
    {
        go_currentPickAxeModel.transform.SetParent(newParent);

        go_currentPickAxeModel.transform.localPosition = new Vector3();
        go_currentPickAxeModel.transform.localEulerAngles = new Vector3();
        go_currentPickAxeModel.transform.localScale = new Vector3(1, 1, 1);
    }
}
