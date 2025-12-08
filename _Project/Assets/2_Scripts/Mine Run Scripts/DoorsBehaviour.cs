using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorsBehaviour : MonoBehaviour, IDoorBehaviour, IActiveNoMoreEnemies
{
    Action enterBehaviour;
    [SerializeField] Collider hitbox;
    [SerializeField] GameObject combat;
    [SerializeField] GameObject interrogation;
    [SerializeField] GameObject mining;
    [SerializeField] GameObject skull;
    [SerializeField] GameObject position;
    [SerializeField] GameObject planks;
    bool enter = false;
    enum typeOfBehaviour
    {
        Combat,
        Mining,
        Boss,
        Victory,
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
                Instantiate(combat, position.transform.position, Quaternion.Euler(0,transform.rotation.eulerAngles.y + 90, 0));
                ChangeBehaviour(CombatBehaviour);
                break;
            case (int) typeOfBehaviour.Mining:
                Instantiate(mining, position.transform.position, Quaternion.Euler(0, transform.rotation.eulerAngles.y + 90, 0));
                ChangeBehaviour(MiningBehaviour);
                break;
            case (int)typeOfBehaviour.Boss:
                Instantiate(skull, position.transform.position, Quaternion.Euler(0, transform.rotation.eulerAngles.y + 90, 0));
                ChangeBehaviour(BossBehaviour);
                break;
            case (int)typeOfBehaviour.Victory:
                ChangeBehaviour(VictoryBehaviour);
                break;
        }
    }

    public void ChooseEvent(int tEvent)
    {
        Instantiate(interrogation, position.transform.position, Quaternion.Euler(0, transform.rotation.eulerAngles.y + 90, 0));
        switch (tEvent)
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
        enter = true;
        enterBehaviour?.Invoke();
    }

    public void ChangeBehaviour(Action behaviour)
    {
        enterBehaviour = behaviour;
    }

    public void Active()
    {
        hitbox.enabled = true;
        planks.SetActive(false);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || enter)
            return;
        Debug.Log("HE ENTRADO A UNA PUERTA SUUUU: " + other.name);
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

    private void BossBehaviour()
    {
        SceneManager.LoadScene("8_BossRoom");
    }
    private void VictoryBehaviour()
    {
        UnlockNextBiome();
        
        FindAnyObjectByType<GameMenu>().OnPlayerVictory();
    }
    private void UnlockNextBiome()
    {
        int currentBiomeIdx = (int)BiomeManager.CurrentBiome;
        int nextBiomeIdx = currentBiomeIdx + 1;

        if (nextBiomeIdx >= BiomeManager.numberOfBiomes) return;
        if (BiomeManager.unlockedBiomes[(BiomeName)nextBiomeIdx]) return;
        
        BiomeManager.unlockedBiomes[(BiomeName)nextBiomeIdx] = true;
    }
    #endregion
}
