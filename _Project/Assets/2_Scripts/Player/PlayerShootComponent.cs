using PlayerComponents;
using UnityEngine;

public class PlayerShootComponent : MonoBehaviour, IAttackComponent
{
    public bool isShooting = false;

    [SerializeField]GameObject pickaxe;
    ThrowingPickaxe throwing;

    public int damage;
    public float critMultiplier;
    public float critProbability;

    private void Awake()
    {
        throwing = pickaxe.GetComponent<ThrowingPickaxe>();
        throwing.player = transform;
        throwing.shoot = this;
        AssignStats();
    }

    public void ActiveHitbox()
    {
        throw new System.NotImplementedException();
    }

    public void Attack()
    {
        isShooting = true;
        Vector2 dir = GetComponent<PlayerMovementComponent>().directionRotation;
        throwing.dir = new Vector3(dir.x,0f,dir.y);
        Instantiate(pickaxe, pickaxe.transform.position, pickaxe.transform.rotation).SetActive(true);      
    }

    void AssignStats()
    {
        throwing.damage = damage;
        throwing.critMultiplier = critMultiplier;
        throwing.critProbability = critProbability;
    }
}
