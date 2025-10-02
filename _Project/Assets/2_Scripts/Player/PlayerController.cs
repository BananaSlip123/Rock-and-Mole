using Codice.CM.Common.Checkin.Partial;
using PlayerComponents;
using Unity.Plastic.Newtonsoft.Json.Linq;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerComponents
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerAttackComponent attackComponent;
        [SerializeField] private PlayerMovementComponent moveComponent;
        [SerializeField] private ISkillComponent dashComponent;

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

        public void OnDash(InputAction.CallbackContext context)
        {
            dashComponent.DoSpecialSkill();
        }
    }
}
