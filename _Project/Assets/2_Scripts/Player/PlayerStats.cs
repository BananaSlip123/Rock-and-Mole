using PickaxeStats;
using PlayerComponents;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour, IPlayerStats
{
    public int roomNumber = 0;
    private int _actualHealth;
    public int actualHealth
    {
        get => _actualHealth;
        private set
        {           
            if (value != _actualHealth)
            {
                
                if (value > health)
                    _actualHealth = health;
                else
                    _actualHealth = value;
                if(playerHealth != null)
                    playerHealth.SetHealth(value);
            }
        }
    }

    private int _health;
    public int health
    {
        get => _health;
        private set
        {
            if (value != health)
            {
                _health = value;
                actualHealth = value;
            }
        }
    }
    private int _damage;
    public int damage
    {
        get => _damage;
        private set
        {
            if (value != damage)
            {
                _damage = value;
                if (playerAttack != null)
                    playerAttack.damage = value;
            }
        }
    }

    private float _attackSpeed;
    public float attackSpeed
    {
        get => _attackSpeed;
        private set
        {
            if (value != attackSpeed)
            {
                _attackSpeed = value;
                if (playerAttack != null)
                    playerAttack.COOLDOWN = value;
            }
        }
    }

    private float _critMultiplier;
    public float critMultiplier 
    { 
        get => _critMultiplier; 
        private set 
        { 
            if(value != critMultiplier)
            {
                _critMultiplier = value;
                if (playerAttack != null)
                    playerAttack.critMultiplier = value;
            }
        } 
    }
    private float _critProbability;
    public float critProbability
    {
        get => _critProbability;
        private set
        {
            if (value != critProbability)
            {
                _critProbability = value;
                if (playerAttack != null)
                    playerAttack.critProbability = value;
            }
        }
    }

    private float _speed;
    public float speed 
    {
        get => _speed;
        private set
        {
            if (value != speed)
            {
                _speed = value;
                if (playerSpeed != null)
                    playerSpeed.speed = value;
            }
        }
    }

    DamageableComponent playerHealth;
    PlayerAttackComponent playerAttack;
    PlayerMovementComponent playerSpeed;

    public PickaxeStatsScripteableObject currentPickaxe
    {
        get => EquipmentManager.CurrentPickaxeData;
    }
    public ClothStatsScripteableObject currentChestCloth
    {
        get => EquipmentManager.CurrentChestClothData;
    }
    public ClothStatsScripteableObject currentHelmet
    {
        get => EquipmentManager.CurrentHelmetData;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        FindComponents();
        //OnEquipmentChange();
    }
    private void OnEnable()
    {
        OnEquipmentChange();
        EquipmentManager.OnEquipmentChange += OnEquipmentChange;
    }
    private void OnDisable()
    {
        EquipmentManager.OnEquipmentChange -= OnEquipmentChange;
    }
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneChange;
    }

    private void OnSceneChange(Scene scene, LoadSceneMode  mode)
    {
        FindComponents();

        if(SceneManager.GetActiveScene().name == "1_VILLAGE_SCENE")
        {
            roomNumber = 0;

            Debug.Log("ESTOY PASANDO LA VIDA CORRECTA:" + health);
            actualHealth = health;
            playerSpeed.speed = speed;
        }
        else
        {
            Debug.Log("ESTOY PASANDO LA VIDA CORRECTA:" + actualHealth);
            playerHealth.SetHealth(actualHealth);
            playerAttack.critMultiplier = critMultiplier;
            playerAttack.critProbability = critProbability;
            playerAttack.damage = damage;
            playerSpeed.speed = speed;
            playerAttack.COOLDOWN = attackSpeed;

            Debug.Log("ESTOY PASANDO LA VIDA CORRECTA?????:" + playerHealth.Health);

            roomNumber += 1;
        }       
    }

    private void FindComponents()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        
        if (go == null)
            return;
        playerHealth = go.GetComponent<DamageableComponent>();
        playerAttack = go.GetComponent<PlayerAttackComponent>();
        playerSpeed = go.GetComponent<PlayerMovementComponent>();
    }

    public void ChangeClothes(ClothStatsScripteableObject newCloth)
    {
        //Debug.Log("Estoy cambiando la ropa: " + newCloth.name);
        foreach(ModifierStats mod in newCloth.modifiers)
        {
            Debug.Log("HOLA SOY: " + mod.stat);
            SetModifier(mod);
        }
    }

    private void SetModifier(ModifierStats mod)
    {
        switch (mod.stat)
        {
            case Stats.health:
                Debug.Log("vida:" + _actualHealth);
                health += (int) mod.value;
                break;
            case Stats.speed:
                speed += speed * mod.value;
                break;
            case Stats.critMultiplier:
                critMultiplier += mod.value;
                break;
            case Stats.critProbability:
                critProbability += mod.value;
                break;
            case Stats.damage:
                damage += (int) mod.value;
                break;
            case Stats.attackSpeed:
                attackSpeed -= attackSpeed * mod.value;
                break;
            default:
                Debug.Log("No reconozco enum");
                break;
        }
    }

    public void ChangePickaxe(PickaxeStatsScripteableObject newPickaxe)
    {     
        damage += newPickaxe.damage;
        critMultiplier += newPickaxe.critMultiplier;
        critProbability += newPickaxe.critProbability;
        attackSpeed -= attackSpeed*newPickaxe.attackSpeed;

        Debug.Log("DAÑO: " + _damage);
    }

    public void OnEquipmentChange()
    {
        ResetStats();
        //Debug.Log("ENUM: " + currentChestCloth.name + " " + currentHelmet.name);
        Debug.Log("Equipamiento Actualizado");
        Debug.Log(currentPickaxe);
        if (currentPickaxe != null)
            ChangePickaxe(currentPickaxe);
        //else throw new Exception("Pico nulo");
        if (currentChestCloth != null)
            ChangeClothes(currentChestCloth);
        //else throw new Exception("Peto nulo");
        if (currentHelmet != null)
            ChangeClothes(currentHelmet);
        //else throw new Exception("Casco nulo");

    }
    public void ChangeSomething(PickaxeStatsScripteableObject newPickaxe)
    {
        ResetStats();

        ChangePickaxe(newPickaxe);
        ChangeClothes(currentChestCloth);
    }
    public void ChangeSomething(ClothStatsScripteableObject newCloth)
    {
        ResetStats();

        ChangePickaxe(currentPickaxe);
        ChangeClothes(newCloth);
    }

    public void ResetStats()
    {
        health = 0;
        damage = 0;
        critMultiplier = 0;
        critProbability = 0;
        speed = 5f;
        attackSpeed = 0.4f;
    }

    public void HealPlayer(int healing)
    {
        actualHealth += healing;
    }
    public void HealPlayer() => actualHealth = health;
}
