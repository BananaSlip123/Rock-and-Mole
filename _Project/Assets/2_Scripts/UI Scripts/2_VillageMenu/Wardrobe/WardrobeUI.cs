using UnityEngine;
using TMPro;
using PickaxeStats;
using System;
using System.Collections.Generic;
using System.Collections;
public class WardrobeUI : MonoBehaviour
{
    #region SERIALIZABLE FIELDS
    [Header("Texts Stats")]
    [SerializeField] TextMeshProUGUI txt_name;
    [SerializeField] TextMeshProUGUI txt_lifePoints;
    [SerializeField] TextMeshProUGUI txt_attackSpeed; 
    [SerializeField] TextMeshProUGUI txt_movementSpeed;

    [Header("Texts After Change")]
    [SerializeField] TextMeshProUGUI txt_bonusLifePoints;
    [SerializeField] TextMeshProUGUI txt_bonusAttackSpeed;
    [SerializeField] TextMeshProUGUI txt_bonusMovementSpeed;

    [Header("Dynamic Button Texts")]
    [SerializeField] TextMeshProUGUI txt_button_BuyOrEquip;
    [SerializeField] TextMeshProUGUI txt_button_Change;

    [Header("Game Objects & Transforms")]
    [SerializeField] GameObject go_error;
    [SerializeField] GameObject go_materialsWindow;

    [Header("Colors on stats")]
    [SerializeField] Color color_buff;
    [SerializeField] Color color_deBuff;

    [Header("Materials")]
    [SerializeField] MaterialInfoUI[] materialsInfo;
    #endregion
    #region PRIVATE FIELDS
    private Coroutine currentFade;
    private GameObject currentChestClothPrefab = null;

    WardrobeMode _wardrobeMode = WardrobeMode.chestCloth;
    enum WardrobeMode { helmet = 0, chestCloth = 1}

    string _selectedHelmetID;
    string _selectedChestClothID;

    #endregion
    #region PRIVATE PROPERTIES
    WardrobeMode WardrobeModeProperty
    {
        get => _wardrobeMode;
        set
        {
            _wardrobeMode = value;
            switch (value)
            {
                case WardrobeMode.helmet:
                    OnHelmetMode();
                    break;
                case WardrobeMode.chestCloth:
                    OnChestClothMode();
                    break;
            }
        }
    }
    string Name
    {
        set => txt_name.text = value;
    }
    int LifePoints
    {
        set => txt_lifePoints.text = value.ToString();
    }
    float AttackSpeed
    {
        set
        {
            int percent = (int)(value * 100);
            txt_attackSpeed.text = "+" + percent.ToString() + "%";
        }
    }
    float MovementSpeed
    {
        set
        {
            int percent = (int)(value * 100);
            txt_movementSpeed.text = "+" + percent.ToString() + "%";
        }
    }

    int? BonusLifePoints
    {
        set
        {
            if (value.HasValue)
                txt_bonusLifePoints.text = "+" + value.ToString();
            else
                txt_bonusLifePoints.text = "";
        }
    }
    float? BonusAttackSpeed
    {
        set
        {
            if (value.HasValue)
            {
                int percent = (int)(value * 100);
                txt_bonusAttackSpeed.text = "+" + percent.ToString() + "%";
            }
            else
            {
                txt_bonusAttackSpeed.text = "";
            }
        }
    }
    float? BonusMovementSpeed
    {
        set
        {
            if (value.HasValue)
            {
                int percent = (int)(value * 100);
                txt_bonusMovementSpeed.text = "+" + percent.ToString() + "%";
            }
            else
            {
                txt_bonusMovementSpeed.text = "";
            }
        }
    }
    
