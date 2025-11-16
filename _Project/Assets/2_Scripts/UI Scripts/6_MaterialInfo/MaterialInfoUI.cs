using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MaterialInfoUI : MonoBehaviour
{
    //se usa para mostrar materiales de construccion en las tiendas
    //o para mostrar materiales conseguidos en la pantalla game over

    [SerializeField] Image img_imageComponent;
    [SerializeField] TextMeshProUGUI txt_amount;

    private int _amount = -1;
    private MaterialName _materialAssigned;
    public static Dictionary<MaterialName, MaterialsData.IconData> Icons
    {
       get => MaterialsData.Icons;
    }
    public int Amount
    {
        get => _amount;
        set
        {
            if (value != _amount)
            {
                _amount = value;
                txt_amount.text = "x" + value.ToString();
            }
        }
    }
    public MaterialName MaterialAssigned
    {
        get => _materialAssigned;
        set
        {
            _materialAssigned = value;
            img_imageComponent.sprite = Icons[value].Sprite;
            img_imageComponent.color = Icons[value].Color;
        }
    }
}
