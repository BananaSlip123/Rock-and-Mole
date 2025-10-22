using Codice.CM.Common;
using UnityEngine;

public class Bullets : MonoBehaviour, IPooleableObject, IMoveComponent
{
    Vector3 direction;
    const float SPEED = 1f;

    float TIME_DESPAWN = 1f;
    float timeToDespawn = 0f;

    IObjectPool pool;

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
        throw new System.NotImplementedException();
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
    }

    public void SetActive(bool b)
    {
        enabled = b;
    }

    public void IsMoving(Vector2 m)
    {
        throw new System.NotImplementedException();
    }

    public void Move()
    {
        Vector3 positionToMove = VectorConverter.SetVectorToIsoCoords((direction).normalized, SPEED);
        positionToMove.y = 0;

        transform.position += positionToMove;
    }
}
