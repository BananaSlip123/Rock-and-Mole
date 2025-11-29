using UnityEngine;
using PlayerComponents;
using System.Collections;

public class CampfireHealing : MonoBehaviour
{
    [SerializeField] const int healing = 40;
    PlayerStats stats;

    [SerializeField] Animator birdAnimator;
    [SerializeField] Animator heartAnimator;

    [SerializeField] GameObject heart;

    bool healed = false;

    void Awake()
    {
        GameObject ps = GameObject.Find("PlayerStats");
        stats = ps.GetComponent<PlayerStats>();
    }
    public void HealPlayer()
    {
        if (healed) return;

        Debug.Log("Me estoy curando");
        
        birdAnimator.SetBool("isHealing", true);
        heart.SetActive(true);
        //heartAnimator.SetBool("isRotating", true);
        
        StartCoroutine(WaitAnimation());
        //stats.HealPlayer(healing);
        Debug.Log("He hecho la curación");
        healed = true;
    }

    private IEnumerator WaitAnimation()
    {
        yield return new WaitForSeconds(4.5f);

        heart.SetActive(false);
        stats?.HealPlayer(healing);
    }
}
