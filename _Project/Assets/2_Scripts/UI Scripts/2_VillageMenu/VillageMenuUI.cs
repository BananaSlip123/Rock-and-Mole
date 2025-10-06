using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] GameObject go_closeIcon;

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
            SwitchWindow(value);
            _currentWindow = value;
        }
    }
    #endregion
    #region PRIVATE FUNCS
    private void Awake()
    {
        SwitchWindow(Windows.Main);
    }
    void SwitchWindow(Windows nextWindow)
    {
        go_mainWindow.SetActive(nextWindow == Windows.Main);
        go_settingsWindow.SetActive(nextWindow == Windows.Settings);
        go_pauseWindow.SetActive(nextWindow == Windows.Pause);
        go_inventoryWindow.SetActive(nextWindow == Windows.Inventory);
        go_shopWindow.SetActive(nextWindow == Windows.Shop);
        go_forgeWindow.SetActive(nextWindow == Windows.Forge);
        go_wardrobeWindow.SetActive(nextWindow == Windows.Wardrobe);

        go_inventory.SetActive(nextWindow == Windows.Shop || nextWindow == Windows.Inventory);
        go_closeIcon.SetActive(nextWindow != Windows.Main && nextWindow != Windows.Pause);
    }
    #endregion

    #region PUBLIC FUNCS
    public void Button_OnClose()
    {
        if (CurrentWindow == Windows.Settings)
            CurrentWindow = Windows.Pause;
        else
            CurrentWindow = Windows.Main;
    }
    public void Button_Pause() => CurrentWindow = Windows.Pause;
    public void Button_OpenInventory() => CurrentWindow = Windows.Inventory;
    public void Button_OpenSettings() => CurrentWindow = Windows.Settings;
    public void Button_ReturnToMain() => CurrentWindow = Windows.Main;
    public void Button_ReturnToMenuScene() => SceneManager.LoadScene("1_MAIN_SCENE");
    #endregion
}
