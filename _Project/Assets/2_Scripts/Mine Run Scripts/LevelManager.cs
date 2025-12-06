using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject[] rooms;

    [SerializeField] int nEnemies = 0;

    [SerializeField] bool spawnEnemy = true;

    public static LevelManager instance;

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
            Instantiate(rooms[0]);
        }
    }

    private void Start()
    {
        if (!spawnEnemy)
            doorsManagementEnemies.ThereIsNoEnemies();
    }

    public void EnemyDead()
    {
        nEnemies -= 1;

        if (nEnemies <= 0)
        {
            doorsManagementEnemies.ThereIsNoEnemies();

            // CAMBIO DE MÚSICA AL TERMINAR EL COMBATE
            //AudioManager.Instance?.PlayMusic(AudioManager.MusicType.MineMusic);
        }
    }

    public void EnemyHasSpawned()
    {
        nEnemies += 1;
    }
}
