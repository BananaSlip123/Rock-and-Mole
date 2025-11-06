using System;
using UnityEngine;

public class BunnyWanderState : IStateComponent, IMoveComponent
{
    [SerializeField] float speed = 1.5f;
    const float MAXMOVE = 2f;
    const float MINMOVE = 1f;
    float timeMovement = 0f;
    float actualTimeMovement = 0f;

    int directions = 0;

    bool isMoving = false;

    Vector2 directionChoosed = Vector2.zero;
    IStateMachineComponent mStateMachine;

    Transform enemyTransform;

    Animator animator;

    GameObject explosion;

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

    public BunnyWanderState(IStateMachineComponent stateMachine, Transform transform, GameObject c)
    {
        mStateMachine = stateMachine;
        enemyTransform = transform;
        explosion = c;
    }

    public void Enter()
    {
        directions = Enum.GetValues(typeof(Directions)).Length;
    }

    public void Exit()
    {

    }

    void IStateComponent.FixedUpdate()
        {
        Collider[] p = Physics.OverlapSphere(enemyTransform.position, 4f);
        bool player = false;

        foreach (Collider detected in p)
        {
            if (detected.CompareTag("Player"))
            {
                player = true;
                break;
            }
        }

        if (player)
        {
            //mStateMachine.ChangeState(new MouseAttackComponent(enemyTransform, mStateMachine, animator));
            mStateMachine.ChangeState(new BunnyChaseState(enemyTransform, mStateMachine, explosion));
            //Debug.Log("He detectado al jugador");
            return;
        }

        if (!isMoving)
            return;

        if (actualTimeMovement >= timeMovement)
        {
            isMoving = false;
            actualTimeMovement = 0;
            return;
        }

        actualTimeMovement += Time.fixedDeltaTime;
        Move();
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

    Directions ChooseDirection()
    {
        return (Directions)UnityEngine.Random.Range(0, directions);
    }

    public void IsMoving(Vector2 m)
    {
        throw new System.NotImplementedException();
    }

    public void Move()
    {
        enemyTransform.position += VectorConverter.SetVectorToIsoCoords(new Vector3(directionChoosed.x, 0, directionChoosed.y), speed);
        Quaternion rotation = Quaternion.LookRotation(VectorConverter.VectorConeverter(new Vector3(-directionChoosed.y, 0, directionChoosed.x).normalized), Vector3.up);
        enemyTransform.rotation = rotation;
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
                return new Vector2(-1, 1);
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
