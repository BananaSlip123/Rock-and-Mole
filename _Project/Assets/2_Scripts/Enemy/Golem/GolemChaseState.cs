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
            Move();
            if ((enemyTransform.position - playerPosition).magnitude < 10f)
            {
                mStateMachine.ChangeState(new GolemAttackState(mStateMachine, enemyTransform, GameObject.FindGameObjectWithTag("Player").GetComponent<IDamageableComponent>(), GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>()));
            }
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
        Vector3 direction = playerPosition - enemyTransform.position;
        Vector3 positionToMove = VectorConverter.SetVectorToIsoCoords((direction).normalized, speed);
        positionToMove.y = 0;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(-direction.z, 0, direction.x).normalized, Vector3.up);
        enemyTransform.position += positionToMove;
        enemyTransform.rotation = rotation;
    }

    private bool TakePlayerPosition()
    {
        Collider[] p = Physics.OverlapSphere(enemyTransform.position, 15f);

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
