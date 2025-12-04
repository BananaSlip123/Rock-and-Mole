using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
public class GameMenu : MonoBehaviour
{
    #region SERIALIZABLE
    [Header("NAVIGATION WINDOWS")]
    [SerializeField] GameObject go_mainWindow;
    [SerializeField] GameObject go_pauseWindow;
    [SerializeField] GameObject go_settingsWindow;
    [SerializeField] GameObject go_runInventoryWindow;
    [SerializeField] GameObject go_runInventoryInfoWindow;
    [SerializeField] GameObject go_cartWindow;
    [SerializeField] GameObject go_gameOverWindow;
    [SerializeField] GameObject go_materialsCollectedWindow;
    [SerializeField] GameObject go_interactionWindow;
    [Header("LIFE BAR")]
    [SerializeField] GameObject go_lifeBar;
    [Header("INPUT NAVIGATION")]
    [SerializeField] InputMapsManager playerInputMapsManager;
    [SerializeField] EventSystem eventSystem;
    [SerializeField] Selectable firstSelected_settings;
    [SerializeField] Selectable firstSelected_pause;
    [SerializeField] Selectable firstSelected_gameOver;
    [SerializeField] Selectable firstSelected_cart;
    [Header("References")]
    [SerializeField] DamageableComponent playerDamageableComponent;
    #endregion

    #region PRIVATE VARS
    Windows _currentWindow = Windows.Main;
    RunInventoryUI inventoryReference;
    bool _canInteract = false;
    #endregion
    #region PUBLIC VARS
    public enum Windows
    {
        Main,
        MainLifeBarInvisible,
        Pause,
        Settings,
        RunInventory,
        GameOver,
        Cart,
    }
    public bool ShowInteractWindow
    {
        get => _canInteract;
        set
        {
            _canInteract = value;

            go_interactionWindow.SetActive(_canInteract && CurrentWindow == Windows.Main);
        }
    }
    public Windows CurrentWindow
    {
        get => _currentWindow;
        set
        {
            Debug.Log("Current Window: " + _currentWindow.ToString());
            Debug.Log("Next Window: " + value.ToString());

            Windows lastWindow = _currentWindow;
            _currentWindow = value;

            SwitchWindow(lastWindow, _currentWindow);
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
        bool isMain = nextWindow == Windows.Main || nextWindow == Windows.MainLifeBarInvisible;

        ShowInteractWindow = _canInteract;

        go_mainWindow.SetActive(isMain);
        go_materialsCollectedWindow.SetActive(isMain);
        go_cartWindow.SetActive(nextWindow == Windows.Cart);
        go_settingsWindow.SetActive(nextWindow == Windows.Settings);
        go_runInventoryWindow.SetActive(nextWindow == Windows.RunInventory);
        go_runInventoryInfoWindow.SetActive(nextWindow == Windows.RunInventory);
        go_lifeBar.SetActive(nextWindow == Windows.Main);
        go_gameOverWindow.SetActive(nextWindow == Windows.GameOver);

        bool isPause = nextWindow == Windows.Pause;
        go_pauseWindow.SetActive(isPause);

        if (isPause) Time.timeScale = 0;
        else Time.timeScale = 1;

        bool isInit = !lastWindow.HasValue;

        if (isMain && (isInit || lastWindow.Value != Windows.Main)) 
        {
            //si es init y estamos en tutorial no se llama, ya q se llamara a el mapa TutorialCallout
            if (!GameData.NeedsTutorial || !isInit)
                playerInputMapsManager.InputMapProperty = InputMapsManager.InputMap.playerAndUi;
        }   
        else if (!isMain && (isInit || lastWindow.Value == Windows.Main))
            playerInputMapsManager.InputMapProperty = InputMapsManager.InputMap.uiNavigation;
    }

    void UpdateSelectedButton()
    {
        if (CurrentWindow == Windows.Settings)
            firstSelected_settings?.Select();
        else if (CurrentWindow == Windows.Pause)
            firstSelected_pause?.Select();
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
        else if (CurrentWindow == Windows.GameOver)
            firstSelected_gameOver?.Select();
        else if (CurrentWindow == Windows.Cart)
            firstSelected_cart?.Select();
    }
    #endregion

    #region PUBLIC FUNCS
    public void OnPlayerVictory()
    {
        CurrentWindow = Windows.GameOver;
        GameOverUI gameOverUI = go_gameOverWindow.GetComponent<GameOverUI>();
        gameOverUI.MaterialsToShow = GameData.Put_RunInventory_Into_Inventory(100);
        gameOverUI.IsDefeat = false;

    }
    public void OnPlayerDeath()
    {
        CurrentWindow = Windows.GameOver;
        GameOverUI gameOverUI = go_gameOverWindow.GetComponent<GameOverUI>();
        gameOverUI.MaterialsToShow = GameData.Put_RunInventory_Into_Inventory(70);
        gameOverUI.IsDefeat = true;
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
    public void Button_Pause(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed)
            return;

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
    public void Button_Cart() => CurrentWindow = Windows.Cart;
    public void Button_OpenSettings() => CurrentWindow = Windows.Settings;
    public void Button_OpenMain() => CurrentWindow = Windows.Main;

    public void OnPointer()
    {
        if (eventSystem?.currentSelectedGameObject != null)
        {
            GameObject selected = eventSystem.currentSelectedGameObject;
            bool isInputField = selected.GetComponent<InputField>() != null ||
                          selected.GetComponent<TMPro.TMP_InputField>() != null;

            if (isInputField) return;
        }

        eventSystem.SetSelectedGameObject(null);
    }
    public void onNavigation()
    {
        if (eventSystem?.currentSelectedGameObject == null || !eventSystem.currentSelectedGameObject.activeInHierarchy)
        {
            UpdateSelectedButton();
        }
    }
    #endregion
}
