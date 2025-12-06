using System;
using System.Collections;
using UnityEngine;

internal class PlantLookingState : IStateComponent
{
    private IStateMachineComponent plantController;
    private Transform enemy;
    private Animator animator;

    Vector2 directionChoosed = Vector2.zero;

    int directions = 0;

    bool isWaiting = false;

    const float MAXMOVE = 2f;
    const float MINMOVE = 1f;

    float timeMovement = 0f;
    float actualTimeMovement = 0f;

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

    public PlantLookingState(IStateMachineComponent plantController, Transform transform, Animator animator)
    {
        this.plantController = plantController;
        this.enemy = transform;
        this.animator = animator;
    }

    public void Enter()
    {
        directions = Enum.GetValues(typeof(Directions)).Length;
    }

    public void Exit()
    {
        
    }

    public void FixedUpdate()
    {
        if (enemy == null) return;

        if (PlayerInRange(3f))
        {
            plantController.ChangeState(new PlantBiteState(plantController, enemy, GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(), animator));
        }
        else if (PlayerInRange(10f))
        {
            plantController.ChangeState(new PlantShootState(plantController, enemy, GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(), animator));
        }

        if(isWaiting)
        {
            actualTimeMovement += Time.fixedDeltaTime;

            if(actualTimeMovement >= timeMovement)
            {
                actualTimeMovement = 0f;
                isWaiting = false;
            }
        }
    }

    public void Update()
    {
        if (enemy == null) return;

        if (!isWaiting)
        {
            directionChoosed = InitializeMovement();      
            enemy.LookAt(directionChoosed);
            timeMovement = UnityEngine.Random.Range(MINMOVE, MAXMOVE);
            isWaiting = true;
        }
    }


    Directions ChooseDirection()
    {
        return (Directions)UnityEngine.Random.Range(0, directions);
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

    public bool PlayerInRange(float radius)
    {
        Collider[] p = Physics.OverlapSphere(enemy.transform.position, radius);

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