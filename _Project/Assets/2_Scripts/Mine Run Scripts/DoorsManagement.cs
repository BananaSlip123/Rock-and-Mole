using System.Collections.Generic;
using UnityEngine;

public class DoorsManagement : MonoBehaviour, IDoorsManagement
{
    [SerializeField] SO_DoorsProbabilities probabilities;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        GameObject[] doors = GameObject.FindGameObjectsWithTag("Doors");   
        
        foreach(GameObject go in doors)
        {
            ChooseRoom(go.GetComponent<IDoorBehaviour>());
        }
    }

    public void ChooseRoom(IDoorBehaviour puerta)
    {
        float random = Random.Range(0f, 99f);

        if(random < probabilities.combatProb)
            puerta.ChooseBehaviour(0);
        else if(random < probabilities.miningProb)
            puerta.ChooseBehaviour(1);
        else
            ChooseEventType(puerta);
    }

    public void ChooseEventType(IDoorBehaviour puerta)
    {
        float random = Random.Range(0f, 99f);

        if (random < probabilities.campamentProb)
            puerta.ChooseEvent(0);
        else if (random < probabilities.rescueProb)
            puerta.ChooseEvent(1);
        else if (random < probabilities.tresaureProb)
            puerta.ChooseEvent(2);
        else
            puerta.ChooseEvent(3);
    }
}