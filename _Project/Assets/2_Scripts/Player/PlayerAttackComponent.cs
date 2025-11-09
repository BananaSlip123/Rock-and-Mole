using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using PickaxeStats;
using System.Collections.Generic;

namespace PlayerComponents
{
    public class PlayerAttackComponent : MonoBehaviour, IAttackComponent
    {
        [SerializeField] public float COOLDOWN = 0.4f;
        const float TIME_HITBOX = 1.5f;

        private float timeToAttack = 0f;
        private float timeHitbox = 0f;

        private bool isInCooldown = false;

        public int damage;
        public float critMultiplier;
        public float critProbability;

        private Queue<Collider> hitColliders = new Queue<Collider>();

        [SerializeField] Collider attackHitbox;
        [SerializeField] PickaxeStatsScripteableObject actualPickaxeStats;
        [SerializeField] Animator animator;

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
                if(timeHitbox == 0.4f)
                    attackHitbox.enabled = false;
                else if (timeHitbox >= TIME_HITBOX)
                {                                     
                    HidePickaxe.instance.HidePickaxeAnimation(false);
                    
                    timeHitbox = 0f;

                    animator?.SetBool("Atacar", false);

                    foreach (Collider c in hitColliders)
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

                if (animator != null && !animator.GetBool("Atacar"))
                    animator.SetBool("Atacar", true);

                HidePickaxe.instance.HidePickaxeAnimation(true);

                //Reproducir sonido de ataque a enemigo
                AudioManager.Instance.PlayAudio(AudioManager.AudioType.AttackToEnemySound);
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
                        float hitCrit = Random.Range(0,1);
                        int damage = this.damage;
                        if(hitCrit < critProbability)
                        {
                            damage = (int) (critMultiplier * damage);
                        }
                        hitCollider.gameObject.GetComponent<IDamageableComponent>().RecieveDamage(damage);
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
