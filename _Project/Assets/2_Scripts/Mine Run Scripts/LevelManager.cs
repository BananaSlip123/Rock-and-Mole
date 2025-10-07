using Codice.Client.Common.WebApi.Requests;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject prefabGolem;
    [SerializeField] int nEnemies = 1;

    INoMoreEnemies doorsManagementEnemies;

    void Awake()
    {
        doorsManagementEnemies = GetComponent<INoMoreEnemies>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(nEnemies <= 0)
        {
            doorsManagementEnemies.ThereIsNoEnemies();
        }
    }
}
