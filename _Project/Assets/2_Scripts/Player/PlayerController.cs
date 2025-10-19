using PlayerComponents;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerComponents
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerAttackComponent attackComponent;
        [SerializeField] private PlayerMovementComponent moveComponent;

        //public Transform calculoMovimiento;

        // Update is called once per frame
        void Update()
        {
            moveComponent.Move();
        }

        /// <summary>
        ///CallBack para el ataque del jugador
        /// </summary>
        public void OnAttack(InputAction.CallbackContext context)
        {
            attackComponent.Attack();
        }

        /// <summary>
        /// CallBack para el movimiento del jugador
        /// Si el valor cambia se comprueba si ha dejado de moverse
        /// </summary>

        public void OnMove(InputAction.CallbackContext context)
        {
            moveComponent.isPlayerMoving(context.ReadValue<Vector2>());
        }
    }
}
