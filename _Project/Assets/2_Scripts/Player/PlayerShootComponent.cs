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
            onIsShootingChange?.Invoke(value);
            throwingPickaxeComponent?.gameObject.SetActive(value);
        }
    }

    [SerializeField] ThrowingPickaxe throwingPickaxeComponent;
    [SerializeField] Transform t_pickaxeSpawnTransform;

    public int damage;
    public float critMultiplier;
    public float critProbability;

    public Action<bool> onIsShootingChange;

    private void Awake()
    {
        throwingPickaxeComponent.ResetValues();
        throwingPickaxeComponent.player = transform;
        throwingPickaxeComponent.shoot = this;
        throwingPickaxeComponent.gameObject.transform.parent = null;

        

        IsShooting = false;
    }

    private void Start()
    {
        AssignStats();
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
        throwingPickaxeComponent.ResetValues();
        throwingPickaxeComponent.gameObject.transform.SetPositionAndRotation(t_pickaxeSpawnTransform.position, t_pickaxeSpawnTransform.rotation);
    }

    void AssignStats()
    {       
        throwingPickaxeComponent.damage = damage;
        throwingPickaxeComponent.critMultiplier = critMultiplier;
        throwingPickaxeComponent.critProbability = critProbability;

        Debug.Log("Asignacion stats: " + throwingPickaxeComponent.damage + " " + damage);
    }
}
