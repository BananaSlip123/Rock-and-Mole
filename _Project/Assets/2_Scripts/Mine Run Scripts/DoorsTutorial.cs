using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorsTutorial : MonoBehaviour, IDoorBehaviour, IActiveNoMoreEnemies
{
    [SerializeField] Collider hitbox;
    [SerializeField] string escena;

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
        SceneManager.LoadScene(escena);
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnterToRoom();
    }
}
