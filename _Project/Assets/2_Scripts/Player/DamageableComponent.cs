using Codice.Client.Common.GameUI.Checkin;
using System;
using UnityEngine;

public class DamageableComponent : MonoBehaviour, IDamageableComponent
{
    private bool hasBeenDamaged = false;

    [SerializeField] private int health = 50;

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
    private void FixedUpdate()
    {
        
    }

    public void RecieveDamage(int damage)
    {
        Health -= damage;
        hasBeenDamaged = true;

        if(Health <= 0)
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
        //GameData.RunInventory.
    }
}
