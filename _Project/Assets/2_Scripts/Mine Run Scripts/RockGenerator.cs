using UnityEngine;

public class RockGenerator : MonoBehaviour, IEnemyGenerator
{
    [SerializeField] GameObject[] rockPrefabs;

    [SerializeField] Vector2Int minMaxRock;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int random = UnityEngine.Random.Range(minMaxRock.x, minMaxRock.y + 1);

        for (int i = 0; i < random; i++)
        {
            int randomType = UnityEngine.Random.Range(0,rockPrefabs.Length);
            Debug.Log("INDEX: " + randomType);
            SpawnEnemy(rockPrefabs[randomType]);
        }

        gameObject.SetActive(false);
    }

    public void SpawnEnemy(GameObject prefab)
    {
        Collider collider = GetComponent<Collider>();

        float randomX = UnityEngine.Random.Range(collider.bounds.min.x,collider.bounds.max.x);
        float randomZ = UnityEngine.Random.Range(collider.bounds.min.z, collider.bounds.max.z);

        Quaternion random = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f),0f);

        Instantiate(prefab, new Vector3(randomX, transform.position.y, randomZ), random);
    }
}
