using UnityEngine;

public class GolemAttackState : IStateComponent, IAttackComponent
{

    const float COOLDOWN = 0.5f;
    const float TIME_HITBOX = 0.1f;

    private float timeToAttack = 0f;
    private float timeHitbox = 0f;

    private bool isInCooldown = false;

    int damage = 5;

    IStateMachineComponent mStateMachine;

    IDamageableComponent playerHealth;

    Transform enemyTransform;
    Transform playerTransform;

    Collider attackHitbox;

    public GolemAttackState(IStateMachineComponent m, Transform e, IDamageableComponent p, Transform t)
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
        playerHealth.RecieveDamage(damage);
    }

    public void Enter()
    {
        attackHitbox = enemyTransform.GetComponentInChildren<Collider>();
        Debug.Log("ESTOY ATACANDO");
    }

    public void Exit()
    {

    }

    public void FixedUpdate()
    {
        Quaternion rotation = Quaternion.LookRotation(VectorConverter.VectorConeverter(new Vector3(-playerTransform.position.y, 0, playerTransform.position.x).normalized), Vector3.up);

        enemyTransform.rotation = rotation;
        if (isInCooldown)
        {
            timeToAttack += Time.fixedDeltaTime;

            if (timeToAttack >= COOLDOWN)
            {
                isInCooldown = false;
                timeToAttack = 0f;

                mStateMachine.ChangeState(new GolemChaseState(enemyTransform, mStateMachine));
            }

            return;
        }
        else
        {
            ActiveHitbox();
        }           

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
    }

    void IStateComponent.Update()
    {

    }

    private bool isHitingPlayer()
    {
        Collider[] p = Physics.OverlapBox(enemyTransform.position, new Vector3(2.5f,2.5f,2.5f), Quaternion.identity);

        foreach (Collider c in p)
        {
            if (c.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }

    private bool TakePlayerPosition()
    {
        Collider[] p = Physics.OverlapSphere(enemyTransform.position, 0.2f);

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
