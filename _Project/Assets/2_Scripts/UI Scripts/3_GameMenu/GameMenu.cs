using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    #region SERIALIZABLE
    [SerializeField] GameObject go_mainWindow;
    [SerializeField] GameObject go_pauseWindow;
    [SerializeField] GameObject go_settingsWindow;
    [SerializeField] GameObject go_runInventoryWindow;
    [SerializeField] GameObject go_runInventoryInfoWindow;
    [SerializeField] GameObject go_lifeBar;
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
        go_runInventoryWindow.SetActive(nextWindow == Windows.RunInventory);
        go_runInventoryInfoWindow.SetActive(nextWindow == Windows.RunInventory);
        go_lifeBar.SetActive(nextWindow == Windows.Main);
    }
    #endregion

    #region PUBLIC FUNCS
    public void Button_OpenPause() => CurrentWindow = Windows.Pause;
    public void Button_OpenRunInventory() => CurrentWindow = Windows.RunInventory;
    public void Button_OpenSettings() => CurrentWindow = Windows.Settings;
    public void Button_OpenMain() => CurrentWindow = Windows.Main;

    #endregion
}
