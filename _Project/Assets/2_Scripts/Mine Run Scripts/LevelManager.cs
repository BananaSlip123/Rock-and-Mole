using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject[] rooms;

    [SerializeField] int nEnemies = 0;

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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
