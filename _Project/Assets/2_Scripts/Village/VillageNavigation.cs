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

        OnSellInteraction();//esto no deberia ir aqui realmente, de momento si
    }
    public void OnSellInteraction() => villageMenuUI.Button_OpenShop();
    public void OnForgeEntry()
    {
        //q te meta en la forja por dentro
        OnForgeInteraction();
    }
    public void OnForgeInteraction() => villageMenuUI.Button_OpenForge();
    public void OnVillageEntry()
    {
        //sales de la forja/tienda/mina a la calle
        //villageMenuUI.Button_OpenMain();
    }
    public void OnMineEntry()
    {
        //escena de mina
    }
    #endregion
}

