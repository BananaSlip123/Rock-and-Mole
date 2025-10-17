using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
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
    [SerializeField] GameObject go_shopWindow;
    [SerializeField] GameObject go_forgeWindow;
    [SerializeField] GameObject go_wardrobeWindow;
    [Header("COMMON ELEMENTS")] //elementos compartidos por varias ventanas
    [SerializeField] GameObject go_inventory; //used by shop & inventory windows
   // [SerializeField] GameObject go_closeIcon;
    
    [Header("INPUT NAVIGATION")]
    [SerializeField] PlayerInput playerInput;
    [SerializeField] EventSystem eventSystem;
    [SerializeField] Selectable firstSelected_pause;
    [SerializeField] Selectable firstSelected_settings;
    [SerializeField] Selectable firstSelected_forge;
    [SerializeField] Selectable firstSelected_wardrobe;

    #endregion
    #region PRIVATE VARS
    Windows _currentWindow = Windows.Main;
    InventoryUI inventoryReference;
    #endregion
    #region PUBLIC VARS
    public enum Windows
    {
        Main,
        Pause,
        Settings,
        InventoryInfo,
        Shop,
        Forge,
        Wardrobe,
    }

    public Windows CurrentWindow
    {
        get => _currentWindow;
        set
        {
            SwitchWindow(_currentWindow, value);
            _currentWindow = value;
            UpdateSelectedButton();
        }
    }
    #endregion
    #region PRIVATE FUNCS
    private void Awake()
    {
        SwitchWindow(null, Windows.Main);
        inventoryReference = go_inventory.GetComponent<InventoryUI>();
    }
    void SwitchWindow(Windows? lastWindow, Windows nextWindow)
    {
        bool isMain = nextWindow == Windows.Main;
        go_mainWindow.SetActive(isMain);
        go_settingsWindow.SetActive(nextWindow == Windows.Settings);
        go_pauseWindow.SetActive(nextWindow == Windows.Pause);
        go_inventoryWindow.SetActive(nextWindow == Windows.InventoryInfo);
        go_shopWindow.SetActive(nextWindow == Windows.Shop);
        go_forgeWindow.SetActive(nextWindow == Windows.Forge);
        go_wardrobeWindow.SetActive(nextWindow == Windows.Wardrobe);

        go_inventory.SetActive(nextWindow == Windows.Shop || nextWindow == Windows.InventoryInfo);

        bool isInit = !lastWindow.HasValue;

        if (isMain && (isInit || lastWindow.Value != Windows.Main)) //si isInit entra en el if y no accede a value
            playerInput.SwitchCurrentActionMap("Player");
        else if (!isMain && (isInit || lastWindow.Value == Windows.Main))
            playerInput.SwitchCurrentActionMap("UI");

        //go_closeIcon.SetActive(nextWindow != Windows.Main && nextWindow != Windows.Pause);
    }

    void UpdateSelectedButton()
    {
        if (CurrentWindow == Windows.Settings)
            firstSelected_settings.Select();
        else if (CurrentWindow == Windows.Pause)
            firstSelected_pause.Select();
        else if (CurrentWindow == Windows.InventoryInfo || CurrentWindow == Windows.Shop)
        {
            Selectable firstSlot = inventoryReference.FirstElementToSelect;
            if (firstSlot == null) CurrentWindow = Windows.Main;
            else firstSlot.Select();
        }
        else if (CurrentWindow == Windows.Wardrobe)
            firstSelected_wardrobe.Select();
        else if (CurrentWindow == Windows.Forge)
            firstSelected_forge.Select();
    }
    #endregion

    #region PUBLIC FUNCS
    public void Button_OpenPause() => CurrentWindow = Windows.Pause;
    public void Button_OpenInventory() => CurrentWindow = Windows.InventoryInfo;
    public void Button_OpenShop() => CurrentWindow = Windows.Shop;
    public void Button_OpenForge() => CurrentWindow = Windows.Forge;
    public void Button_OpenWardrobe() => CurrentWindow = Windows.Wardrobe;
    public void Button_OpenSettings() => CurrentWindow = Windows.Settings;
    public void Button_OpenMain() => CurrentWindow = Windows.Main;
    public void Button_ReturnToMenuScene() => SceneManager.LoadScene("1_MAIN_SCENE");

    public void onPointer() => eventSystem.SetSelectedGameObject(null);
    public void onNavigation()
    {
        if (eventSystem.currentSelectedGameObject == null)
        {
            UpdateSelectedButton();
        }
    }
    #endregion
}
