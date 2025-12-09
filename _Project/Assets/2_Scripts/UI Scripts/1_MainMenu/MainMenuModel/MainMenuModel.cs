using UnityEngine;

public class MainMenuModel : MonoBehaviour
{
    //leer q skin hay guardada y mostrarla
    [SerializeField] Transform modelTransform;
    void Start()
    {
        GameObject chestCloth = Instantiate(EquipmentManager.CurrentChestCloth.model);
        GameObject helmet = Instantiate(EquipmentManager.CurrentHelmet.model);
        GameObject pickAxe = Instantiate(EquipmentManager.CurrentPickaxe.model);

        AssignParent(chestCloth.transform, modelTransform);

        Transform helmetTransform = chestCloth.GetComponent<ChestClothGetter>().bone_Helmet;
        AssignParent(helmet.transform, helmetTransform);
        
        Transform pickAxeTransform = chestCloth.GetComponent<ChestClothGetter>().bone_PickAxeHand;
        AssignParent(pickAxe.transform, pickAxeTransform);
    }
    void AssignParent(Transform objectTransform, Transform parentTransform)
    {
        objectTransform.SetParent(parentTransform);
        objectTransform.localPosition = new Vector3();
        objectTransform.localEulerAngles = new Vector3();
        objectTransform.localScale = new Vector3(1, 1, 1);
    }
}
