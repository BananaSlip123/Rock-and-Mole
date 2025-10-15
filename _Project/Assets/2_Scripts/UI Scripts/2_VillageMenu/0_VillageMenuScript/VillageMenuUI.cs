using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class VillageMenuUI : MonoBehaviour
{
    #region SERIALIZABLE
    [Header("PLAYER INPUT")]
    [SerializeField] PlayerInput playerInput;
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

    #endregion
    #region PRIVATE VARS
    Windows _currentWindow = Windows.Main;
    #endregion
    #region PUBLIC VARS
    public enum Windows
    {
        Main,
        Pause,
        Settings,
        Inventory,
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
        }
    }
    #endregion
    #region PRIVATE FUNCS
    private void Awake()
    {
        SwitchWindow(null, Windows.Main);
    }
    void SwitchWindow(Windows? lastWindow, Windows nextWindow)
    {
        bool isMain = nextWindow == Windows.Main;
        go_mainWindow.SetActive(isMain);
        go_settingsWindow.SetActive(nextWindow == Windows.Settings);
        go_pauseWindow.SetActive(nextWindow == Windows.Pause);
        go_inventoryWindow.SetActive(nextWindow == Windows.Inventory);
        go_shopWindow.SetActive(nextWindow == Windows.Shop);
        go_forgeWindow.SetActive(nextWindow == Windows.Forge);
        go_wardrobeWindow.SetActive(nextWindow == Windows.Wardrobe);

        go_inventory.SetActive(nextWindow == Windows.Shop || nextWindow == Windows.Inventory);

        bool isInit = !lastWindow.HasValue;

        if (isMain && (isInit || lastWindow.Value != Windows.Main)) //si isInit entra en el if y no accede a value
            playerInput.SwitchCurrentActionMap("Player");
        else if (!isMain && (isInit || lastWindow.Value == Windows.Main))
            playerInput.SwitchCurrentActionMap("UI");

        //go_closeIcon.SetActive(nextWindow != Windows.Main && nextWindow != Windows.Pause);
    }
    #endregion

    #region PUBLIC FUNCS
    public void Button_OpenPause() => CurrentWindow = Windows.Pause;
    public void Button_OpenInventory() => CurrentWindow = Windows.Inventory;
    public void Button_OpenShop() => CurrentWindow = Windows.Shop;
    public void Button_OpenForge() => CurrentWindow = Windows.Forge;
    public void Button_OpenWardrobe() => CurrentWindow = Windows.Wardrobe;
    public void Button_OpenSettings() => CurrentWindow = Windows.Settings;
    public void Button_OpenMain() => CurrentWindow = Windows.Main;
    public void Button_ReturnToMenuScene() => SceneManager.LoadScene("1_MAIN_SCENE");
    #endregion
}
