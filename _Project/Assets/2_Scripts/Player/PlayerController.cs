using Codice.CM.Common.Checkin.Partial;
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

        private bool isMoving = false;

        private Vector2 movement = new Vector2();

        //public Transform calculoMovimiento;

        // Update is called once per frame
        void Update()
        {
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

        #region Movement
        /// <summary>
        /// CallBack para el movimeinto del jugador
        /// Si el valor cambia se comprueba si ha dejado de moverse
        /// </summary>

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
            Vector3 positionChange = new Vector3(movement.x,0,movement.y);
            positionChange = VectorConeverter(positionChange);

            positionChange = positionChange * Time.deltaTime * speed;

            transform.position += positionChange;
        }
        #endregion

        /// <summary>
        /// Transforma una dirección en coordenadas isométricas
        /// </summary>
        /// <param name="vectorToChange"></param>
        /// <returns></returns>
        private Vector3 VectorConeverter(Vector3 vectorToChange)
        {
            //Quaternion rotation = Quaternion.Euler(0, Vector3.Angle(Vector3.forward, calculoMovimiento.transform.forward), 0);
            Quaternion rotation = Quaternion.Euler(0, 45f, 0);
            Matrix4x4 matrix = Matrix4x4.Rotate(rotation);
            Vector3 vectorConverted = matrix.MultiplyPoint3x4(vectorToChange);
            return vectorConverted;
        }

    }
}
