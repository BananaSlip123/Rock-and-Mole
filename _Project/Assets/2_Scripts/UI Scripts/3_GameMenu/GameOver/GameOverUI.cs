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

            int idx = 0;
            foreach(MaterialName key in value.Keys)
            {
                if (value.ContainsKey(key) && value[key] != 0)
                {
                    if (idx >= materialsInfo.Length) 
                        throw new System.Exception("Tiene que haber mas huecos en el array que posibles valores de material");

                    materialsInfo[idx].gameObject.SetActive(true);
                    materialsInfo[idx].Amount = value[key];
                    materialsInfo[idx].MaterialAssigned = key;
                }
            }
            //desactivar el resto
            for(; idx < materialsInfo.Length; idx++)
            {
                materialsInfo[idx].gameObject.SetActive(false);
            }
        }
    }
    
}
