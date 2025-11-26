using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class VillageMenuUI : MonoBehaviour
{
    #region SERIALIZABLE
    [Header("NAVEGATION WINDOWS")]
    [SerializeField] GameObject go_mainWindow;
    [SerializeField] GameObject go_pauseWindow;
    [SerializeField] GameObject go_settingsWindow;
    [SerializeField] GameObject go_inventoryWindow;
    [SerializeField] GameObject go_cartWindow;
    [SerializeField] GameObject go_shopWindow;
    [SerializeField] GameObject go_forgeWindow;
    [SerializeField] GameObject go_wardrobeWindow;
    [SerializeField] GameObject go_interactionWindow;

    [Header("COMMON ELEMENTS")] //elementos compartidos por varias ventanas
    [SerializeField] GameObject go_inventory; //used by shop & inventory windows
    
    [Header("INPUT NAVIGATION")]
    [SerializeField] InputMapsManager playerInputMapsManager;
    [SerializeField] EventSystem eventSystem;
    [SerializeField] Selectable firstSelected_pause;
    [SerializeField] Selectable firstSelected_settings;
    [SerializeField] Selectable firstSelected_forge;
    [SerializeField] Selectable firstSelected_wardrobe;
    [SerializeField] Selectable firstSelected_cart;

    #endregion
    #region PRIVATE VARS
    Windows _currentWindow = Windows.Main;
    InventoryUI inventoryReference;
    bool _canInteract = false;
    #endregion
    #region PUBLIC VARS / PROPETIES
    public enum Windows
    {
        Main,
        Pause,
        Settings,
        InventoryInfo,
        Shop,
        Forge,
        Wardrobe,
        Cart,
    }
    public bool ShowInteractWindow
    {
        get => _canInteract;
        set
        {
            _canInteract = value;
            
            go_interactionWindow.SetActive(_canInteract && CurrentWindow == Windows.Main);
        }
    }
    public Windows CurrentWindow
    {
        get => _currentWindow;
        set
        {
            Debug.Log("Current Window: "+_currentWindow.ToString());
            Debug.Log("Next Window: " + value.ToString());

            Windows lastWindow = _currentWindow;
            _currentWindow = value;

            SwitchWindow(lastWindow, _currentWindow);
            UpdateSelectedButton();
        }
    }
    #endregion
    #region PRIVATE FUNCS
    private void Start()
    {
        SwitchWindow(null, Windows.Main);
        inventoryReference = go_inventory.GetComponent<InventoryUI>();
    }
    void SwitchWindow(Windows? lastWindow, Windows nextWindow)
    {
        bool isMain = nextWindow == Windows.Main;
        go_mainWindow.SetActive(isMain);

        ShowInteractWindow = _canInteract;

        go_settingsWindow.SetActive(nextWindow == Windows.Settings);

        go_shopWindow.SetActive(nextWindow == Windows.Shop);
        go_cartWindow.SetActive(nextWindow == Windows.Cart);
        go_forgeWindow.SetActive(nextWindow == Windows.Forge);
        go_pauseWindow.SetActive(nextWindow == Windows.Pause);
        go_wardrobeWindow.SetActive(nextWindow == Windows.Wardrobe);
        go_inventoryWindow.SetActive(nextWindow == Windows.InventoryInfo);
        go_inventory.SetActive(nextWindow == Windows.Shop || nextWindow == Windows.InventoryInfo);
        

        bool isInit = !lastWindow.HasValue;

        if (isMain && (isInit || lastWindow.Value != Windows.Main)) //si isInit entra en el if y no accede a value
        {
            if (!GameData.NeedsTutorial || !isInit)
                //playerInputMapsManager.SwitchCurrentActionMap("Player");
                playerInputMapsManager.InputMapProperty = InputMapsManager.InputMap.playerAndUi;
        }
        else if (!isMain && (isInit || lastWindow.Value == Windows.Main))
            //playerInputMapsManager.SwitchCurrentActionMap("UI");
            playerInputMapsManager.InputMapProperty = InputMapsManager.InputMap.uiNavigation;
    }

    void UpdateSelectedButton()
    {
        if (CurrentWindow == Windows.Settings)
            firstSelected_settings?.Select();
        else if (CurrentWindow == Windows.Pause)
            firstSelected_pause?.Select();
        else if (CurrentWindow == Windows.InventoryInfo || CurrentWindow == Windows.Shop)
        {
            Selectable firstSlot = inventoryReference.FirstElementToSelect;
            if (firstSlot == null) CurrentWindow = Windows.Main;
            else firstSlot.Select();
        }
        else if (CurrentWindow == Windows.Wardrobe)
            firstSelected_wardrobe?.Select();
        else if (CurrentWindow == Windows.Forge)
            firstSelected_forge?.Select();
        else if (CurrentWindow == Windows.Cart)
            firstSelected_cart?.Select();
    }
    #endregion

    #region PUBLIC FUNCS
    public void Button_Pause()
    {
        if (CurrentWindow == Windows.Main)
            CurrentWindow = Windows.Pause;
        else if(CurrentWindow == Windows.Pause)
            CurrentWindow = Windows.Main;
    }

    public void Button_Inventory()
    {
        if (CurrentWindow == Windows.InventoryInfo || CurrentWindow  == Windows.Shop)
            CurrentWindow = Windows.Main;
        else
            CurrentWindow = Windows.InventoryInfo;
    }
    public void Button_Cart() => CurrentWindow = Windows.Cart;
    public void Button_OpenShop() => CurrentWindow = Windows.Shop;
    public void Button_OpenForge() => CurrentWindow = Windows.Forge;
    public void Button_OpenWardrobe() => CurrentWindow = Windows.Wardrobe;
    public void Button_OpenSettings() => CurrentWindow = Windows.Settings;
    public void Button_OpenMain() => CurrentWindow = Windows.Main;
    public void Button_ReturnToMenuScene() => SceneManager.LoadScene("-1_MAIN_SCENE");

    public void OnPointer()
    {
        if (eventSystem?.currentSelectedGameObject != null)
        {
            GameObject selected = eventSystem.currentSelectedGameObject;
            bool isInputField = selected.GetComponent<InputField>() != null ||
                          selected.GetComponent<TMPro.TMP_InputField>() != null;

            if (isInputField) return;
        }

        eventSystem.SetSelectedGameObject(null);
    }
    public void onNavigation()
    {
        if (eventSystem.currentSelectedGameObject == null || !eventSystem.currentSelectedGameObject.activeInHierarchy)
        {
            UpdateSelectedButton();
        }
    }
    #endregion
}
