using Codice.CM.Common;
using UnityEngine;

public class ThrowingPickaxe : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int damage;
    public float critMultiplier;
    public float critProbability;

    bool isReturning = false;

    public PlayerShootComponent shoot;
    public Vector3 dir;

    public Transform player;

    float MAX_SECONDS = 1f;
    [SerializeField]float speed = 100f;
    public float seconds = 0f;

    // Update is called once per frame
    public void FixedUpdate()
    {
        if(seconds <= MAX_SECONDS)
        {
            IsMoving(dir);

            seconds += Time.fixedDeltaTime;
        }
        else
        {
            isReturning = true;
            speed = 10f;
            ReturnPickaxe(player.position);         
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemigo") && !other.GetComponent<IDamageableComponent>().GetHasBeenDamaged())
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
            shoot.isShooting = false;
            Destroy(this.gameObject);
        }
        else if(other.CompareTag("Wall"))
        {
            seconds = 100f;
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
