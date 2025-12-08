using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject[] rooms;

    [SerializeField] int nEnemies = 0;

    [SerializeField] bool spawnEnemy = true;

    public static LevelManager instance;

    public Action onRoomCleaned;

    INoMoreEnemies doorsManagementEnemies;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        doorsManagementEnemies = GetComponent<INoMoreEnemies>();

        if (rooms.Length != 0)
        {
            switch(BiomeManager.CurrentBiome)
            {
                case BiomeName.starterMines:
                    Instantiate(rooms[UnityEngine.Random.Range(0, 2)]);
                    break;
                case BiomeName.undergroundForest:
                    Instantiate(rooms[UnityEngine.Random.Range(2, rooms.Length)]);
                    break;
            }          
        }
    }

    private void Start()
    {
        if (!spawnEnemy) ThereIsNoEnemies();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void ThereIsNoEnemies()
    {
        doorsManagementEnemies.ThereIsNoEnemies();
        onRoomCleaned?.Invoke();
    }

    public void EnemyDead()
    {
        nEnemies -= 1;

        if (nEnemies <= 0)
        {
            ThereIsNoEnemies();
        }
    }

    public void EnemyHasSpawned()
    {
        nEnemies += 1;
    }
}
