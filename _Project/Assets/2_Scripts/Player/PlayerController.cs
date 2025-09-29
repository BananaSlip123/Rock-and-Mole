using PlayerComponents;
using Unity.Plastic.Newtonsoft.Json.Linq;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerComponents
{
    public class PlayerController : MonoBehaviour, IPlayerController
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private float damage = 0f;
        private Vector2 movement = new Vector2();
        private bool isMoving = false;

        [SerializeField] private InputAction moveCommand;

        // Update is called once per frame
        void Update()
        {
            /*
            isMoving = moveCommand.triggered;
            Debug.Log("ACCION: " + moveCommand.triggered);
            
            */

            if (isMoving)
            {
                Move(movement);
            }
        }

        /// <Atack>
        /// Método de ataque del jugador
        /// </Atack>
        public void Atack()
        {
            Debug.Log("PICAAAAAAASO");
        }

        /// <Movement>
        /// Método de movimiento del jugador
        /// Usa la posición de la cámara para moverse
        /// </Movement>

        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 valor = context.ReadValue<Vector2>();

            if (valor == Vector2.zero)
            {
                isMoving = false;
                movement = Vector2.zero;
                return;
            }

            isMoving = true;
            movement = valor;
        }

        public void Move(Vector2 movement)
        {
            Debug.Log("CAMINAAAAAAANDO: " + movement);
        }
    }
}
