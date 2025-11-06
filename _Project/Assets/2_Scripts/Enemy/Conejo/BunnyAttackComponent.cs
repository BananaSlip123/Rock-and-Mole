using UnityEditor;
using UnityEngine;

public class BunnyAttackComponent : IStateComponent, IAttackComponent
{

    const float COOLDOWN = 2.5f;
    float TIME_HITBOX = 0.1f;

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

    public BunnyAttackComponent(IStateMachineComponent m, Transform e, IDamageableComponent p, Transform t, Animator a)
    {
        mStateMachine = m;
        enemyTransform = e;
        playerTransform = t;
        playerHealth = p;
        animator = a;
    }

    public BunnyAttackComponent(IStateMachineComponent m, Transform e, IDamageableComponent p, Transform t)
    {
        mStateMachine = m;
        enemyTransform = e;
        playerTransform = t;
        playerHealth = p;
    }

    public void ActiveHitbox()
    {
        attackHitbox.enabled = true;
    }

    public void Attack()
    {
        //animator.SetBool("Atacar", true);
        playerHealth.RecieveDamage(damage);
        GameObject.Destroy(enemyTransform.gameObject);
    }

    public void Enter()
    {
        attackHitbox = enemyTransform.GetChild(0).GetComponent<Collider>();
        ActiveHitbox();
        //Debug.Log("ESTOY ATACANDO");
        //animator.SetBool("Atacar", true);

        //Debug.Log("DURACION: " + animator.GetCurrentAnimatorStateInfo(0).length);
        //TIME_HITBOX = animator.GetCurrentAnimatorStateInfo(0).length;
    }

    public void Exit()
    {
        //animator.SetBool("Atacar", false);
    }

    public void FixedUpdate()
    {
        if (IsHitingPlayer())
        {
            //animator.SetBool("Atacar", false);
            timeToAttack += Time.fixedDeltaTime;

            if (timeToAttack >= COOLDOWN)
            {
                isInCooldown = false;
                timeToAttack = 0f;
            }

            if (!isInCooldown)
                Attack();
        }
        else
        {
            mStateMachine.ChangeState(new BunnyChaseState(enemyTransform, mStateMachine));
            //mStateMachine.ChangeState(new BunnyChaseState(enemyTransform, mStateMachine, animator));
        }
    }

    void IStateComponent.Update()
    {
    }

    private bool IsHitingPlayer()
    {
        Collider[] p = Physics.OverlapBox(attackHitbox.bounds.center, attackHitbox.bounds.extents, attackHitbox.transform.rotation);

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
