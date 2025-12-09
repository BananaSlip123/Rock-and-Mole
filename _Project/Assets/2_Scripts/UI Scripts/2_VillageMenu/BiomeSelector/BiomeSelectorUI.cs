using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class BiomeSelectorUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txt_BiomeName;
    [SerializeField] GameObject go_EnabledBiomeUI;
    [SerializeField] GameObject go_DisabledBiomeUI;
    [SerializeField] Image img_selectedBiomeIcon;
    [SerializeField] List<DoorSprites> spritesDoors;

    [System.Serializable]
    struct DoorSprites
    {
        public Sprite UnlockedDoorSprite;
        public Sprite LockedDoorSprite;
    }
    #region PRIVATE VARS
    int? _selectedBiomeIdx;
    #endregion
    #region PRIVATE PROPERTIES
    int NumberOfBiomes
    {
        get => BiomeManager.numberOfBiomes;
    }
    int SelectedBiomeIdx
    {
        get
        {
            if (!_selectedBiomeIdx.HasValue)
                _selectedBiomeIdx = (int)BiomeManager.CurrentBiome;
            return _selectedBiomeIdx.Value;
        }
        set
        {
            _selectedBiomeIdx = (value + NumberOfBiomes) % NumberOfBiomes;
            UpdateUI();
        }
    }
    BiomeName SelectedBiome
    {
        get => (BiomeName)SelectedBiomeIdx;
    }
    string SelectedBiomeName
    {
        get => BiomeManager.BiomeNameToString(SelectedBiome);
    }


    #endregion
    #region PRIVATE FUNCS
    void OnEnable()
    {
        UpdateUI();
    }
    private void UpdateUI()
    {
        txt_BiomeName.text = SelectedBiomeName;
        bool isUnlocked = BiomeManager.unlockedBiomes[SelectedBiome];
        go_EnabledBiomeUI.SetActive(isUnlocked);
        go_DisabledBiomeUI.SetActive(!isUnlocked);

        if (isUnlocked)
            img_selectedBiomeIcon.sprite = spritesDoors[SelectedBiomeIdx].UnlockedDoorSprite;
        else
            img_selectedBiomeIcon.sprite = spritesDoors[SelectedBiomeIdx].LockedDoorSprite;

    }
    #endregion

    #region PUBLIC FUNCS
    public void ButtonLeft() => SelectedBiomeIdx--;
    public void ButtonRight() => SelectedBiomeIdx++;
    public void ButtonExplore()
    {
        if (!BiomeManager.unlockedBiomes[SelectedBiome]) return;

        AudioManager.Instance.PlayMusic(AudioManager.MusicType.EnemyFightMusic);
        BiomeManager.CurrentBiome = SelectedBiome;
        //SceneManager.LoadScene("2_CombatRoom");
        //SceneManager.LoadScene("5_RescueRoom");

        //SceneManager.LoadScene("6_CampamentRoom");
        SceneManager.LoadScene("7_DarkRoom");
    }
    #endregion
}
