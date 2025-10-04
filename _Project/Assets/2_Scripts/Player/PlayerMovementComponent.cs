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

            transform.position += VectorConverter.SetVectorToIsoCoords(new Vector3(movement.x, 0, movement.y),speed);
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
            transform.position += VectorConverter.SetVectorToIsoCoords(new Vector3(movement.x, 0, movement.y), speedDash);           

            if(timeDashing < DASH_TIME)
                timeDashing += Time.fixedDeltaTime;
            else
            {
                isDashing = false;
                IsInCooldown = true;

                Debug.Log("He terminado el dash");
            }
        }
    }
}
