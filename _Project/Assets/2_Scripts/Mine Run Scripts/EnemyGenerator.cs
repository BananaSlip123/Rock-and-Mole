using System;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour, IEnemyGenerator
{
    [SerializeField] GameObject[] enemiesPrefabs;
    [SerializeField] Vector2Int limitX;
    [SerializeField] Vector2Int limitZ;
    [SerializeField] Vector2Int minMaxEnemies;

    void Start()
    {
        float random = UnityEngine.Random.Range(minMaxEnemies.x, minMaxEnemies.y);

        for (int i = 0; i < random; i++)
        {           
            SpawnEnemy(enemiesPrefabs[0]);
            LevelManager.instance.EnemyHasSpawned();
        }
    }

    public void SpawnEnemy(GameObject prefab)
    {
        float randomZ = UnityEngine.Random.Range(limitZ.x, limitZ.y);
        float randomX = UnityEngine.Random.Range(limitX.x, limitX.y);

        Instantiate(prefab, new Vector3(randomX, 16, randomZ), Quaternion.identity);
    }
}
