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

    private void Awake()
    {
        playerAttackComponent = GetComponent<PlayerAttackComponent>();
        playerMovementComponent = GetComponent<PlayerMovementComponent>();
    }

    private void OnEnable()
    {
        OnEquipmentChange();

        EquipmentManager.OnCurrentChestClothChange += OnEquipmentChange;
        EquipmentManager.OnCurrentHelmetChange += OnHelmetChange;
        EquipmentManager.OnPickaxeLevelChange += OnPickAxeChange;
    }
    private void OnDisable()
    {
        EquipmentManager.OnCurrentChestClothChange += OnEquipmentChange;
        EquipmentManager.OnCurrentHelmetChange += OnHelmetChange;
        EquipmentManager.OnPickaxeLevelChange += OnPickAxeChange;
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

        Transform parentToAssign = chestClothGetter.bone_PickAxeHand;
        go_currentPickAxeModel = Instantiate(EquipmentManager.CurrentPickaxe.model, parentToAssign);
    }
}
