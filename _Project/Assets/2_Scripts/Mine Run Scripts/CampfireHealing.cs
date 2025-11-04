using UnityEngine;
using PlayerComponents;

public class CampfireHealing : MonoBehaviour
{
    [SerializeField] const int healing = 20;
    PlayerController player;
    PlayerStats stats;

    bool healed = false;

    void Awake()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        player = p.GetComponent<PlayerController>();
        stats = p.GetComponent<PlayerStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !healed)
        {
            Debug.Log("Asigno curacion");
            player.pressButtonA = HealPlayer;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Quito curacion");
            player.pressButtonA = null;
        }
    }

    private void HealPlayer()
    {
        Debug.Log("Me estoy curando");
        stats.HealPlayer(healing);
        player.pressButtonA = null;
        healed = true;
    }
}
