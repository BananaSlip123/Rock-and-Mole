using Codice.CM.Common;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class BunnyAttackComponent : IStateComponent, IAttackComponent
{

    const float COOLDOWN = 2.5f;

    private float timeToAttack = 0f;

    private bool isInCooldown = true;
    private bool hasAttacked = false;

    int damage = 40;

    IStateMachineComponent mStateMachine;

    IDamageableComponent playerHealth;

    Transform enemyTransform;
    Transform playerTransform;

    Collider attackHitbox;

    Animator animator;

    MaterialChanger changer;

    public BunnyAttackComponent(IStateMachineComponent m, Transform e, IDamageableComponent p, Transform t, Animator a)
    {
        mStateMachine = m;
        enemyTransform = e;
        playerTransform = t;
        playerHealth = p;
        animator = a;
    }

    public BunnyAttackComponent(IStateMachineComponent m, Transform e, IDamageableComponent p, Transform t, GameObject c)
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
        playerHealth.RecieveDamage(damage, 1f, 0.75f);
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
        animator.SetBool("Morir", true);

        changer = enemyTransform.GetComponent<MaterialChanger>();
        //Debug.Log("DURACION: " + animator.GetCurrentAnimatorStateInfo(0).length);
        //TIME_HITBOX = animator.GetCurrentAnimatorStateInfo(0).length;
    }

    public void Exit()
    {
        animator.SetBool("Morir", false);
        changer.StopAllCoroutines();
        changer.AssignDefaultMat();
        enemyTransform.GetChild(2).localScale = Vector3.one;
    }

    public void FixedUpdate()
    {
        if (IsHitingPlayer())
        {
            //animator.SetBool("Morir", false);
            timeToAttack += Time.fixedDeltaTime;

            Material material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            material.color = new Color(1, 0.5f + Mathf.PingPong(Time.time + 4f * timeToAttack, 0.5f), 0);

            changer.AssignTemporalMaterial(material);

            enemyTransform.GetChild(2).localScale = Vector3.one * (0.75f + Mathf.PingPong(Time.time + 4f * timeToAttack, 0.5f));

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
            //mStateMachine.ChangeState(new BunnyChaseState(enemyTransform, mStateMachine, explosion));
            mStateMachine.ChangeState(new BunnyChaseState(enemyTransform, mStateMachine, animator));
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
