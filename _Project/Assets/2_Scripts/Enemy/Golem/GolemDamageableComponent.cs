using UnityEngine;

public class GolemDamageableComponent : MonoBehaviour, IDamageableComponent
{
    private bool hasBeenDamaged = false;

    [SerializeField] private int health = 50;
    [SerializeField] Animator animator;
    private float timeToDeath = 0f;
    const float TIME_TO_DEATH = 1f;

    private void FixedUpdate()
    {
        if(animator.GetBool("Morir"))
        {
            timeToDeath += Time.fixedDeltaTime;

            if(timeToDeath >= TIME_TO_DEATH)
                DeathLogic();
        }
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
        animator.SetBool("Morir", true);        
    }

    private void DeathLogic()
    {
        Destroy(this.gameObject);

        if (LevelManager.instance != null)
        {
            LevelManager.instance.EnemyDead();
            GameData.RunInventory.AddObject(MaterialName.Hierro, Random.Range(1, 4));
        }
    }
}
