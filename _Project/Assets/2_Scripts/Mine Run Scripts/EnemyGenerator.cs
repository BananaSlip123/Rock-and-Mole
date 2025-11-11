using System;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour, IEnemyGenerator
{
    [SerializeField] GameObject[] enemiesPrefabs;
    [SerializeField] Vector2Int minMaxEnemies;

    void Start()
    {
        float random = UnityEngine.Random.Range(minMaxEnemies.x, minMaxEnemies.y);

        for (int i = 0; i < random; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, enemiesPrefabs.Length);
            SpawnEnemy(enemiesPrefabs[randomIndex]);
            LevelManager.instance.EnemyHasSpawned();
        }

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
