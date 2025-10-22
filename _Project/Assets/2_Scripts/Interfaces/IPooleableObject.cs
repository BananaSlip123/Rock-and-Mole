using UnityEngine;

public interface IPooleableObject : IPrototype
{
    void Init(Vector3 s, Vector3 p);
    void SetActive(bool b);
    bool IsActive();
    void ResetObject();
}
