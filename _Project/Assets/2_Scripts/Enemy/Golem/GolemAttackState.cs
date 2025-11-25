using UnityEngine;

public class GolemAttackState : IStateComponent, IAttackComponent
{

    const float COOLDOWN = 2.5f;
    float TIME_HITBOX = 0.5f;

    float radiusToAttack = 1f;

    private float timeToAttack = 0f;
    private float timeHitbox = 0f;

    private bool isInCooldown = true;

    int damage = 20;

    IStateMachineComponent mStateMachine;

    IDamageableComponent playerHealth;

    Transform enemyTransform;
    Transform playerTransform;

    Collider attackHitbox;

    Animator animator;

    public GolemAttackState(IStateMachineComponent m, Transform e, IDamageableComponent p, Transform t, Animator a)
    {
        mStateMachine = m;
        enemyTransform = e;
        playerTransform = t;
        playerHealth = p;
        animator = a;
    }

    public void ActiveHitbox()
    {
        attackHitbox.enabled = true;
    }

    public void Attack()
    {
        //animator.SetBool("Atacar", true);
        Debug.Log("ATACO");
        playerHealth.RecieveDamage(damage, 0.5f, 0.1f);
    }

    public void Enter()
    {
        attackHitbox = enemyTransform.GetChild(1).GetComponent<Collider>();
        //Debug.Log("ESTOY ATACANDO");
        //animator.SetBool("Atacar", true);

        Debug.Log("DURACION: " + animator.GetCurrentAnimatorStateInfo(0).length);
        //TIME_HITBOX = animator.GetCurrentAnimatorStateInfo(1).length;

        if (enemyTransform.gameObject.name == "GolemBoss")
        {
            damage = 60;
            radiusToAttack = 5f;
            TIME_HITBOX = 0.5f;
        }
            
    }

    public void Exit()
    {
        animator.SetBool("Atacar", false);
    }

    public void FixedUpdate()
    {        
        if (isInCooldown)
        {
            //animator.SetBool("Atacar", false);
            timeToAttack += Time.fixedDeltaTime;

            if (timeToAttack >= COOLDOWN)
            {
                isInCooldown = false;
                timeToAttack = 0f;

                //mStateMachine.ChangeState(new GolemChaseState(enemyTransform, mStateMachine, animator));
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
            attackHitbox.enabled = false;
            timeHitbox = 0f;
            isInCooldown = true;
        }        
    }

    void IStateComponent.Update()
    {
        if (playerTransform == null) return;
        if (enemyTransform == null) return;

        Vector3 direction = playerTransform.position - enemyTransform.position;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(-direction.z, 0, direction.x).normalized, Vector3.up);
        enemyTransform.rotation = rotation;

        if (!TakePlayerPosition())
            mStateMachine.ChangeState(new GolemChaseState(enemyTransform, mStateMachine, animator));
    }

    private bool IsHitingPlayer()
    {
        Collider[] p = Physics.OverlapBox(attackHitbox.bounds.center, attackHitbox.bounds.extents, attackHitbox.transform.rotation);

        foreach (Collider c in p)
        {
            if (c.CompareTag("Player"))
            {
                return !playerHealth.GetHasBeenDamaged();
            }
        }

        return false;
    }

    private bool TakePlayerPosition()
    {
        Collider[] p = Physics.OverlapSphere(enemyTransform.position, radiusToAttack);

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
