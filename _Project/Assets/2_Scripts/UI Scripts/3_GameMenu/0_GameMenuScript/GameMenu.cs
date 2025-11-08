using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public class GameMenu : MonoBehaviour
{
    #region SERIALIZABLE
    [Header("NAVIGATION WINDOWS")]
    [SerializeField] GameObject go_mainWindow;
    [SerializeField] GameObject go_pauseWindow;
    [SerializeField] GameObject go_settingsWindow;
    [SerializeField] GameObject go_runInventoryWindow;
    [SerializeField] GameObject go_runInventoryInfoWindow;
    [SerializeField] GameObject go_gameOverWindow;
    [Header("LIFE BAR")]
    [SerializeField] GameObject go_lifeBar;
    [Header("INPUT NAVIGATION")]
    [SerializeField] InputMapsManager playerInputMapsManager;
    [SerializeField] EventSystem eventSystem;
    [SerializeField] Selectable firstSelected_settings;
    [SerializeField] Selectable firstSelected_pause;
    [Header("References")]
    [SerializeField] DamageableComponent playerDamageableComponent;
    #endregion

    #region PRIVATE VARS
    Windows _currentWindow = Windows.Main;
    RunInventoryUI inventoryReference;
    #endregion
    #region PUBLIC VARS
    public enum Windows
    {
        Main,
        Pause,
        Settings,
        RunInventory,
        GameOver,
    }

    public Windows CurrentWindow
    {
        get => _currentWindow;
        set
        {
            Debug.Log("Current Window: " + _currentWindow.ToString());
            Debug.Log("Next Window: " + value.ToString());
            SwitchWindow(_currentWindow, value);
            _currentWindow = value;
            UpdateSelectedButton();
        }
    }
    #endregion
    #region PRIVATE FUNCS
    private void OnEnable()
    {
        playerDamageableComponent.OnDeath += OnPlayerDeath;
    }
    private void OnDisable()
    {
        playerDamageableComponent.OnDeath -= OnPlayerDeath;
    }
    private void Start()
    {
        SwitchWindow(null, Windows.Main);
        inventoryReference = go_runInventoryWindow.GetComponent<RunInventoryUI>();
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
        go_gameOverWindow.SetActive(nextWindow == Windows.GameOver);

        bool isInit = !lastWindow.HasValue;

        if (isMain && (isInit || lastWindow.Value != Windows.Main)) 
        {
            //si isInit entra en el if y no accede a value
            //playerInputMapsManager.SwitchCurrentActionMap("Player");

            //si es init y estamos en tutorial no se llama, ya q se llamara a el mapa TutorialCallout
            if (!GameData.NeedsTutorial || !isInit)
                playerInputMapsManager.InputMapProperty = InputMapsManager.InputMap.playerAndUi;
        }   
        else if (!isMain && (isInit || lastWindow.Value == Windows.Main))
            //playerInputMapsManager.SwitchCurrentActionMap("UI");
            playerInputMapsManager.InputMapProperty = InputMapsManager.InputMap.uiNavigation;
    }

    void UpdateSelectedButton()
    {
        if (CurrentWindow == Windows.Settings)
            firstSelected_settings.Select();
        else if (CurrentWindow == Windows.Pause)
            firstSelected_pause.Select();
        else if (CurrentWindow == Windows.RunInventory)
        {
            Selectable firstSlot = inventoryReference.FirstElementToSelect;
            if (firstSlot == null)
            {
                Debug.Log("grr");
                CurrentWindow = Windows.Main;
            }
            else firstSlot.Select();
        }
    }
    #endregion

    #region PUBLIC FUNCS
    public void OnPlayerVictory(Dictionary<MaterialName, int> materialesConseguidos)
    {

    }
    public void OnPlayerDeath(Dictionary<MaterialName, int> materialesConseguidos)
    {
        CurrentWindow = Windows.GameOver;
        go_gameOverWindow.GetComponent<GameOverUI>().MaterialsToShow = materialesConseguidos;
    }
    public void Button_ReturnToVillage()
    {
        SceneManager.LoadScene("1_VILLAGE_SCENE");
    }
    public void Button_Pause()
    {
        if (CurrentWindow == Windows.Main)
            CurrentWindow = Windows.Pause;
        else
            CurrentWindow = Windows.Main;
    }
    public void Button_Inventory()
    {
        if (CurrentWindow == Windows.RunInventory)
            CurrentWindow = Windows.Main;
        else
            CurrentWindow = Windows.RunInventory;
    }
    public void Button_OpenSettings() => CurrentWindow = Windows.Settings;
    public void Button_OpenMain() => CurrentWindow = Windows.Main;

    public void onPointer() => eventSystem?.SetSelectedGameObject(null);
    public void onNavigation()
    {
        if (eventSystem?.currentSelectedGameObject == null)
        {
            UpdateSelectedButton();
        }
    }
    #endregion
}
