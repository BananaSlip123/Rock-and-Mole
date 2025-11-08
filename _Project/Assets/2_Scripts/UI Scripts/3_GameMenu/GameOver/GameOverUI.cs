using UnityEngine;
using TMPro;
using System.Collections.Generic;
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
        PlayerStats playerStats = FindFirstObjectByType<PlayerStats>();
        if (playerStats != null)
            RoomNumber = playerStats.roomNumber;
    }
    public Dictionary<MaterialName,int> MaterialsToShow
    {
        set
        {
            if (value == null) return;
            foreach (MaterialInfoUI mat in materialsInfo)
            {
                MaterialName key = mat.MaterialAssigned;
                
                if (value.ContainsKey(key) && value[key]!= 0)
                {
                    mat.gameObject.SetActive(true);
                    mat.Amount = value[key];
                    mat.MaterialAssigned = key;
                }
                else
                {
                    mat.gameObject.SetActive(false);
                }
            }
        }
    }
    
}
