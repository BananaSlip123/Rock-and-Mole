using UnityEngine;
using UnityEngine.InputSystem.HID;
using static UnityEngine.GraphicsBuffer;

namespace PlayerComponents
{
    public class PlayerMovementComponent : MonoBehaviour, IMoveComponent, ISkillComponent
    {
        LayerMask layerMask;

        #region Movimiento
        [SerializeField] public float speed;
        [SerializeField] Transform go;

        private bool isMoving = false;
        #endregion

        #region Dash
        const float COOLDOWN = 1f;
        const float DASH_TIME = 0.1f;

        float timeCooldown = 0f;
        float timeDashing = 0f;
        [SerializeField]float speedDash = 10f;

        bool IsInCooldown = false;

        public bool isDashing = false;
        #endregion

        private Vector2 movement = new Vector2();
        [SerializeField] Animator animator;

        private void Awake()
        {
            layerMask = LayerMask.GetMask("Wall");
        }

        public void IsMoving(Vector2 valor)
        {
            if (valor == Vector2.zero)
            {
                isMoving = false;
                movement = Vector2.zero;

                animator.SetBool("Andar", false);

                //Detener sonido de caminar
                AudioManager.Instance.StopAudio(AudioManager.AudioType.WalkSound);
                return;
            }

            isMoving = true;
            movement = valor;
            animator.SetBool("Andar", true);

            //Reproducir sonido de caminar si no se está reproduciendo
            AudioManager.Instance.PlayLoopedAudio(AudioManager.AudioType.WalkSound);
        }

        public bool IsPlayerDashing()
        {
            return isDashing;
        }

        public void Move()
        {
            Quaternion rotation = Quaternion.LookRotation(VectorConverter.VectorConeverter(new Vector3(-movement.y, 0, movement.x).normalized), Vector3.up);

            transform.rotation = rotation;


            Vector3 direction = VectorConverter.VectorConeverter(new Vector3(movement.x, 0, movement.y).normalized);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, 0.5f, layerMask))           
                return;

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
                    timeCooldown = 0f;
                }
            }
                
            if (!isMoving)
                return;

            transform.position += VectorConverter.SetVectorToIsoCoords(new Vector3(movement.x, 0, movement.y),speed);           
        }

        public void InitializeSpecialSkill()
        {
            if (IsInCooldown || isDashing)
                return;

            timeCooldown = 0;
            timeDashing = 0;
            isDashing = true;

            Debug.Log("He iniciado el dash");
        }

        public void DoSpecialSkill()
        {
            if (IsInCooldown)
                return;

            transform.position += VectorConverter.SetVectorToIsoCoords(new Vector3(movement.x, 0, movement.y), speedDash);           

            if(timeDashing < DASH_TIME)
                timeDashing += Time.fixedDeltaTime;
            else
            {
                isDashing = false;
                IsInCooldown = true;
                timeDashing = 0f;

                Debug.Log("He terminado el dash");
            }
        }
    }
}
