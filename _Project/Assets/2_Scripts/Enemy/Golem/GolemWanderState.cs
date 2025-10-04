using UnityEngine;
using System;

public class GolemWanderState : IStateComponent, IMoveComponent
{
    [SerializeField] float speed = 3f;
    const float MAXMOVE = 2f;
    const float MINMOVE = 1f;
    float timeMovement = 0f;
    float actualTimeMovement = 0f;

    int directions = 0;

    bool isMoving = false;

    Vector2 directionChoosed = Vector2.zero;
    IStateMachineComponent mStateMachine;

    Transform enemyTransform;

    public GolemWanderState(IStateMachineComponent m, Transform enemyTransform)
    {
        mStateMachine = m;
        this.enemyTransform = enemyTransform;
    }

    enum Directions
    {
        Up,
        Up_Left,
        Left,
        Left_Down,
        Right,
        Right_Down,
        Down,
        Up_Right
    }

    public void Enter()
    {
        directions = Enum.GetValues(typeof(Directions)).Length;
    }

    public void Exit()
    {
        
    }

    void IStateComponent.Update()
    {
        if (!isMoving)
        {
            directionChoosed = InitializeMovement();
            isMoving = true;

            timeMovement = UnityEngine.Random.Range(MINMOVE, MAXMOVE);
        }        
    }

    void IStateComponent.FixedUpdate()
    {
        Collider[] p = Physics.OverlapSphere(enemyTransform.position, 5f);
        bool player = false;

        foreach(Collider detected in p)
        {
            if(detected.CompareTag("Player"))
            {
                player = true;
                break;
            }
        }

        if (player)
        {
            mStateMachine.ChangeState(new GolemChaseState(enemyTransform, mStateMachine));
            return;
        }

        if (!isMoving)
            return;

        if(actualTimeMovement >= timeMovement)
        {
            isMoving = false;
            actualTimeMovement = 0;
            return;
        }

        actualTimeMovement += Time.fixedDeltaTime;
        Move();
    }

    Directions ChooseDirection()
    {
        return (Directions) UnityEngine.Random.Range(0, directions);
    }

    public void IsMoving(Vector2 m)
    {
        throw new System.NotImplementedException();
    }

    public void Move()
    {
        enemyTransform.position += VectorConverter.SetVectorToIsoCoords(new Vector3(directionChoosed.x, 0, directionChoosed.y), speed);
    }

    Vector2 InitializeMovement()
    {
        switch (ChooseDirection())
        {
            case Directions.Up:
                return Vector2.up;
            case Directions.Down:
                return Vector2.down;
            case Directions.Left:
                return Vector2.left;
            case Directions.Right:
                return Vector2.right;
            case Directions.Up_Left:
                return new Vector2(-1,1);
            case Directions.Up_Right:
                return new Vector2(1, 1);
            case Directions.Left_Down:
                return new Vector2(-1, -1);
            case Directions.Right_Down:
                return new Vector2(1, -1);
        }

        return Vector2.zero;
    }
}
