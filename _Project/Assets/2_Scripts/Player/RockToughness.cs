using UnityEngine;

public class RockToughness : MonoBehaviour, IDamageableComponent
{
    [SerializeField]int toughness = 2;
    [SerializeField] float numberProbability = 10f;

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

        //PARA UNAI HACER QUE SE AÑADAN AL INVENTARIO EN ESTA FUNCION
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
