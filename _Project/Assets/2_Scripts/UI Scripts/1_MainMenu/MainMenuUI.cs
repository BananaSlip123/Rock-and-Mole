using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    #region SERIALIZABLE
    [SerializeField] GameObject go_mainWindow;
    [SerializeField] GameObject go_settingsWindow;
    [SerializeField] GameObject go_creditsWindow;
    [SerializeField] GameObject go_closeIcon;
    #endregion
    #region PUBLIC VARS
    public enum Windows
    {
        Main,
        Settings,
        Credits
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
        go_creditsWindow.SetActive(nextWindow == Windows.Credits);
        go_closeIcon.SetActive(nextWindow != Windows.Main);
    }
    #endregion

    #region PUBLIC FUNCS
    public void SwitchToMain() => SwitchWindow(Windows.Main);
    public void SwitchToSettings() => SwitchWindow(Windows.Settings);
    public void SwitchToCredits() => SwitchWindow(Windows.Credits);
    public void SwitchToGameScene() => SceneManager.LoadScene("2_VILLAGE_SCENE");
    #endregion
}
