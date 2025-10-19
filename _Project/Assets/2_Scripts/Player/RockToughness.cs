using UnityEngine;

public class RockToughness : MonoBehaviour, IDamageableComponent
{
    [SerializeField]int toughness = 2;
    [SerializeField] float numberProbability = 0.10f;

    int numberOfHits = 0;
    bool hasBeenHit = false;

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
        Destroy(gameObject);

        if (Random.Range(0f, 0.11f) < numberProbability)
            GameData.RunInventory.AddObject(MaterialName.Carbon, Random.Range(1,3));
    }

    public void RecieveDamage(int damage)
    {
        numberOfHits++;
        hasBeenHit = true;
        if (numberOfHits == toughness)
            DestroyRock();
    }

    public void ResetHasBeenDamaged()
    {
        hasBeenHit = false;
    }

    public bool GetHasBeenDamaged()
    {
        return hasBeenHit;
    }
}
