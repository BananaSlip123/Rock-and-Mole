using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class GameMenu : MonoBehaviour
{
    #region SERIALIZABLE
    [Header("NAVIGATION WINDOWS")]
    [SerializeField] GameObject go_mainWindow;
    [SerializeField] GameObject go_pauseWindow;
    [SerializeField] GameObject go_settingsWindow;
    [SerializeField] GameObject go_runInventoryWindow;
    [SerializeField] GameObject go_runInventoryInfoWindow;
    [Header("LIFE BAR")]
    [SerializeField] GameObject go_lifeBar;
    [Header("INPUT NAVIGATION")]
    [SerializeField] PlayerInput playerInput;
    [SerializeField] EventSystem eventSystem;
    [SerializeField] Selectable firstSelected_main;
    [SerializeField] Selectable firstSelected_settings;
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
        RunInventory,
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
        go_runInventoryWindow.SetActive(nextWindow == Windows.RunInventory);
        go_runInventoryInfoWindow.SetActive(nextWindow == Windows.RunInventory);
        go_lifeBar.SetActive(isMain);

        bool isInit = !lastWindow.HasValue;

        if (isMain && (isInit || lastWindow.Value != Windows.Main)) //si isInit entra en el if y no accede a value
            playerInput.SwitchCurrentActionMap("Player");
        else if (!isMain && (isInit || lastWindow.Value == Windows.Main))
            playerInput.SwitchCurrentActionMap("UI");
    }

    void UpdateSelectedButton()
    {

    }
    #endregion

    #region PUBLIC FUNCS
    public void Button_OpenPause() => CurrentWindow = Windows.Pause;
    public void Button_OpenRunInventory() => CurrentWindow = Windows.RunInventory;
    public void Button_OpenSettings() => CurrentWindow = Windows.Settings;
    public void Button_OpenMain() => CurrentWindow = Windows.Main;

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
