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
    [SerializeField] GameObject go_skipTutorialWindow;
    [SerializeField] GameObject go_closeIcon;
    [Header("INPUT NAVIGATION")]
    [SerializeField] PlayerInput playerInput;

    [SerializeField] EventSystem eventSystem;
    [SerializeField] Selectable firstSelected_main;
    [SerializeField] Selectable firstSelected_settings;
    [SerializeField] Selectable firstSelected_credits;
    [SerializeField] Selectable firstSelected_skipTutorial;

    Selectable lastSelected;
    Windows currentWindow;
    #endregion
    #region PUBLIC VARS
    public enum Windows
    {
        Main,
        Settings,
        Credits,
        SkipTutorial,
    }
    #endregion
    #region PRIVATE FUNCS
    private void Awake()
    {
        SwitchWindow(Windows.Main);
        playerInput.SwitchCurrentActionMap("UI");
        AudioManager.Instance?.StopAudio();
        AudioManager.Instance?.PlayMusic(AudioManager.MusicType.MenuMusic);
    }
    void SwitchWindow(Windows nextWindow)
    {
        go_mainWindow.SetActive(nextWindow == Windows.Main);
        go_settingsWindow.SetActive(nextWindow == Windows.Settings);
        go_creditsWindow.SetActive(nextWindow == Windows.Credits);
        go_closeIcon.SetActive(nextWindow == Windows.Settings || nextWindow == Windows.Credits);

        go_skipTutorialWindow.SetActive(nextWindow == Windows.SkipTutorial);

        currentWindow = nextWindow;

        UpdateSelectedButton();
    }

    void UpdateSelectedButton()
    {
        if (currentWindow == Windows.Main)
            firstSelected_main?.Select();
        else if (currentWindow == Windows.Settings)
            firstSelected_settings?.Select();
        else if (currentWindow == Windows.Credits)
            firstSelected_credits?.Select();
        else if (currentWindow == Windows.SkipTutorial)
            firstSelected_skipTutorial.Select();
    }
    #endregion

    #region PUBLIC FUNCS
    public void SwitchToMain() => SwitchWindow(Windows.Main);
    public void SwitchToSettings() => SwitchWindow(Windows.Settings);
    public void SwitchToCredits() => SwitchWindow(Windows.Credits);
    public void ButtonPlay()
    {
        if (GameData.NeedsTutorial)
            SwitchWindow(Windows.SkipTutorial);
        else
            SceneManager.LoadScene("1_VILLAGE_SCENE");
    }

    public void ButtonSkipTutorial(bool wantToSkip)
    {
        if (wantToSkip){
            GameData.NeedsTutorial = false;
            SceneManager.LoadScene("1_VILLAGE_SCENE");
        }
        else
            SceneManager.LoadScene("0_Tutorial");
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
