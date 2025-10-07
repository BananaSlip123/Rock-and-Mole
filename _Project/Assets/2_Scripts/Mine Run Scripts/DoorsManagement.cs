using UnityEngine;

public class DoorsManagement : MonoBehaviour, IDoorsManagement, INoMoreEnemies
{
    [SerializeField] SO_DoorsProbabilities probabilities;
    GameObject[] doors;

    void Awake()
    {
        doors = GameObject.FindGameObjectsWithTag("Door");   
        
        foreach(GameObject go in doors)
        {
            ChooseRoom(go.GetComponent<IDoorBehaviour>());
        }
    }

    public void ChooseRoom(IDoorBehaviour puerta)
    {
        float random = Random.Range(0f, 0.99f);
        Debug.Log("RANDOM ROOM: " + random);
        if(random < probabilities.combatProb)
            puerta.ChooseBehaviour(0);
        else if(random < probabilities.miningProb)
            puerta.ChooseBehaviour(1);
        else
            ChooseEventType(puerta);
    }

    public void ChooseEventType(IDoorBehaviour puerta)
    {
        float random = Random.Range(0f, 0.99f);

        Debug.Log("RANDOM EVENT: " + random);

        if (random < probabilities.campamentProb)
            puerta.ChooseEvent(0);
        else if (random < probabilities.rescueProb)
            puerta.ChooseEvent(1);
        else if (random < probabilities.tresaureProb)
            puerta.ChooseEvent(2);
        else
            puerta.ChooseEvent(3);
    }

    public void ThereIsNoEnemies()
    {
        foreach (GameObject door in doors)
        {
            door.GetComponent<IActiveNoMoreEnemies>().Active();
        }
    }
}