using System;
using UnityEngine;
using TMPro;
public class ShopUI : MonoBehaviour
{
    //used in the village scene
    #region SERIALIZABLE
    [SerializeField] GameObject go_error; //salta si intentas comprar y no tienes suficientes materiales?
   
    [SerializeField] TextMeshProUGUI txt_amountAvailable; //marcar en rojo si no tienes suficiente
    [SerializeField] TextMeshProUGUI txt_price;
    [SerializeField] TextMeshProUGUI txt_totalIncome;

    [SerializeField] InventoryUI inventory;
    #endregion
    #region PRIVATE VARS

    #endregion
    #region PUBLIC VARS
    #endregion
    #region PRIVATE FUNCS
    private void Awake()
    {

    }
    private void OnEnable()//al inicio y al activar un objeto
    {
        //Añadir callback sobre el material seleccionado en InventoryUI
        inventory.OnSelectedMaterialChanged += (MaterialName material) =>
        {

        };
    }

    private void OnDisable()
    {
        inventory.OnSelectedMaterialChanged = null;
    }

    void ProcesarNumeroIngresado(int numero) => throw new NotImplementedException();
    #endregion
    #region PUBLIC FUNCS
    public void OnAmountChanged(string userInput)
    {
        if (int.TryParse(userInput, out int number))
        {
            // Llamar a tu función con el número
            ProcesarNumeroIngresado(number);
        }
    }
    #endregion
}
