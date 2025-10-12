using UnityEngine;

public class EnemyController : MonoBehaviour, IStateMachineComponent
{
    [SerializeField] IStateComponent actualState;
    [SerializeField] IStateComponent lastState;

    [SerializeField]Animator animator;

    void Awake()
    {
        actualState = new GolemWanderState(this, transform, animator);
        actualState.Enter();
    }

    // Update is called once per frame
    void Update()
    {
        MUpdate();
    }

    void FixedUpdate()
    {
        MFixedUpdate();
    }

    public void MUpdate()
    {
        actualState.Update();
    }

    public void MFixedUpdate()
    {
        actualState.FixedUpdate();
    }

    public void ChangeState(IStateComponent newState)
    {
        lastState = actualState;
        actualState = newState;

        lastState.Exit();
        actualState.Enter();
    }
}
