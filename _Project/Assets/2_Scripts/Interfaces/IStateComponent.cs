using UnityEngine;

public interface IStateComponent
{
    void Enter();
    void Exit();
    void Update();
    void FixedUpdate();
}
