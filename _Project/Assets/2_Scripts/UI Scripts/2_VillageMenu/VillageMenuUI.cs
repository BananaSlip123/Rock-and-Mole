using UnityEngine;
using UnityEngine.SceneManagement;

public class VillageMenuUI : MonoBehaviour
{
    #region SERIALIZABLE
    [SerializeField] GameObject go_mainWindow;
    [SerializeField] GameObject go_settingsWindow;
    #endregion
    #region PUBLIC VARS
    public enum Windows
    {
        Main,
        Settings,
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
    }
    #endregion

    #region PUBLIC FUNCS
    public void SwitchToMain() => SwitchWindow(Windows.Main);
    #endregion
}
