using PlayerComponents;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerComponents
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerAttackComponent attackComponent;
        [SerializeField] private IMoveComponent moveComponent;
        [SerializeField] private ISkillComponent dashComponent;

        public Action pressButtonA = null;

        //public Transform calculoMovimiento;
        private void Awake()
        {
            moveComponent = GetComponent<IMoveComponent>();
            dashComponent = GetComponent<ISkillComponent>();
        }
        // Update is called once per frame
        void FixedUpdate()
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
            moveComponent.IsMoving(context.ReadValue<Vector2>());
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            dashComponent.DoSpecialSkill();
        }

        public void OnButtonA(InputAction.CallbackContext context)
        {
            pressButtonA?.Invoke();
        }
    }
}
