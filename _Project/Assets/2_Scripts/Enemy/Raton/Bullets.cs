using Codice.CM.Common;
using UnityEngine;

public class Bullets : MonoBehaviour, IPooleableObject, IMoveComponent
{
    Vector3 direction;
    const float SPEED = 5f;

    float TIME_DESPAWN = 5f;
    float timeToDespawn = 0f;

    [SerializeField] GameObject poolO;
    IObjectPool pool;

    void Awake()
    {
        pool = poolO.GetComponent<IObjectPool>();
    }

    void FixedUpdate()
    {
        Move();

        timeToDespawn += Time.fixedDeltaTime;
        if (timeToDespawn >= TIME_DESPAWN)
            ResetObject();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(Vector3 directionToMove, Vector3 position)
    {
        SetActive(true);

        transform.position = position;
        direction = directionToMove;       
    }

    public IPrototype Clone()
    {
        GameObject b = Instantiate(gameObject);
        Bullets bala = b.GetComponent<Bullets>();
        return bala;
    }

    public bool IsActive()
    {
        return enabled;
    }

    public void ResetObject()
    {
        SetActive(false);
        pool.Release(this);
        direction = Vector3.zero;

        timeToDespawn = 0f;
    }

    public void SetActive(bool b)
    {
        enabled = b;
        gameObject.SetActive(b);
    }

    public void IsMoving(Vector2 m)
    {
        throw new System.NotImplementedException();
    }

    public void Move()
    {
        Vector3 positionToMove = VectorConverter.MovingVector((direction).normalized, SPEED);
        positionToMove.y = 0;

        Quaternion rotation = Quaternion.LookRotation(new Vector3(-direction.z, 0, direction.x).normalized, Vector3.up);
        transform.position += positionToMove;
        transform.rotation = rotation;
    }
}
