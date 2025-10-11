using UnityEngine;
using UnityEngine.SceneManagement;

public class VillageNavigation : MonoBehaviour
{
    [SerializeField] VillageMenuUI villageMenuUI;
    [SerializeField] GameObject go_village;
    [SerializeField] GameObject go_shop;

    //[SerializeField] GameObject go_playerRef;
    //[SerializeField] GameObject go_forge;

    public static VillageNavigation Instance { get; private set; }

    enum Locations
    {
        village,
        shop, 
        forge,
    }

    Locations Location
    {
        set
        {
            go_village.SetActive(value == Locations.village);
            go_shop.SetActive(value == Locations.shop);
        }
    }

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
        Location = Locations.village;
    }
    #region PUBLIC FUNCS
    public void OnShopEntry()
    {
        //q te meta en la tienda por dentro
        Location = Locations.shop;

    }
    public void OnSellInteraction() => villageMenuUI.Button_OpenShop();
    public void OnWardrobeInteraction() => villageMenuUI.Button_OpenWardrobe();
    public void OnForgeEntry()
    {
        //q te meta en la forja por dentro
        OnForgeInteraction(); //de momento hasta tener modelo de la forja por dentro
    }
    public void OnForgeInteraction() => villageMenuUI.Button_OpenForge();
    public void OnVillageEntry()
    {
        //sales de la forja/tienda/mina a la calle
        //villageMenuUI.Button_OpenMain();

        Location = Locations.village;
    }
    public void OnMineEntry()
    {
        //escena de mina
        int random = Random.Range(0,2);
        if(random == 0)
            SceneManager.LoadScene("3_MiningRoom");
        else
            SceneManager.LoadScene("2_CombatRoom");

    }
    #endregion
}

