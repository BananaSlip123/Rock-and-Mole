using UnityEngine;
using TMPro;
using PickaxeStats;
using System;
using System.Collections.Generic;
using System.Collections;
public class ForgeUI : MonoBehaviour
{
    #region SERIALIZABLE FIELDS
    [Header("Texts Stats")]
    [SerializeField] TextMeshProUGUI txt_name;
    [SerializeField] TextMeshProUGUI txt_baseDamage;
    [SerializeField] TextMeshProUGUI txt_criticDamage;
    [SerializeField] TextMeshProUGUI txt_attackSpeed;
    [SerializeField] TextMeshProUGUI txt_criticProbability;

    [Header("Texts After Upgrade")]
    [SerializeField] TextMeshProUGUI txt_bonusBaseDamage;
    [SerializeField] TextMeshProUGUI txt_bonusCriticDamage;
    [SerializeField] TextMeshProUGUI txt_bonusAttackSpeed;
    [SerializeField] TextMeshProUGUI txt_bonusCriticProbability;

    [Header("Game Objects & Transforms")]
    [SerializeField] Transform tr_pickaxeModelPosition;
    [SerializeField] GameObject go_error;
    [SerializeField] GameObject go_maxLevelWindow;
    [SerializeField] GameObject go_materialsWindow;
    [SerializeField] GameObject go_buyButton;

    [Header("Materials")]
    [SerializeField] MaterialInfoUI[] materialsInfo;
    #endregion
    #region PRIVATE FIELDS
    private Coroutine currentFade;
    private GameObject currentModel = null;
    #endregion
    #region PRIVATE PROPERTIES
    string Name
    {
        set => txt_name.text = value;
    }
    int BaseDamage
    {
        set => txt_baseDamage.text = value.ToString();
    }
    int CriticDamage
    {
        set => txt_criticDamage.text = value.ToString();
    }
    float AttackSpeed
    {
        set
        {
            int percent = (int)(value * 100);
            txt_attackSpeed.text = "+" + percent.ToString() + "%";
        }
    }
    float CriticProbability
    {
        set
        {
            int percent = (int)(value * 100);
            txt_criticProbability.text = "+" + percent.ToString() + "%";
        }
    }

