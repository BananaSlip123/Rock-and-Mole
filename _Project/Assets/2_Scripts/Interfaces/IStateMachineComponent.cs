using UnityEngine;

public interface IStateMachineComponent
{
    bool IsPaused { get; set; }
    void MUpdate();
    void MFixedUpdate();
    void ChangeState(IStateComponent s);
}
