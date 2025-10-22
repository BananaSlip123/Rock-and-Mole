using System.Collections.Generic;
using UnityEngine;

public class BulletsPool : MonoBehaviour, IObjectPool
{
    Queue<IPooleableObject> bullets = new Queue<IPooleableObject>();
    [SerializeField] List<IPooleableObject> bulletsList = new List<IPooleableObject>();
    void Awake()
    {
        foreach(IPooleableObject p in bulletsList)
        {
            bullets.Enqueue(p);
        }

        bulletsList = null;
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
