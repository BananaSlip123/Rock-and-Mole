using PlasticGui.Diff.Annotate;
using UnityEngine;

public class GolemChaseState : IStateComponent, IMoveComponent
{
    Transform enemyTransform;
    [SerializeField] float speed = 5f;

    Vector3 playerPosition;

    IStateMachineComponent mStateMachine;

    public GolemChaseState(Transform e, IStateMachineComponent mStateMachine)
    {
        enemyTransform = e;
        this.mStateMachine = mStateMachine;
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
            if ((enemyTransform.position - playerPosition).magnitude < 0.5f)
            {
                mStateMachine.ChangeState(new GolemAttackState(mStateMachine, enemyTransform, GameObject.FindGameObjectWithTag("Player").GetComponent<IDamageableComponent>()));
            }
            Move();
        }
        else
        {
            mStateMachine.ChangeState(new GolemWanderState(mStateMachine, enemyTransform));
        }
    }

    public void IsMoving(Vector2 m)
    {
        throw new System.NotImplementedException();
    }

    public void Move()
    {
        enemyTransform.position += VectorConverter.SetVectorToIsoCoords((playerPosition - enemyTransform.position).normalized, speed);
    }

    private bool TakePlayerPosition()
    {
        Collider[] p = Physics.OverlapSphere(enemyTransform.position, 5f);

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
