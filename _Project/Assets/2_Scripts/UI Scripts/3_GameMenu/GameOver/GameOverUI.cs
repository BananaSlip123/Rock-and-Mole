using UnityEngine;
using TMPro;
public class GameOverUI : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] TextMeshProUGUI txt_roomsNumber;

    [Header("Materials")]
    [SerializeField] MaterialInfoUI[] materialsInfo;

    int RoomNumber
    {
        set => txt_roomsNumber.text = value.ToString();
    }
    private void OnEnable()
    {
        foreach (MaterialInfoUI mat in materialsInfo)
        {
            //mat.Amount = ;
            //mat.MaterialAssigned = ;
        }
    }
}
