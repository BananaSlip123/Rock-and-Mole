using PickaxeStats;
using PlayerComponents;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IPlayerStats
{
    public int actualHealth
    {
        get => 1;
        private set
        {
            if (value != actualHealth)
            {
                if (value > health)
                    actualHealth = health;
                else
                    actualHealth = value;
                playerHealth.SetHealth(value);
            }
        }
    }
    public int health
    {
        get => 50;
        private set
        {
            if (value != health)
            {
                health = value;
                actualHealth = value;
            }
        }
    }
    public int damage
    {
        get => damage;
        private set
        {
            if (value != damage)
            {
                damage = value;
                playerAttack.damage = value;
            }
        }
    }
    public float critMultiplier 
    { 
        get => critMultiplier; 
        private set 
        { 
            if(value != critMultiplier)
            {
                critMultiplier = value;
                playerAttack.critMultiplier = value;
            }
        } 
    }
    public float critProbability
    {
        get => critProbability;
        private set
        {
            if (value != critProbability)
            {
                critProbability = value;
                playerAttack.critProbability = value;
            }
        }
    }
    public float speed 
    {
        get => speed;
        private set
        {
            if (value != speed)
            {
                speed = value;
                playerSpeed.speed = value;
            }
        }
    }
    public float defense 
    {
        get => defense;
        private set
        {
            if (value != defense)
            {
                defense = value;
                playerHealth.defense = value;
            }
        }
    }

    DamageableComponent playerHealth;
    PlayerAttackComponent playerAttack;
    PlayerMovementComponent playerSpeed;

    private void Awake()
    {
        playerHealth = GetComponent<DamageableComponent>();
        playerAttack = GetComponent<PlayerAttackComponent>();
        playerSpeed = GetComponent<PlayerMovementComponent>();
    }

    public void ChangeClothes()
    {
        throw new System.NotImplementedException();
    }

    public void ChangePickaxe(PickaxeStatsScripteableObject newPickaxe)
    {
        damage = newPickaxe.damage;
        critMultiplier = newPickaxe.critMultiplier;
        critProbability = newPickaxe.critProbability;
    }

    public void ResetStats()
    {
        health = 50;
        damage = 0;
        critMultiplier = 0;
        critProbability = 0;
        speed = 5f;
        defense = 0f;
    }

    public void HealPlayer(int healing)
    {
        actualHealth += healing;
    }
}
