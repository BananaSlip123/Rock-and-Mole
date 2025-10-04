using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] IDamageableComponent damageableComponent;
    void Awake()
    {
        damageableComponent = GetComponent<IDamageableComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
