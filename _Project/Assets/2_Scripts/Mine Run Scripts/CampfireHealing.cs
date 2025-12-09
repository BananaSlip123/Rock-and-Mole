using UnityEngine;
using PlayerComponents;
using System.Collections;

public class CampfireHealing : MonoBehaviour
{
    [SerializeField] const int healing = 50;
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
        
        if(birdAnimator != null)
            birdAnimator.SetBool("isHealing", true);
        if(heart != null)
            heart?.SetActive(true);
        //heartAnimator.SetBool("isRotating", true);
        
        StartCoroutine(WaitAnimation());
        //stats.HealPlayer(healing);
        Debug.Log("He hecho la curación");
        healed = true;
    }

    private IEnumerator WaitAnimation()
    {
        yield return new WaitForSeconds(1.0f);
        if (heart != null)
            heart.SetActive(false);
        stats.HealPlayer(healing);
    }
}
