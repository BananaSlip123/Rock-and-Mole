using UnityEngine;
using PlayerComponents;

public class CampfireHealing : MonoBehaviour
{
    [SerializeField] const int healing = 20;
    PlayerController player;
    [SerializeField]PlayerStats stats;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            player.pressButtonA = HealPlayer;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            player.pressButtonA = null;
        }
    }

    private void HealPlayer()
    {
        stats.HealPlayer(healing);
    }
}
