using UnityEngine;

public class VillageNavigation : MonoBehaviour
{
    [SerializeField] VillageMenuUI villageMenuUI;
    public static VillageNavigation Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #region PUBLIC FUNCS
    public void OnShopEntry()
    {
        //q te meta en la tienda por dentro

        villageMenuUI.Button_OpenShop();
    }
    public void OnForgeEntry()
    {
        //q te meta en la forja por dentro
        villageMenuUI.Button_OpenForge();
    }
    public void OnVillageEntry()
    {
        //sales de la forja/tienda/mina a la calle
        villageMenuUI.Button_ReturnToMain();
    }
    public void OnMineEntry()
    {
        //escena de mina
    }
    #endregion
}

