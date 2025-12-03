using PlayerComponents;
using System;
using UnityEngine;

public class PlayerShootComponent : MonoBehaviour, IAttackComponent
{
    public bool _isShooting;
    public bool IsShooting{
        get => _isShooting;
        set
        {
            _isShooting = value;
            onIsShootingChange.Invoke(value);
            throwingPickaxeComponent?.gameObject.SetActive(value);
        }
    }

    [SerializeField] ThrowingPickaxe throwingPickaxeComponent;

    public int damage;
    public float critMultiplier;
    public float critProbability;

    public Action<bool> onIsShootingChange;

    private void Awake()
    {
        throwingPickaxeComponent.player = transform;
        throwingPickaxeComponent.shoot = this;
        AssignStats();

        IsShooting = false;
    }

    public void ActiveHitbox()
    {
        throw new System.NotImplementedException();
    }

    public void Attack()
    {
        IsShooting = true;
        Vector2 dir = GetComponent<PlayerMovementComponent>().directionRotation;
        throwingPickaxeComponent.dir = new Vector3(dir.x,0f,dir.y);    
    }

    void AssignStats()
    {
        throwingPickaxeComponent.damage = damage;
        throwingPickaxeComponent.critMultiplier = critMultiplier;
        throwingPickaxeComponent.critProbability = critProbability;
    }
}
