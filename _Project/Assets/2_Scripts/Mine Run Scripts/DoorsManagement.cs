using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorsManagement : MonoBehaviour, IDoorsManagement, INoMoreEnemies
{
    [SerializeField] SO_DoorsProbabilities probabilities;
    [SerializeField] int NUMBER_TO_BOSS = 10;

    GameObject[] doors;
    PlayerStats access;

    void Awake()
    {
        access = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        doors = GameObject.FindGameObjectsWithTag("Door");   
        
        foreach(GameObject go in doors)
        {
            ChooseRoom(go.GetComponent<IDoorBehaviour>());
        }
    }

    public void ChooseRoom(IDoorBehaviour puerta)
    {
        if(access.roomNumber == NUMBER_TO_BOSS)
        {
            puerta.ChooseBehaviour(3);
            return;
        }
        else if(access.roomNumber == NUMBER_TO_BOSS - 1)
        {
            puerta.ChooseBehaviour(2);
            return;
        }

        float random = Random.Range(0f, 0.99f);
        Debug.Log("RANDOM ROOM: " + random);
        if(random < probabilities.combatProb)
            puerta.ChooseBehaviour(0);
        else
            ChooseEventType(puerta);
    }

    public void ChooseEventType(IDoorBehaviour puerta)
    {
        float random = 0f;

        if(SceneManager.GetActiveScene().name == "4_TreasureRoom")
        {         
            random = 0.4f;
        }
        else if(SceneManager.GetActiveScene().name == "6_CampamentRoom")
        {
            random = 0.7f;
        }
        else
        {
            random = Random.Range(0f, 0.99f);
        }
        

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
        Debug.Log("ABRO PUERTAS");
        foreach (GameObject door in doors)
        {
            door.GetComponent<IActiveNoMoreEnemies>().Active();
        }
    }
    
}