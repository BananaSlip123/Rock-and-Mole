using UnityEngine;

public class RockToughness : MonoBehaviour, IDamageableComponent
{
    [SerializeField]int toughness = 2;
    [SerializeField] float numberProbability = 0.10f;
    [SerializeField] GameObject sparks;
    [SerializeField] MaterialName rockType;

    int numberOfHits = 0;
    bool hasBeenHit = false;

    private void Awake()
    {
        toughness = UnityEngine.Random.Range(2,6);
    }

    private int GetToughness()
    {
        return toughness;
    }

    private int GetHits()
    {
        return numberOfHits;
    }

    private void DestroyRock()
    {
        float random = Random.Range(0f, 1f);

        GameData.MaterialsRock(rockType);

        Destroy(gameObject);       
    }

    public void RecieveDamage(int damage, float duration, float magnitude)
    {
        Debug.Log("ME HAN GOLEPADO");
        numberOfHits++;
        sparks.SetActive(true);
        hasBeenHit = true;
        if (numberOfHits == toughness)
            DestroyRock();
    }

    public void ResetHasBeenDamaged()
    {
        hasBeenHit = false;
        sparks.SetActive(false);
    }

    public bool GetHasBeenDamaged()
    {
        
        return hasBeenHit;
    }
}
