using UnityEngine;

class PlantBiteState : IStateComponent, IAttackComponent
{
    private IStateMachineComponent plantController;
    private Transform enemy;
    private Transform player;
    private Animator animator;

    private Collider hitbox;

    IDamageableComponent playerHealth;

    int damage = 30;

    private float timeToAttack = 0f;
    private float timeHitbox = 0f;
    private float radius = 3f;

    private bool isInCooldown = true;

    const float COOLDOWN = 2.5f;
    float TIME_HITBOX = 0.5f;

    public PlantBiteState(IStateMachineComponent plantController, Transform enemy, Transform player, Animator animator)
    {
        this.plantController = plantController;
        this.enemy = enemy;
        this.player = player;
        this.animator = animator;
    }

    public void ActiveHitbox()
    {
        hitbox.enabled = true;
    }

    public void Attack()
    {
        playerHealth.RecieveDamage(damage, 0.5f, 0.1f);
    }

    public void Enter()
    {
        hitbox = enemy.GetChild(1).GetComponent<Collider>();
        playerHealth = player.GetComponent<IDamageableComponent>();
        animator.SetBool("Morder", true);

        if (enemy.GetComponent<GolemDamageableComponent>().tipoEnemigo == EnemyName.PlantBoss)
            radius = 5f;
    }

    public void Exit()
    {
        animator.SetBool("Morder", false);
    }

    public void FixedUpdate()
    {
        if (player == null) return;
        if (enemy == null) return;

        if (isInCooldown)
        {
            timeToAttack += Time.fixedDeltaTime;

            if (timeToAttack >= COOLDOWN)
            {
                isInCooldown = false;
                timeToAttack = 0f;
            }
        }
        else
        {
            timeHitbox += Time.fixedDeltaTime;

            animator.SetBool("Atacar", true);

            if (timeHitbox >= 0.4f)
            {
                ActiveHitbox();
                if (IsHitingPlayer())
                    Attack();
            }
        }

        if (timeHitbox >= TIME_HITBOX)
        {
            animator.SetBool("Atacar", false);
            hitbox.enabled = false;
            timeHitbox = 0f;
            isInCooldown = true;
        }
    }

    public void Update()
    {
        if (player == null) return;
        if (enemy == null) return;

        Vector3 direction = player.position - enemy.position;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(-direction.z, 0, direction.x).normalized, Vector3.up);
        enemy.rotation = rotation;       

        if (!TakePlayerPosition(radius))
            plantController.ChangeState(new PlantShootState(plantController, enemy, player, animator));
        else if(!TakePlayerPosition(radius * 3.33f))
            plantController.ChangeState(new PlantLookingState(plantController, enemy, animator));
    }

    private bool IsHitingPlayer()
    {
        Collider[] p = Physics.OverlapBox(hitbox.bounds.center, hitbox.bounds.extents, hitbox.transform.rotation);

        foreach (Collider c in p)
        {
            if (c.CompareTag("Player"))
            {
                return !playerHealth.GetHasBeenDamaged();
            }
        }

        return false;
    }

    private bool TakePlayerPosition(float radius)
    {
        Collider[] p = Physics.OverlapSphere(enemy.position, radius);

        foreach (Collider c in p)
        {
            if (c.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }
}