    string SelectedHelmetID
    {
        get => _selectedHelmetID;
        set
        {
            _selectedHelmetID = value;
            UpdateUI();
        }
    }
    string SelectedChestClothID
    {
        get => _selectedChestClothID;
        set
        {
            _selectedChestClothID = value;
            UpdateUI();
        }
    }
    ClothStatsScripteableObject SelectedHelmet
    {
        get => EquipmentManager.Helmets[_selectedHelmetID].data;
    }
    ClothStatsScripteableObject SelectedChestCloth
    {
        get => EquipmentManager.ChestCloths[_selectedChestClothID].data;
    }
    ClothStatsScripteableObject SelectedCloth
    {
        get
        {
            switch (_wardrobeMode)
            {
                case WardrobeMode.helmet: return SelectedHelmet;
                case WardrobeMode.chestCloth: return SelectedChestCloth;
                default: return null;
            }
        }
    }
    ClothStatsScripteableObject EquippedCloth
    {
        get
        {
            switch (_wardrobeMode)
            {
                case WardrobeMode.helmet: return EquipmentManager.CurrentHelmetData;
                case WardrobeMode.chestCloth: return EquipmentManager.CurrentChestClothData;
                default: return null;
            }
        }
    }

    string[] OrderedChestCloths
    {
        get => EquipmentManager.keysOrdered_chestCloth;
    }
    string[] OrderedHelmets
    {
        get => EquipmentManager.keysOrdered_helmet;
    }


    #endregion
    #region PRIVATE FUNCS
    private void Awake()
    {
        go_error.SetActive(false);
    }
    private void OnEnable()
    {
        WardrobeModeProperty = WardrobeMode.chestCloth;
    }
    private void OnChestClothMode()
    {
        EquipmentManager.OnCurrentChestClothChange += UpdateUI;
        EquipmentManager.OnCurrentHelmetChange -= UpdateUI; //si no lo tiene asignado no hace nada
        UpdateUI();
    }
    private void OnHelmetMode()
    {
        EquipmentManager.OnCurrentChestClothChange -= UpdateUI;
        EquipmentManager.OnCurrentHelmetChange += UpdateUI;
        UpdateUI();
    }

    private void OnDisable()
    {
        EquipmentManager.OnCurrentChestClothChange -= UpdateUI;
        EquipmentManager.OnCurrentHelmetChange -= UpdateUI;
    }

    private void UpdateUI()
    {
        UpdateEquippedCloth(EquippedCloth); //refleja los stats base
        UpdateSelectedCloth(SelectedCloth); //refleja las mejoras etc

        UpdatePrice(); //precio si no esta comprado

        ActualiceModel();
    }

    void UpdateEquippedCloth(ClothStatsScripteableObject value)
    {
        Name = value.name;
       // int baseDamage = value.damage;
       // LifePoints = baseDamage;
       //// CriticDamage = (int)(baseDamage * value.critMultiplier);
       // AttackSpeed = value.attackSpeed;
       // MovementSpeed = value.critProbability;
    }
    private void UpdateSelectedCloth(ClothStatsScripteableObject value)
    {
        if (value == null)
        {
            BonusLifePoints = null;
            BonusMovementSpeed = null;
            BonusAttackSpeed = null;
            BonusMovementSpeed = null;
        }
        else
        {
            
        }
    }
    private void UpdatePrice()
    {
        bool isUnlocked = false;
        go_materialsWindow.SetActive(!isUnlocked);
        

        if (isUnlocked) return;
        
        materialsInfo[0].Amount = SelectedCloth.coinsPrice;

        for (int i = 0; i < SelectedCloth.costs.Count; i++)
        {
            if (i + 1 < materialsInfo.Length)
            {
                materialsInfo[i + 1].gameObject.SetActive(true);
                materialsInfo[i + 1].Amount = SelectedCloth.costs[i].cost;
                materialsInfo[i + 1].MaterialAssigned = SelectedCloth.costs[i].material;
            }
            else throw new Exception("Debes meter más huecos de material en el array materialsInfo");
        }
        for(int i = SelectedCloth.costs.Count; i < materialsInfo.Length - 1;i++)
        {
            materialsInfo[i + 1].gameObject.SetActive(false);
        }
    }

