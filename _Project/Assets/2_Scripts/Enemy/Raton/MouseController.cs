using UnityEngine;

public class MouseController : MonoBehaviour, IStateMachineComponent
{
    [SerializeField] IStateComponent actualState;
    [SerializeField] IStateComponent lastState;

    [SerializeField] Animator animator;

    void Awake()
    {
        actualState = new MouseWanderState(this, transform, animator);
        actualState.Enter();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPaused || GameObject.FindGameObjectWithTag("Player") == null) return;
        MUpdate();
    }

    void FixedUpdate()
    {
        if (IsPaused || GameObject.FindGameObjectWithTag("Player") == null) return;
        MFixedUpdate();
    }
    
    bool _isPaused;
    public bool IsPaused
    {
        get => _isPaused;
        set => _isPaused = value;
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
