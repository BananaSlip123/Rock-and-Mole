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
        [SerializeField] Animator animator;
        [SerializeField] Animator animatorPickaxe;


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

                if (IsHitingAnEnemy(localEnemies))
                    DoDamage(localEnemies);

                if (timeHitbox >= TIME_HITBOX)
                {
                    animator.SetBool("Atacar", false);
                    animatorPickaxe.SetBool("Atacar", false);
                    HidePickaxe.instance.HidePickaxeAnimation(false);
                    attackHitbox.enabled = false;
                    timeHitbox = 0f;

                    foreach(Collider c in hitColliders)
                    {
                        if (c != null)
                        {
                            c.gameObject.GetComponent<IDamageableComponent>().ResetHasBeenDamaged();
                        }                      
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

                if (!animator.GetBool("Atacar"))
                    animator.SetBool("Atacar", true);

                animatorPickaxe.SetBool("Atacar", true);

                HidePickaxe.instance.HidePickaxeAnimation(true);
            }
            //else
                //Debug.Log("Estoy en cooldown");
            
        }

        public void ActiveHitbox()
        {
            attackHitbox.enabled = true;
        }

        public void DoDamage(Collider[] hitColliders)
        {           
            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider != null)
                {
                    if (!hitCollider.gameObject.GetComponent<IDamageableComponent>().GetHasBeenDamaged())
                    {
                        hitCollider.gameObject.GetComponent<IDamageableComponent>().RecieveDamage(actualPickaxeStats.damage);
                        Debug.Log("He golpeado a: " + hitCollider.gameObject.name);
                    }
                }
            }          
        }

        private bool IsHitingAnEnemy(Collider[] hitColliders)
        {
            return hitColliders.Length > 0;
        }

        private Queue<Collider> EnemiesCanBeDamaged()
        {
            Collider[] enemies = Physics.OverlapBox(attackHitbox.bounds.center, attackHitbox.bounds.size/2,Quaternion.identity);
            Queue<Collider> enemiesToHit = hitColliders;

            foreach(Collider enemy in enemies)
            {
                if(enemy.CompareTag("Enemy") || enemy.CompareTag("Rock") && !enemy.gameObject.GetComponent<IDamageableComponent>().GetHasBeenDamaged())
                {
                    Debug.Log("ENEMIGO: " + enemy.name);
                    enemiesToHit.Enqueue(enemy);
                }
            }

            return enemiesToHit;
        }
    }
}