    private bool HaveEnoughMaterials()
    {
        bool hasEnoughMaterials = true;

        foreach (MaterialCost materialCost in SelectedCloth.costs)
        {
            bool enough = GameData.Inventory.GetAmount(materialCost.material) >= materialCost.cost;
            if (!enough) hasEnoughMaterials = false;
        }
        return hasEnoughMaterials;
    }
    private void DecreaseMaterials()
    {
        foreach (MaterialCost materialCost in SelectedCloth.costs)
        {
            bool success = GameData.Inventory.TryRemoveObject(materialCost.material, materialCost.cost);
            if (!success) throw new Exception("Not enough materials");
        }
    }
    private bool HaveEnoughCoins()
    {
        return GameData.Coins >= SelectedCloth.coinsPrice;
    }
    private void DecreaseCoins()
    {
        GameData.Coins -= SelectedCloth.coinsPrice;
    }

    private void ActualiceModel()
    {
        //if (currentModel != null) Destroy(currentModel);

        //currentModel = Instantiate(EquipmentManager.CurrentPickaxe.model);
        
        //currentModel.transform.parent = tr_pickaxeModelPosition;
        //currentModel.transform.localPosition = new Vector3();
        //currentModel.transform.localEulerAngles = new Vector3();
        //currentModel.transform.localScale = new Vector3(1,1,1);
    }

    private void ChangeSelectedCloth(int placesToMove)
    {
        int currentIndex, numberOfElements, nextIndex;
        switch (_wardrobeMode)
        {
            case WardrobeMode.helmet:
                currentIndex = EquipmentManager.GetOrderHelmet(SelectedHelmetID);
                numberOfElements = EquipmentManager.NumberOfHelmets;

                nextIndex = (currentIndex + placesToMove + numberOfElements) % numberOfElements;
                SelectedHelmetID = OrderedHelmets[nextIndex];
                break;
            case WardrobeMode.chestCloth:
                currentIndex = EquipmentManager.GetOrderChestCloth(SelectedChestClothID);
                numberOfElements = EquipmentManager.NumberOfChestCloths;

                nextIndex = (currentIndex + placesToMove + numberOfElements) % numberOfElements;
                SelectedChestClothID = OrderedChestCloths[EquipmentManager.GetOrderChestCloth(_selectedChestClothID)];
                break;
        }
    }
    private void ShowErrorMessage()
    {
        // Detener fade anterior y comenzar uno nuevo
        if (currentFade != null)
            StopCoroutine(currentFade);

        currentFade = StartCoroutine(FadeSequenceCoroutine());
    }
    private IEnumerator FadeSequenceCoroutine()
    {
        if (!go_error.activeInHierarchy)
        {
            go_error.SetActive(true);
        }
        TextMeshProUGUI textToFade = go_error.GetComponent<TextMeshProUGUI>();
        Color originalColor = textToFade.color;
        textToFade.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

        yield return null;//esperar un frame para q se active sin problemas

        float duration = 2f;
        float interval = 0.2f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            textToFade.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            elapsedTime += interval;
            yield return new WaitForSeconds(interval);
        }

        textToFade.gameObject.SetActive(false);

        currentFade = null;
    }
    #endregion
    #region PUBLIC FUNCS
    public void OnButtonBuyOrEquip()
    {
        //if (IsMaxed) return;
        //if (HaveEnoughMaterials() && HaveEnoughCoins())
        //{
        //    //restar costo
        //    DecreaseMaterials();
        //    DecreaseCoins();
        //    //subir el nivel del arma
        //    EquipmentManager.PickAxeLevel++;
        //}
        //else
        //{
        //    ShowErrorMessage();
        //}
        ////La ui deberia actualizarse sola al recibir callback de equipment manager
    }
    public void OnButtonChange()
    {
        int currentMode = (int)_wardrobeMode;
        int nextMode = (currentMode + 1)%2;
        WardrobeModeProperty = (WardrobeMode)nextMode;
    }
    public void OnButtonLeftArrow()
    {
        ChangeSelectedCloth(-1);
    }
    public void OnButtonRightArrow()
    {
        ChangeSelectedCloth(1);
    }
    #endregion
}