using System.Collections.Generic;
using UnityEngine;

public class BulletsPool : MonoBehaviour, IObjectPool
{
    Queue<IPooleableObject> bullets = new Queue<IPooleableObject>();
    [SerializeField] GameObject prefab;

    const short LIMIT = 40;

    void Awake()
    {
        IPrototype cloneable = prefab.GetComponent<IPrototype>();

        for(int i = 0; i < LIMIT; i++)
        {
            IPooleableObject objectToEnqueue = (IPooleableObject)cloneable.Clone();
            objectToEnqueue.SetActive(false);
            bullets.Enqueue(objectToEnqueue);
        }
    }

    public IPooleableObject Get()
    {
        return bullets.Dequeue();
    }

    public void Release(IPooleableObject o)
    {
        bullets.Enqueue(o);
    }
}
