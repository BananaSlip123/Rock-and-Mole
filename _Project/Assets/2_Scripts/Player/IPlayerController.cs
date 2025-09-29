using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerComponents
{
    public interface IPlayerController
    {
        void Atack();
        void Move(Vector2 movement);
    }
}
