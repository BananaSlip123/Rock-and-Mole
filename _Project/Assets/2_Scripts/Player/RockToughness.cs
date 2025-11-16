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
        float random = Random.Range(0f, 1f);
        if ( random < 0.25f)
            GameData.RunInventory.AddObject(MaterialName.Carbon, Random.Range(1, 3));
        else if(random < 0.5f)
            GameData.RunInventory.AddObject(MaterialName.Hierro, Random.Range(1, 3));
        else if (random < 0.65f)
            GameData.RunInventory.AddObject(MaterialName.Bronce, Random.Range(1, 3));
        else if (random < 0.8f)
            GameData.RunInventory.AddObject(MaterialName.Cuarzo, Random.Range(1, 3));
        else if (random < 0.9f)
            GameData.RunInventory.AddObject(MaterialName.Rubi, Random.Range(1, 3));
        else if (random < 0.95f)
            GameData.RunInventory.AddObject(MaterialName.Diamante, 1);
        else
            GameData.RunInventory.AddObject(MaterialName.Obsidiana, 1);

        Destroy(gameObject);       
    }

    public void RecieveDamage(int damage)
    {
        Debug.Log("ME HAN GOLEPADO");
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
