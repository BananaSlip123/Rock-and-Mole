using UnityEngine;

public interface IStateMachineComponent
{
    void MUpdate();
    void MFixedUpdate();
    void ChangeState(IStateComponent s);
}
