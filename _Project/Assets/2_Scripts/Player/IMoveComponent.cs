using UnityEngine;
using UnityEngine.InputSystem;
public interface IMoveComponent
{
    void IsMoving(Vector2 m);
    void Move();
}
