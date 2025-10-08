using UnityEngine;

public class GolemDamageableComponent : MonoBehaviour, IDamageableComponent
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

        if(health <= 0)
            Death();
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

    private void Death()
    {
        Destroy(this.gameObject);

        if(LevelManager.instance != null)
        {
            LevelManager.instance.EnemyDead();
        }
    }
}
