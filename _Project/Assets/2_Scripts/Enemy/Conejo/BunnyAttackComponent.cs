using System.Collections;
using UnityEditor;
using UnityEngine;

public class BunnyAttackComponent : IStateComponent, IAttackComponent
{

    const float COOLDOWN = 2.5f;

    private float timeToAttack = 0f;

    private bool isInCooldown = true;
    private bool hasAttacked = false;

    int damage = 20;

    IStateMachineComponent mStateMachine;

    IDamageableComponent playerHealth;

    Transform enemyTransform;
    Transform playerTransform;

    GameObject explosion;

    Collider attackHitbox;

    Animator animator;

    public BunnyAttackComponent(IStateMachineComponent m, Transform e, IDamageableComponent p, Transform t, Animator a, GameObject c)
    {
        mStateMachine = m;
        enemyTransform = e;
        playerTransform = t;
        playerHealth = p;
        animator = a;

        explosion = c;
    }

    public BunnyAttackComponent(IStateMachineComponent m, Transform e, IDamageableComponent p, Transform t, GameObject c)
    {
        mStateMachine = m;
        enemyTransform = e;
        playerTransform = t;
        playerHealth = p;

        explosion = c;
    }

    public void ActiveHitbox()
    {
        attackHitbox.enabled = true;
    }

    public void Attack()
    {
        //animator.SetBool("Atacar", true);
        playerHealth.RecieveDamage(damage);
        hasAttacked = true;

        BunnyController m = (BunnyController)mStateMachine;
        m.GenerateGameObject(enemyTransform);

        enemyTransform.GetComponent<BunnyDamageableComponent>().Exploded();
        m.DestroyGameObject(enemyTransform.gameObject);
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

            if (!isInCooldown && !hasAttacked)
                Attack();
        }
        else
        {
            mStateMachine.ChangeState(new BunnyChaseState(enemyTransform, mStateMachine, explosion));
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
