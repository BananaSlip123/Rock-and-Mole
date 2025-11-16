using System;
using UnityEngine;

public class MouseRunawayState : IStateComponent, IMoveComponent
{
    [SerializeField] float speed = 2f;
    const float MAXMOVE = 2f;
    const float MINMOVE = 1f;
    float timeMovement = 0f;
    float actualTimeMovement = 0f;

    int directions = 0;

    bool isMoving = false;

    Vector2 directionChoosed = Vector2.zero;
    IStateMachineComponent mStateMachine;

    Transform enemyTransform;
    Transform playerTransform;

    Animator animator;

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

    public MouseRunawayState(IStateMachineComponent stateMachine, Transform transform, Transform player, Animator a)
    {
        mStateMachine = stateMachine;
        enemyTransform = transform;
        animator = a;
        playerTransform = player;
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
        Collider[] p = Physics.OverlapSphere(enemyTransform.position, 5f);
        bool player = false;

        foreach (Collider detected in p)
        {
            if (detected.CompareTag("Player"))
            {
                player = true;
                break;
            }
        }

        if (!player)
        {
            //mStateMachine.ChangeState(new MouseAttackComponent(enemyTransform, mStateMachine, animator));
            mStateMachine.ChangeState(new MouseAttackComponent(enemyTransform, GameObject.FindGameObjectWithTag("Player").transform, animator, mStateMachine));
            //Debug.Log("He detectado al jugador");
            return;
        }

        Move();
    }

    void IStateComponent.Update()
    {
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
        Vector3 direction = enemyTransform.position - playerTransform.position;
        enemyTransform.position += VectorConverter.SetVectorToIsoCoords(new Vector3(direction.x, 0, direction.z).normalized, speed);
        Quaternion rotation = Quaternion.LookRotation(VectorConverter.VectorConeverter(new Vector3(-direction.z, 0, direction.x).normalized), Vector3.up);
        enemyTransform.rotation = rotation;
    }
}
