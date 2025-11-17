using PlayerComponents;
using UnityEngine;

public class PlayerModelActualizer : MonoBehaviour
{
    [SerializeField] Transform t_modelTransform;
    [SerializeField] GameObject go_currentModel;
    [SerializeField] PlayerAttackComponent playerAttackComponent;
    [SerializeField] PlayerMovementComponent playerMovementComponent;

    GameObject go_currentHelmetModel = null;
    GameObject go_currentPickAxeModel = null;

    ChestClothGetter chestClothGetter = null;

    private void OnEnable()
    {

    }
    private void OnDisable()
    {
        
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
    }
    private void OnPickAxeChange()
    {
        if (go_currentPickAxeModel != null) Destroy(go_currentPickAxeModel);
    }
}
