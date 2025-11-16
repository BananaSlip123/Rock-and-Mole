using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorsTutorial : MonoBehaviour, IDoorBehaviour, IActiveNoMoreEnemies
{
    [SerializeField] Collider hitbox;
    [SerializeField] string escena;
    bool enter = false;

    public void Active()
    {
        hitbox.enabled = true;
    }

    public void ChangeBehaviour(Action n)
    {
        
    }

    public void ChooseBehaviour(int n)
    {
        
    }

    public void ChooseEvent(int n)
    {
        
    }

    public void EnterToRoom()
    {
        enter = true;
        SceneManager.LoadScene(escena);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enter || !other.CompareTag("Player"))
            return;
        EnterToRoom();
    }
}
