using Codice.CM.Common;
using UnityEngine;

namespace PlayerComponents
{
    public class PlayerMovementComponent : MonoBehaviour, IMoveComponent
    {
        [SerializeField] private float speed;

        private bool isMoving = false;

        private Vector2 movement = new Vector2();

        public void isPlayerMoving(Vector2 valor)
        {
            if (valor == Vector2.zero)
            {
                isMoving = false;
                movement = Vector2.zero;
                return;
            }

            isMoving = true;
            movement = valor;
        }

        public void Move()
        {
            if (!isMoving)
                return;

            Vector3 positionChange = new Vector3(movement.x, 0, movement.y);
            positionChange = VectorConeverter(positionChange);

            positionChange = positionChange * Time.deltaTime * speed;

            transform.position += positionChange;
        }

        /// <summary>
        /// Transforma una dirección en coordenadas isométricas
        /// </summary>
        /// <param name="vectorToChange">Vector a cambiar de coordenadas</param>
        /// <returns>El vector convertido en coordenadas isométricas</returns>
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
