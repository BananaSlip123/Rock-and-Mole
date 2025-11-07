using UnityEngine;

public class BunnyChaseState : IStateComponent, IMoveComponent
{
    Transform enemyTransform;
    [SerializeField] float speed = 2.5f;
    float radiusToAttack = 1f;
    float radiusToStopChasing = 4f;

    Vector3 playerPosition;

    IStateMachineComponent mStateMachine;

    Animator animator;

    GameObject explosion;

    public BunnyChaseState(Transform e, IStateMachineComponent mStateMachine, Animator a, GameObject c)
    {
        enemyTransform = e;
        this.mStateMachine = mStateMachine;
        animator = a;
        explosion = c;
    }

    public BunnyChaseState(Transform e, IStateMachineComponent mStateMachine, GameObject c)
    {
        enemyTransform = e;
        this.mStateMachine = mStateMachine;
        explosion = c;
    }

    public void Enter()
    {
        TakePlayerPosition();
    }

    public void Exit()
    {

    }

    void IStateComponent.Update()
    {

    }

    void IStateComponent.FixedUpdate()
    {
        if (TakePlayerPosition())
        {
            Move();
            if ((enemyTransform.position - playerPosition).magnitude < radiusToAttack)
            {
                Debug.Log("DISTANCIA: " + (enemyTransform.position - playerPosition).magnitude);
                mStateMachine.ChangeState(new BunnyAttackComponent(mStateMachine, enemyTransform, GameObject.FindGameObjectWithTag("Player").GetComponent<IDamageableComponent>(), GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(), explosion));
            }
        }
        else
        {
            //mStateMachine.ChangeState(new BunnyWanderState(mStateMachine, enemyTransform, animator));
            mStateMachine.ChangeState(new BunnyWanderState(mStateMachine, enemyTransform, explosion));
        }
    }

    public void IsMoving(Vector2 m)
    {
        throw new System.NotImplementedException();
    }

    public void Move()
    {
        Vector3 direction = playerPosition - enemyTransform.position;
        Vector3 positionToMove = speed * Time.fixedDeltaTime * direction.normalized;
        positionToMove.y = 0;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(-direction.z, 0, direction.x).normalized, Vector3.up);
        enemyTransform.position += positionToMove;
        enemyTransform.rotation = rotation;
    }

    private bool TakePlayerPosition()
    {
        Collider[] p = Physics.OverlapSphere(enemyTransform.position, radiusToStopChasing);

        foreach (Collider c in p)
        {
            if (c.CompareTag("Player"))
            {
                playerPosition = c.transform.position;
                return true;
            }
        }

        return false;
    }
}
