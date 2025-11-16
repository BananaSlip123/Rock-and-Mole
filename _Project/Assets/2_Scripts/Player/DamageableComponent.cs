using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageableComponent : MonoBehaviour, IDamageableComponent
{
    private bool hasBeenDamaged = false;

    [SerializeField] private int health = 0;
    public int Health
    {
        get => health;
        private set
        {
            if(value != health)
            {
                health = value;
                OnHealthChange?.Invoke(value);
            }
            
        }
    }

    public Action<int> OnHealthChange;
    public Action OnDeath;

    public void SetHealth(int health)
    {
        Health = health;
    }

    public void RecieveDamage(int damage)
    {
        Health -= damage;

        if(!hasBeenDamaged)
        {
            hasBeenDamaged = true;

            ResetHasBeenDamaged();
        }
        

        if(Health <= 0)
            Death();
        Debug.Log("Me han quitado vida :" + damage + " me queda: "+ Health);
    }

    public bool GetHasBeenDamaged()
    {
        return hasBeenDamaged;
    }

    public void ResetHasBeenDamaged()
    {
        StartCoroutine(InvencivilityTime());
    }

    IEnumerator InvencivilityTime()
    {
        yield return new WaitForSeconds(2f);

        hasBeenDamaged = false;
    }

    private void Death()
    {
        Destroy(this.gameObject);
        OnDeath?.Invoke();
    }
}
