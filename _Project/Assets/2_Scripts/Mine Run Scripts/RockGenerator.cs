using UnityEngine;

public class RockGenerator : MonoBehaviour, IEnemyGenerator
{
    [SerializeField] GameObject[] rockPrefabs;

    [SerializeField] Vector2Int limitX;
    [SerializeField] Vector2Int limitZ;
    [SerializeField] Vector2Int minMaxRock;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float random = UnityEngine.Random.Range(minMaxRock.x, minMaxRock.y);

        for (int i = 0; i < random; i++)
        {
            SpawnEnemy(rockPrefabs[0]);
            LevelManager.instance.EnemyHasSpawned();
        }

        gameObject.SetActive(false);
    }

    public void SpawnEnemy(GameObject prefab)
    {
        Collider collider = GetComponent<Collider>();
        //float randomZ = UnityEngine.Random.Range(limitZ.x, limitZ.y);
        //float randomX = UnityEngine.Random.Range(limitX.x, limitX.y);

        float randomX = UnityEngine.Random.Range(collider.bounds.min.x,collider.bounds.max.x);
        float randomZ = UnityEngine.Random.Range(collider.bounds.min.z, collider.bounds.max.z);

        Instantiate(prefab, new Vector3(randomX, transform.position.y, randomZ), Quaternion.identity);
    }
}
