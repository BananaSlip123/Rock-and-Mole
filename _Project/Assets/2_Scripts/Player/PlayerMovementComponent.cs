using Codice.CM.Common;
using UnityEngine;

namespace PlayerComponents
{
    public class PlayerMovementComponent : MonoBehaviour, IMoveComponent, ISkillComponent
    {
        #region Movimiento
        [SerializeField] private float speed;

        private bool isMoving = false;
        #endregion

        #region Dash
        const float COOLDOWN = 0.2f;
        const float DASH_TIME = 0.1f;

        float timeCooldown = 0f;
        float timeDashing = 0f;
        [SerializeField]float speedDash = 10f;

        bool IsInCooldown = false;

        public bool isDashing = false;
        #endregion

        private Vector2 movement = new Vector2();

        public void IsMoving(Vector2 valor)
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

        public bool IsPlayerDashing()
        {
            return isDashing;
        }

        public void Move()
        {
            if (isDashing)
            {
                DoSpecialSkill();
                return;
            }
            else if(IsInCooldown)
            {
                Debug.Log("Estoy recargando el dash");
                if (timeCooldown < COOLDOWN)
                    timeCooldown += Time.fixedDeltaTime;
                else
                {
                    IsInCooldown = false;
                }
            }
                
            if (!isMoving)
                return;

            transform.position += SetVectorToIsoCoords(new Vector3(movement.x, 0, movement.y),speed);
        }

        public void InitializeSpecialSkill()
        {
            if (IsInCooldown)
                return;

            timeCooldown = 0;
            timeDashing = 0;
            isDashing = true;

            Debug.Log("He iniciado el dash");
        }

        public void DoSpecialSkill()
        {                        
            transform.position += SetVectorToIsoCoords(new Vector3(movement.x, 0, movement.y), speedDash);           

            if(timeDashing < DASH_TIME)
                timeDashing += Time.fixedDeltaTime;
            else
            {
                isDashing = false;
                IsInCooldown = true;

                Debug.Log("He terminado el dash");
            }
        }

        private Vector3 SetVectorToIsoCoords(Vector3 vector, float speed)
        {
            vector = VectorConeverter(vector);

            vector = speed * Time.fixedDeltaTime * vector;

            return vector;
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
