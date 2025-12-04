using System;
using UnityEngine;

public class ThrowingPickaxe : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int damage;
    public float critMultiplier;
    public float critProbability;


    public PlayerShootComponent shoot;
    public Vector3 dir;
    public Transform player;

    const float MAX_SECONDS = 1f;
    const float WALL_SECONDS = 0.4f;
    const float ATTACK_SPEED = 40f;
    const float RETURN_SPEED = 10f;
    [SerializeField] float speed = ATTACK_SPEED;
    public float seconds = 0f;
    bool isReturning = false;

    public void ResetValues()
    {
        isReturning = false;
        seconds = 0f;
        speed = ATTACK_SPEED;
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        if(seconds < MAX_SECONDS)
        {
            IsMoving(dir);

            seconds += Time.fixedDeltaTime;
        }
        else
        {
            isReturning = true;
            speed = RETURN_SPEED;
            ReturnPickaxe(player.position);         
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy") && !other.GetComponent<IDamageableComponent>().GetHasBeenDamaged())
        {
            float hitCrit = UnityEngine.Random.Range(0, 1);
            int damage = this.damage;
            if (hitCrit < critProbability)
            {
                damage = (int)(critMultiplier * damage);
            }
            other.GetComponent<IDamageableComponent>().RecieveDamage(damage, 0.5f, 0.5f);
        }
        else if(other.CompareTag("Player") && isReturning)
        {            
            seconds = 0f;
            shoot.IsShooting = false;
            
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            seconds = MAX_SECONDS - WALL_SECONDS;
            speed = 0;
        }
    }

    public void IsMoving(Vector3 m)
    {
        transform.position += VectorConverter.SetVectorToIsoCoords(m, speed);
        transform.rotation *= Quaternion.Euler(0f, 0f, 25f);
    }
    
    public void ReturnPickaxe(Vector3 m)
    {
        transform.position += speed * Time.fixedDeltaTime * (m - transform.position);
        transform.rotation *= Quaternion.Euler(0f, 0f, 30f);
    }
}
