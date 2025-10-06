using UnityEngine;

public interface IPooleableObject : IPrototype
{
    void SetActive(bool b);
    bool IsActive();
    void Reset();
}
