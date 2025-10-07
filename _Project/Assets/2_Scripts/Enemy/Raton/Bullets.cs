using UnityEngine;

public class Bullets : MonoBehaviour, IPooleableObject
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IPrototype Clone()
    {
        throw new System.NotImplementedException();
    }

    public bool IsActive()
    {
        return enabled;
    }

    public void Reset()
    {
        throw new System.NotImplementedException();
    }

    public void SetActive(bool b)
    {
        enabled = b;
    }
}
