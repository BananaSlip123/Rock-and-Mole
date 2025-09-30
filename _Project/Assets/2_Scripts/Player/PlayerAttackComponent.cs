using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using PickaxeStats;
using System.Collections.Generic;

namespace PlayerComponents
{
    public class PlayerAttackComponent : MonoBehaviour, IAttackComponent
    {
        const float COOLDOWN = 0.2f;
        const float TIME_HITBOX = 0.1f;

        private float timeToAttack = 0f;
        private float timeHitbox = 0f;

        private bool isInCooldown = false;

        private Queue<Collider> hitColliders = new Queue<Collider>();

        [SerializeField] Collider attackHitbox;
        [SerializeField] PickaxeStatsScripteableObject actualPickaxeStats;

        void FixedUpdate()
        {
            if (isInCooldown)
            {
                timeToAttack += Time.fixedDeltaTime;

                if (timeToAttack >= COOLDOWN)
                {
                    isInCooldown = false;
                    timeToAttack = 0f;
                }                  

                return;
            }

            if(attackHitbox.enabled)
            {
                timeHitbox += Time.fixedDeltaTime;
                                   
                hitColliders = EnemiesCanBeDamaged();
                Collider[] localEnemies = hitColliders.ToArray();

                if (isHitingAnEnemy(localEnemies))
                    DoDamage(localEnemies);

                if (timeHitbox >= TIME_HITBOX)
                {
                    attackHitbox.enabled = false;
                    timeHitbox = 0f;
                    foreach(Collider c in hitColliders)
                    {
                        c.gameObject.GetComponent<IDamageableComponent>().ResetHasBeenDamaged();
                    }
                    hitColliders.Clear();
                }
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
        }

        public void DoDamage(Collider[] hitColliders)
        {
            foreach(Collider hitCollider in hitColliders)
            {
                if (!hitCollider.gameObject.GetComponent<IDamageableComponent>().GetHasBeenDamaged())
                {
                    hitCollider.gameObject.GetComponent<IDamageableComponent>().RecieveDamage(actualPickaxeStats.damage);
                    Debug.Log("He golpeado a: " + hitCollider.gameObject.name);
                }
            }          
        }

        private bool isHitingAnEnemy(Collider[] hitColliders)
        {
            return hitColliders.Length > 0;
        }

        private Queue<Collider> EnemiesCanBeDamaged()
        {
            Collider[] enemies = Physics.OverlapBox(attackHitbox.bounds.center, attackHitbox.bounds.size/2,Quaternion.identity);
            Queue<Collider> enemiesToHit = hitColliders;

            foreach(Collider enemy in enemies)
            {
                if(enemy.CompareTag("Enemy") && !enemy.gameObject.GetComponent<IDamageableComponent>().GetHasBeenDamaged())
                {
                    Debug.Log("ENEMIGO: " + enemy.name);
                    enemiesToHit.Enqueue(enemy);
                }
            }

            return enemiesToHit;
        }
    }
}