    int? BonusBaseDamage
    {
        set
        {
            if (value.HasValue)
                txt_bonusBaseDamage.text = "+" + value.ToString();
            else
                txt_bonusBaseDamage.text = "";
        }
    }
    int? BonusCriticDamage
    {
        set
        {
            if (value.HasValue)
                txt_bonusCriticDamage.text = "+" + value.ToString();
            else
                txt_bonusCriticDamage.text = "";
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
    float? BonusCriticProbability
    {
        set
        {
            if (value.HasValue)
            {
                int percent = (int)(value * 100);
                txt_bonusCriticProbability.text = "+" + percent.ToString() + "%";
            }
            else
            {
                txt_bonusCriticProbability.text = "";
            }
        }
    }
    PickaxeStatsScripteableObject CurrentPickaxe
    {
        get => EquipmentManager.Pickaxes[CurrentLevel].data;
    }
    PickaxeStatsScripteableObject NextLevelPickaxe
    {
        get
        {
            if (IsMaxed) return null;
            return EquipmentManager.Pickaxes[CurrentLevel + 1].data;
        }
    }
    int CurrentLevel
    {
        get => EquipmentManager.PickAxeLevel; //lee valor actual de la clase
    }
    int MaxLevel
    {
        get => EquipmentManager.MaxLevel; //lee valor actual de la clase
    }
    bool IsMaxed
    {
        get => CurrentLevel == MaxLevel;
    }
    #endregion
    #region PRIVATE FUNCS
    private void Awake()
    {
        go_error.SetActive(false);
    }
    private void OnEnable()
    {
        UpdateUI();

        EquipmentManager.OnPickaxeLevelChange += UpdateUI;
    }

    private void OnDisable()
    {
        EquipmentManager.OnPickaxeLevelChange -= UpdateUI;
    }

    private void UpdateUI()
    {
        UpdateCurrentPickaxe(CurrentPickaxe);
        UpdateNextLevelPickaxe(NextLevelPickaxe);
        UpdatePrice();
        ActualiceModel();
    }

    void UpdateCurrentPickaxe(PickaxeStatsScripteableObject value)
    {
        Name = value.name;
        int baseDamage = value.damage;
        BaseDamage = baseDamage;
        CriticDamage = (int)(baseDamage * value.critMultiplier);
        AttackSpeed = value.attackSpeed;
        CriticProbability = value.critProbability;
    }
    private void UpdateNextLevelPickaxe(PickaxeStatsScripteableObject value)
    {
        if (value == null)
        {
            BonusBaseDamage = null;
            BonusCriticDamage = null;
            BonusAttackSpeed = null;
            BonusCriticProbability = null;
        }
        else
        {
            int currentDamage = CurrentPickaxe.damage;
            int bonus = value.damage - currentDamage;
            BonusBaseDamage = bonus;
            BonusCriticDamage = (int)(value.damage * value.critMultiplier - currentDamage * CurrentPickaxe.critMultiplier);
            BonusAttackSpeed = value.attackSpeed - CurrentPickaxe.attackSpeed;
            BonusCriticProbability = value.critProbability - CurrentPickaxe.critProbability;
        }
    }
    private void UpdatePrice()
    {
        bool isMaxed = IsMaxed;

        go_maxLevelWindow.SetActive(isMaxed);
        go_materialsWindow.SetActive(!isMaxed);
        go_buyButton.SetActive(!isMaxed);

        if (isMaxed) return;
        
        materialsInfo[0].Amount = NextLevelPickaxe.coinsPrice;

        for (int i = 0; i < NextLevelPickaxe.costs.Count; i++)
        {
            if (i + 1 < materialsInfo.Length)
            {
                materialsInfo[i + 1].gameObject.SetActive(true);
                materialsInfo[i + 1].Amount = NextLevelPickaxe.costs[i].cost;
                materialsInfo[i + 1].MaterialAssigned = NextLevelPickaxe.costs[i].material;
            }
            else throw new Exception("Debes meter más huecos de material en el array materialsInfo");
        }
        for(int i = NextLevelPickaxe.costs.Count; i < materialsInfo.Length - 1;i++)
        {
            materialsInfo[i + 1].gameObject.SetActive(false);
        }
    }

    private bool HaveEnoughMaterials()
    {
        bool hasEnoughMaterials = true;

        foreach (MaterialCost materialCost in NextLevelPickaxe.costs)
        {
            bool enough = GameData.Inventory.GetAmount(materialCost.material) >= materialCost.cost;
            if (!enough) hasEnoughMaterials = false;
        }
        return hasEnoughMaterials;
    }
    private void DecreaseMaterials()
    {
        foreach (MaterialCost materialCost in NextLevelPickaxe.costs)
        {
            bool success = GameData.Inventory.TryRemoveObject(materialCost.material, materialCost.cost);
            if (!success) throw new Exception("Not enough materials");
        }
    }
    private bool HaveEnoughCoins()
    {
        return GameData.Coins >= NextLevelPickaxe.coinsPrice;
    }
    private void DecreaseCoins()
    {
        GameData.Coins -= NextLevelPickaxe.coinsPrice;
    }

    private void ActualiceModel()
    {
        if (currentModel != null) Destroy(currentModel);

        currentModel = Instantiate(EquipmentManager.CurrentPickaxe.model);
        
        currentModel.transform.parent = tr_pickaxeModelPosition.parent;
        currentModel.transform.localPosition = tr_pickaxeModelPosition.localPosition;
        currentModel.transform.localRotation = tr_pickaxeModelPosition.localRotation;
        currentModel.transform.localScale = tr_pickaxeModelPosition.localScale;
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
    public void OnBuyButtonPressed()
    {
        if (IsMaxed) return;
        if (HaveEnoughMaterials() && HaveEnoughCoins())
        {
            //restar costo
            DecreaseMaterials();
            DecreaseCoins();
            //subir el nivel del arma
            EquipmentManager.PickAxeLevel++;
        }
        else
        {
            ShowErrorMessage();
        }
        //La ui deberia actualizarse sola al recibir callback de equipment manager
    }
    #endregion
}
