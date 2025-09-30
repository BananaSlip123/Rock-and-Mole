using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerComponents
{
    public class PlayerAttackComponent : MonoBehaviour, IAttackComponent
    {
        const float COOLDOWN = 0.2f;
        const float TIME_HITBOX = 0.1f;

        private float timeToAttack = 0f;
        private float timeHitbox = 0f;

        private bool isInCooldown = false;

        [SerializeField] Collider attackHitbox;

        

        void FixedUpdate()
        {
            if (isInCooldown)
            {
                timeToAttack += Time.deltaTime;

                if (timeToAttack >= COOLDOWN)
                    isInCooldown = false;
            }
        }

        public void Attack()
        {
            if (!isInCooldown)
            {
                isInCooldown = true;
                ActiveHitbox();
            }
            else
                Debug.Log("Estoy en cooldown");
            
        }

        public void ActiveHitbox()
        {
            attackHitbox.enabled = true;
            StartCoroutine(DeactivateHitbox());
        }

        private IEnumerator DeactivateHitbox()
        {
            yield return new WaitForSeconds(TIME_HITBOX);

            attackHitbox.enabled = false;
        }
    }
}
