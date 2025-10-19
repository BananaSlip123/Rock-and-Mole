using UnityEngine;

public interface IObjectPool
{
    IPooleableObject Get();
    void Release(IPooleableObject o);
}
