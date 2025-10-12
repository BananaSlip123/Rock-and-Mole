using PlasticGui.Diff.Annotate;
using UnityEngine;

public class GolemChaseState : IStateComponent, IMoveComponent
{
    Transform enemyTransform;
    [SerializeField] float speed = 5f;
    float radiusToAttack = 1f;
    float radiusToStopChasing = 1f;

    Vector3 playerPosition;

    IStateMachineComponent mStateMachine;

    Animator animator;

    public GolemChaseState(Transform e, IStateMachineComponent mStateMachine, Animator a)
    {
        enemyTransform = e;
        this.mStateMachine = mStateMachine;
        animator = a;

        if (animator.CompareTag("Chikito"))
        {
            radiusToAttack = 2f;
            radiusToStopChasing = 5f;
        }    
        else
        {
            radiusToAttack = 10f;
            radiusToStopChasing = 15f;
        }
            
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

    public void FixedUpdate()
    {
        if(TakePlayerPosition())
        {
            Move();
            if ((enemyTransform.position - playerPosition).magnitude < radiusToAttack)
            {
                mStateMachine.ChangeState(new GolemAttackState(mStateMachine, enemyTransform, GameObject.FindGameObjectWithTag("Player").GetComponent<IDamageableComponent>(), GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(), animator));
            }
        }
        else
        {
            mStateMachine.ChangeState(new GolemWanderState(mStateMachine, enemyTransform, animator));
        }
    }

    public void IsMoving(Vector2 m)
    {
        throw new System.NotImplementedException();
    }

    public void Move()
    {
        Vector3 direction = playerPosition - enemyTransform.position;
        Vector3 positionToMove = VectorConverter.SetVectorToIsoCoords((direction).normalized, speed);
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
