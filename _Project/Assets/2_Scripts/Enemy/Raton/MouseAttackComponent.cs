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

    IObjectPool pool;

    Transform enemyTransform;
    Transform playerPosition;

    public MouseAttackComponent(Transform enemy, Transform player, IStateMachineComponent stateM)
    {
        enemyTransform = enemy;
        playerPosition = player;
        mStateMachine = stateM;
    }

    public void ActiveHitbox()
    {

    }

    public void Attack()
    {
        Debug.Log("He atacado");
        pool.Get().Init(playerPosition.position - enemyTransform.position, enemyTransform.position);
    }

    public void Enter()
    {
        //attackHitbox = enemyTransform.GetChild(1).GetComponent<Collider>();
        //Debug.Log("ESTOY ATACANDO");
        //animator.SetBool("Atacar", true);

        pool = GameObject.FindGameObjectWithTag("PoolBullet").GetComponent<IObjectPool>();
    }

    public void Exit()
    {
        //animator.SetBool("Atacar", false);
    }

    public void FixedUpdate()
    {
        if (!PlayerInRange())
            mStateMachine.ChangeState(new MouseWanderState(mStateMachine, enemyTransform));

        if (isInCooldown)
        {
            timeToAttack += Time.fixedDeltaTime;

            if (timeToAttack >= COOLDOWN)
            {
                isInCooldown = false;
                timeToAttack = 0f;
                
            }

            return;
        }
    }

    void IStateComponent.Update()
    {
        if(!isInCooldown)
        {
            Attack();

            isInCooldown = true;
            timeToAttack = 0f;
        }
    }

    bool PlayerInRange()
    {
        Collider[] p = Physics.OverlapSphere(enemyTransform.position, 10f);

        foreach (Collider detected in p)
        {
            if (detected.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }
}
