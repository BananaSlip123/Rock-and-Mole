using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorsBehaviour : MonoBehaviour, IDoorBehaviour, IActiveNoMoreEnemies
{
    Action enterBehaviour;
    [SerializeField] Collider hitbox;

    enum typeOfBehaviour
    {
        Combat,
        Mining,
    }

    enum typeOfEvent
    {
        Campament,
        Rescue,
        Treasure,
        Dark
    }

    public void ChooseBehaviour(int behaviour)
    {
        switch(behaviour)
        {
            case (int) typeOfBehaviour.Combat:
                ChangeBehaviour(CombatBehaviour);
                break;
            case (int) typeOfBehaviour.Mining:
                ChangeBehaviour(MiningBehaviour);
                break;
        }
    }

    public void ChooseEvent(int tEvent)
    {
        switch(tEvent)
        {
            case (int) typeOfEvent.Campament:
                ChangeBehaviour(CampamentBehaviour);
                break;
            case (int) typeOfEvent.Treasure:
                ChangeBehaviour(TreasureBehaviour);
                break;
            case (int) typeOfEvent.Rescue:
                ChangeBehaviour(RescueBehaviour);
                break;
            case (int)typeOfEvent.Dark:
                ChangeBehaviour(DarkBehaviour);
                break;
        }
    }

    public void EnterToRoom()
    {
        enterBehaviour.Invoke();
    }

    public void ChangeBehaviour(Action behaviour)
    {
        enterBehaviour = behaviour;
    }

    public void Active()
    {
        hitbox.enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnterToRoom();
    }

    #region Comportamiento de puertas
    private void CombatBehaviour()
    {
        SceneManager.LoadScene("2_CombatRoom");
    }

    private void MiningBehaviour()
    {
        SceneManager.LoadScene("3_MiningRoom");
    }

    private void CampamentBehaviour()
    {
        SceneManager.LoadScene("6_CampamentRoom");
    }

    private void TreasureBehaviour()
    {
        SceneManager.LoadScene("4_TreasureRoom");
    }

    private void DarkBehaviour()
    {
        SceneManager.LoadScene("7_DarkRoom");
    }

    private void RescueBehaviour()
    {
        SceneManager.LoadScene("5_RescueRoom");
    }   
    #endregion
}
