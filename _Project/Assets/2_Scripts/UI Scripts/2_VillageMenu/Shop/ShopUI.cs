using UnityEngine;
using TMPro;
using System.Collections;
public class ShopUI : MonoBehaviour
{
    //used in the village scene
    #region SERIALIZABLE
    [SerializeField] GameObject go_noMaterialSelectedUI;
    [SerializeField] GameObject go_materialSelectedUI;

    [SerializeField] TextMeshProUGUI txt_amountAvailable; //marcar en rojo si no tienes suficiente
    [SerializeField] TextMeshProUGUI txt_price;
    [SerializeField] TextMeshProUGUI txt_totalProfit;
    [SerializeField] TextMeshProUGUI txt_materialName;

    [SerializeField] InventoryUI inventory;
    
    [SerializeField] GameObject go_error; //salta si intentas comprar y no tienes suficientes materiales?
    #endregion
    #region PRIVATE VARS
    bool _init = false;
    bool _isMaterialSelected = false, _canSell = false;
    int _selectedAmount = 0;
    int _price = 0, _profit;
    int _availableAmount = 0;
    MaterialName _materialSelected;
    private Coroutine currentFade;
    int SelectedAmount
    {
        set
        {
            _selectedAmount = value;
            Profit = _price * value;

            _canSell = _selectedAmount <= _availableAmount;

            if (_canSell)
                txt_amountAvailable.color = new Color(0, 0, 0);
            else
                txt_amountAvailable.color = new Color(1, 0, 0);
        }
    }
    int Profit
    {
        set 
        {
            _profit = value;
            txt_totalProfit.text = value.ToString();
        } 
    }
    int Price
    {
        set
        {
            _price = value;
            txt_price.text = value.ToString();
            Profit = _selectedAmount * value;
        }
    }
    int AvailableAmount
    {
        set
        {
            _availableAmount = value;
            txt_amountAvailable.text = value.ToString();
        }
    }
    bool IsMaterialSelected
    {
        set
        {
            if(_init || _isMaterialSelected != value )
            {
                _isMaterialSelected = value;

                go_noMaterialSelectedUI.SetActive(!value);
                go_materialSelectedUI.SetActive(value);
            }
        }
    }
    MaterialName MaterialSelected
    {
        set
        {
            _materialSelected = value;
            txt_materialName.text = value.ToString();
        }
    }
    #endregion
    #region PUBLIC VARS
    #endregion
    #region PRIVATE FUNCS
    private void Awake()
    {
        
    }
    private void OnEnable()//al inicio y al activar un objeto
    {
        _init = true;
        go_error.SetActive(false);

        UpdateShopUI();

        //Añadir callback sobre el material seleccionado en InventoryUI
        inventory.OnSelectedMaterial += OnSelectedMaterial;
        inventory.OnUnSelectedMaterial += OnUnSelectedMaterial;
    }

    private void UpdateShopUI()
    {
        if (inventory.HasMaterialSelected)
            OnSelectedMaterial(inventory.SelectedMaterial);
        else
            OnUnSelectedMaterial();
    }

    private void OnDisable()
    {
        _init = false;
        inventory.OnSelectedMaterial -= OnSelectedMaterial;
    }

    private void OnSelectedMaterial(MaterialName material)
    {
        MaterialSelected = material;
        IsMaterialSelected = true;
        AvailableAmount = GameData.Inventory.GetAmount(material);
        Price =  GameData.MaterialsPrices[material];
        ProcesarNumeroIngresado(_selectedAmount);
    }
    private void OnUnSelectedMaterial()
    {
        IsMaterialSelected = false;
    }
    void ProcesarNumeroIngresado(int numero) => SelectedAmount = numero;

    private void ShowErrorMessage()
    {
        // Detener fade anterior y comenzar uno nuevo
        if (currentFade != null)
            StopCoroutine(currentFade);

        currentFade = StartCoroutine(FadeSequenceCoroutine());
    }
    private IEnumerator FadeSequenceCoroutine()
    {
        if (!go_error.activeInHierarchy)
        {
            go_error.SetActive(true);
        }
        TextMeshProUGUI textToFade = go_error.GetComponent<TextMeshProUGUI>();
        Color originalColor = textToFade.color;
        textToFade.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

        yield return null;//esperar un frame para q se active sin problemas

        float duration = 2f;
        float interval = 0.2f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            textToFade.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            elapsedTime += interval;
            yield return new WaitForSeconds(interval);
        }

        textToFade.gameObject.SetActive(false);

        currentFade = null;
    }
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

    public void SellMaterial()
    {
        if (_canSell)
        {
            bool selled = GameData.Inventory.TryRemoveObject(_materialSelected, _selectedAmount);
            if (selled)
                GameData.Coins += _profit;
        }
        else
        {
            ShowErrorMessage();
        }
        UpdateShopUI();
    }
    #endregion
}
