using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using static UnityEngine.GraphicsBuffer;

namespace PlayerComponents
{
    public class PlayerMovementComponent : MonoBehaviour, IMoveComponent, ISkillComponent
    {
        LayerMask layerMask;
        [SerializeField] GameObject trail;

        #region Movimiento
        [SerializeField] public float speed;
        [SerializeField] Transform go;

        private bool isMoving = false;

        Quaternion rotation;

        Vector2 directionRotation;
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
        public Animator animator;

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

            //directionRotation = movement;
            isMoving = true;
            movement = valor;
            directionRotation = movement;
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
            rotation = Quaternion.LookRotation(VectorConverter.VectorConeverter(new Vector3(-directionRotation.y, 0, directionRotation.x).normalized), Vector3.up);

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

            StartCoroutine(ActiveTrail());
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

        IEnumerator ActiveTrail()
        {
            trail.SetActive(true);

            yield return new WaitForSeconds(DASH_TIME + 0.2f);

            trail.SetActive(false);
        }
    }
}
