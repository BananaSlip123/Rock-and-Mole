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
        if (random < 0.25f)
            GameData.MaterialsRock(MaterialName.Carbon);
        else if (random < 0.5f)
            GameData.MaterialsRock(MaterialName.Hierro);
        else if (random < 0.65f)
            GameData.MaterialsRock(MaterialName.Bronce);
        else if (random < 0.8f)
            GameData.MaterialsRock(MaterialName.Cuarzo);
        else if (random < 0.9f)
            GameData.MaterialsRock(MaterialName.Rubi);
        else if (random < 0.95f)
            GameData.MaterialsRock(MaterialName.Diamante);
        else
            GameData.MaterialsRock(MaterialName.Obsidiana);

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
