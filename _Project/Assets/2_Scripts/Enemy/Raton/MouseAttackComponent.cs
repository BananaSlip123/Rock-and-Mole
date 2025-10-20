using UnityEditorInternal;
using UnityEngine;

public class MouseAttackComponent : IStateComponent, IAttackComponent
{
    const float COOLDOWN = 2.5f;
    const float TIME_HITBOX = 0.1f;

    private float timeToAttack = 0f;
    private float timeHitbox = 0f;

    private bool isInCooldown = false;

    int damage = 5;

    IStateMachineComponent mStateMachine;

    IDamageableComponent playerHealth;

    Transform enemyTransform;

    public void ActiveHitbox()
    {

    }

    public void Attack()
    {
        playerHealth.RecieveDamage(damage);
    }

    public void Enter()
    {
        //attackHitbox = enemyTransform.GetChild(1).GetComponent<Collider>();
        Debug.Log("ESTOY ATACANDO");
        //animator.SetBool("Atacar", true);
    }

    public void Exit()
    {
        //animator.SetBool("Atacar", false);
    }

    public void FixedUpdate()
    {
        if (isInCooldown)
        {
            timeToAttack += Time.fixedDeltaTime;

            if (timeToAttack >= COOLDOWN)
            {
                isInCooldown = false;
                timeToAttack = 0f;
                //mStateMachine.ChangeState(new GolemChaseState(enemyTransform, mStateMachine, animator));
            }

            return;
        }
        else
        {
            ActiveHitbox();
        }

        /*
        if (attackHitbox.enabled)
        {
            timeHitbox += Time.fixedDeltaTime;

            if (isHitingPlayer())
                Attack();

            if (timeHitbox >= TIME_HITBOX)
            {
                attackHitbox.enabled = false;
                timeHitbox = 0f;
                isInCooldown = true;
                playerHealth.ResetHasBeenDamaged();
            }
        }
        */
    }

    void IStateComponent.Update()
    {

    }
}
