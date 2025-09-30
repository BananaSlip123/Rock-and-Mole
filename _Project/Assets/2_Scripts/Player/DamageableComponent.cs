using UnityEngine;

public class DamageableComponent : MonoBehaviour, IDamageableComponent
{
    private bool hasBeenDamaged = false;

    [SerializeField] private int health = 50;

    private void FixedUpdate()
    {
        
    }

    public void RecieveDamage(int damage)
    {
        health -= damage;
        hasBeenDamaged = true;

        Debug.Log("Me han quitado vida");
    }

    public bool GetHasBeenDamaged()
    {
        return hasBeenDamaged;
    }

    public void ResetHasBeenDamaged()
    {
        hasBeenDamaged = false;
        Debug.Log("He salido del area");
    }
}
