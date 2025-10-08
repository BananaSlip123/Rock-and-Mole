using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject[] rooms;

    [SerializeField] int nEnemies = 0;

    [SerializeField] bool spawnEnemy = true;

    public static LevelManager instance;

    INoMoreEnemies doorsManagementEnemies;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        doorsManagementEnemies = GetComponent<INoMoreEnemies>();

        Instantiate(rooms[0]);
    }

    private void Start()
    {
        if(!spawnEnemy)
            doorsManagementEnemies.ThereIsNoEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        if (nEnemies <= 0)
        {
            doorsManagementEnemies.ThereIsNoEnemies();
        }
    }

    public void EnemyDead()
    {
        nEnemies -= 1;

        if(nEnemies <= 0)
        {
            doorsManagementEnemies.ThereIsNoEnemies();
        }
    }

    public void EnemyHasSpawned()
    {
        nEnemies += 1;
    }
}
