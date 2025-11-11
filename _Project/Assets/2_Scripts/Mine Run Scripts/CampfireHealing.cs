using UnityEngine;
using PlayerComponents;
using System.Collections;

public class CampfireHealing : MonoBehaviour
{
    [SerializeField] const int healing = 20;
    PlayerController player;
    PlayerStats stats;

    [SerializeField] Animator birdAnimator;
    [SerializeField] Animator heartAnimator;

    [SerializeField] GameObject heart;

    bool healed = false;

    void Awake()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        GameObject ps = GameObject.Find("PlayerStats");
        player = p.GetComponent<PlayerController>();
        stats = ps.GetComponent<PlayerStats>();
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
        
        birdAnimator.SetBool("isHealing", true);
        heart.SetActive(true);
        //heartAnimator.SetBool("isRotating", true);
        
        //StartCoroutine(WaitAnimation());
        //stats.HealPlayer(healing);
        Debug.Log("He hecho la curación");
        player.pressButtonA = null;
        healed = true;
    }

    private IEnumerator WaitAnimation()
    {
        yield return new WaitForSeconds(4.5f);

        heart.SetActive(false);
        stats?.HealPlayer(healing);
    }
}
