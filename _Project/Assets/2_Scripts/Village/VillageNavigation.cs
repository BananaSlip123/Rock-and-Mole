using UnityEngine;
using UnityEngine.SceneManagement;

public class VillageNavigation : MonoBehaviour
{
    public static VillageNavigation Instance { get; private set; }

    [SerializeField] VillageMenuUI villageMenuUI;
    [SerializeField] GameObject go_village;
    [SerializeField] GameObject go_shop;
    [SerializeField] GameObject go_forge;

    //[SerializeField] GameObject go_forge;

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
            go_forge.SetActive(value == Locations.forge);
            go_shop.SetActive(value == Locations.shop);

            switch (value)
            {
                case Locations.shop:
                        AudioManager.Instance?.PlayMusic(AudioManager.MusicType.StoreMusic);
                    break;
                case Locations.village:
                    AudioManager.Instance?.PlayMusic(AudioManager.MusicType.TownMusic);
                    break;
                case Locations.forge:
                    AudioManager.Instance?.PlayMusic(AudioManager.MusicType.StoreMusic);
                    break;
            }
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
        Location = Locations.forge;
    }
    public void OnForgeInteraction() => villageMenuUI.Button_OpenForge();
    public void OnVillageEntry()
    {
        Location = Locations.village;
    }
    public void OnMineEntry()
    {
        //SceneManager.LoadScene("2_CombatRoom");
        //SceneManager.LoadScene("5_RescueRoom");
        SceneManager.LoadScene("7_DarkRoom");
    }
    #endregion
}

