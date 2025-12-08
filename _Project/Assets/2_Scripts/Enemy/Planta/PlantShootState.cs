using UnityEngine;

internal class PlantShootState : IStateComponent, IAttackComponent
{
    private IStateMachineComponent plantController;
    IObjectPool pool;

    private Transform enemy;
    private Transform player;
    private Animator animator;

    private bool isInCooldown = true;

    const float COOLDOWN = 1.5f;
    private float timeToAttack = 0f;

    public PlantShootState(IStateMachineComponent stateMachineComponent, Transform transform, Transform player, Animator animator)
    {
        this.plantController = stateMachineComponent;
        this.enemy = transform;
        this.player = player;
        this.animator = animator;
    }

    public void Enter()
    {
        pool = GameObject.FindGameObjectWithTag("PoolBullet").GetComponent<IObjectPool>();
        animator.SetBool("Dispara", true);
    }

    public void Exit()
    {
        animator.SetBool("Dispara", false);
    }

    public void FixedUpdate()
    {
        if (player == null) return;
        if (enemy == null) return;

        if (PlayerInRange(3f))
        {
            plantController.ChangeState(new PlantBiteState(plantController, enemy,player, animator));
        }
        else if (!PlayerInRange(10f))
        {
            plantController.ChangeState(new PlantLookingState(plantController, enemy, animator));
        }

        if (isInCooldown)
        {
            timeToAttack += Time.fixedDeltaTime;
            
            if(timeToAttack == 1f)
                animator.SetBool("Dispara", false);

            if (timeToAttack >= COOLDOWN)
            {
                isInCooldown = false;
                timeToAttack = 0f;
                
            }

            return;
        }
    }

    public void Update()
    {
        if (player == null) return;
        if (enemy == null) return;

        Vector3 direction = player.position - enemy.position;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(-direction.z, 0, direction.x).normalized, Vector3.up);
        enemy.rotation = rotation;

        if (!isInCooldown)
        {
            Attack();

            isInCooldown = true;
            timeToAttack = 0f;
        }
    }

    public bool PlayerInRange(float radius)
    {
        Collider[] p = Physics.OverlapSphere(enemy.transform.position, radius);

        foreach (Collider c in p)
        {
            if (c.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }

    public void Attack()
    {
        animator.SetBool("Dispara", true);
        float suma = 2.3f;
        if(enemy.gameObject.name == "PlantaBoss")
        {
            suma = 4f;
        }
        Vector3 posBala = new Vector3(enemy.position.x, enemy.position.y + suma, enemy.position.z);
        pool.Get().Init(player.position - posBala, posBala);
    }

    public void ActiveHitbox()
    {
        throw new System.NotImplementedException();
    }
}