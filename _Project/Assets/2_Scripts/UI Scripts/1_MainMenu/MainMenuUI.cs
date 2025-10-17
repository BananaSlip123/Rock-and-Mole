using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MainMenuUI : MonoBehaviour
{
    #region SERIALIZABLE
    [Header("NAVIGATION WINDOWS")]
    [SerializeField] GameObject go_mainWindow;
    [SerializeField] GameObject go_settingsWindow;
    [SerializeField] GameObject go_creditsWindow;
    [SerializeField] GameObject go_closeIcon;
    [Header("INPUT NAVIGATION")]
    [SerializeField] PlayerInput playerInput;
    [SerializeField] EventSystem eventSystem;
    [SerializeField] Selectable firstSelected_main;
    [SerializeField] Selectable firstSelected_settings;
    [SerializeField] Selectable firstSelected_credits;

    Selectable lastSelected;
    Windows current;
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
        playerInput.SwitchCurrentActionMap("UI");
    }
    void SwitchWindow(Windows nextWindow)
    {
        go_mainWindow.SetActive(nextWindow == Windows.Main);
        go_settingsWindow.SetActive(nextWindow == Windows.Settings);
        go_creditsWindow.SetActive(nextWindow == Windows.Credits);
        go_closeIcon.SetActive(nextWindow != Windows.Main);

        current = nextWindow;

        UpdateSelectedButton();
    }

    void UpdateSelectedButton( )
    {
        if (current == Windows.Main)
            firstSelected_main.Select();
        else if (current == Windows.Settings)
            firstSelected_settings.Select();
        else if (current == Windows.Credits)
            firstSelected_credits.Select();
    }
    #endregion

    #region PUBLIC FUNCS
    public void SwitchToMain() => SwitchWindow(Windows.Main);
    public void SwitchToSettings() => SwitchWindow(Windows.Settings);
    public void SwitchToCredits() => SwitchWindow(Windows.Credits);
    public void SwitchToGameScene()
    {
        if (GameData.NeedsTutorial)
            SceneManager.LoadScene("0_Tutorial");
        else
            SceneManager.LoadScene("2_VILLAGE_SCENE");
    }

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
