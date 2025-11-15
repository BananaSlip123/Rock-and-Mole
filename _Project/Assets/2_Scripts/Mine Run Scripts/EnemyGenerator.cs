using System;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour, IEnemyGenerator
{
    [SerializeField] GameObject[] enemiesPrefabs;
    [SerializeField] GameObject pool;
    [SerializeField] Vector2Int minMaxEnemies;

    static bool firstTime = true;

    void Start()
    {
        float random = UnityEngine.Random.Range(minMaxEnemies.x, minMaxEnemies.y);

        for (int i = 0; i < random; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, enemiesPrefabs.Length);
            if(firstTime && randomIndex == 2)
            {
                firstTime = false;
                Instantiate(pool);
            }
                
            SpawnEnemy(enemiesPrefabs[randomIndex]);
            LevelManager.instance.EnemyHasSpawned();
        }

        firstTime = true;
        gameObject.SetActive(false);
    }

    public void SpawnEnemy(GameObject prefab)
    {
        Collider collider = GetComponent<Collider>();

        float randomX = UnityEngine.Random.Range(collider.bounds.min.x, collider.bounds.max.x);
        float randomZ = UnityEngine.Random.Range(collider.bounds.min.z, collider.bounds.max.z);

        Instantiate(prefab, new Vector3(randomX, transform.position.y, randomZ), Quaternion.identity);
    }
}
