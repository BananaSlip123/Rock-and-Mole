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
