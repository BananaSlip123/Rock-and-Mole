using UnityEngine;
using UnityEngine.SceneManagement;

public class VillageNavigation : MonoBehaviour
{
    public static VillageNavigation Instance { get; private set; }

    [SerializeField] VillageMenuUI villageMenuUI;
    [SerializeField] GameObject go_village;
    [SerializeField] GameObject go_shop;
    [SerializeField] GameObject go_forge;

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
            return;
        }

        Location = Locations.village;
    }

    // ======================
    //   PUBLIC FUNCTIONS
    // ======================

    public void OnShopEntry()
    {
        Location = Locations.shop;
    }

    public void OnSellInteraction() => villageMenuUI.Button_OpenShop();

    public void OnWardrobeInteraction() => villageMenuUI.Button_OpenWardrobe();

    public void OnForgeEntry()
    {
        Location = Locations.forge;
    }

    public void OnForgeInteraction() => villageMenuUI.Button_OpenForge();

    public void OnVillageEntry()
    {
        Location = Locations.village;
    }

    public void OnMineEntry()
    {
        // Escena de mina
        int random = Random.Range(0, 2);

        if (random == 0)
        {
            SceneManager.LoadScene("3_MiningRoom");
            AudioManager.Instance.PlayMusic(AudioManager.MusicType.MineMusic);
        }
        else
        {
            SceneManager.LoadScene("2_CombatRoom");
            AudioManager.Instance.PlayMusic(AudioManager.MusicType.EnemyFightMusic);

            // ACCESO AL nEnemies DEL LEVELMANAGER
            if (LevelManager.Instance != null &&
                LevelManager.Instance.nEnemies <= 0)
            {
                // Cuando no hay enemigos, abre las puertas
                LevelManager.Instance
                    .GetComponent<INoMoreEnemies>()
                    ?.ThereIsNoEnemies();
            }
        }
    }
}
