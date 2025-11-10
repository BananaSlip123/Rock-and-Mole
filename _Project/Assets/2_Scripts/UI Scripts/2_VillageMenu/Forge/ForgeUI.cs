using UnityEngine;
using TMPro;
using PickaxeStats;
using System;
using System.Collections.Generic;
using System.Collections;
public class ForgeUI : MonoBehaviour
{
    [Header("Texts Stats")]
    [SerializeField] TextMeshProUGUI txt_baseDamage;
    [SerializeField] TextMeshProUGUI txt_criticDamage;
    [SerializeField] TextMeshProUGUI txt_attackSpeed;
    [SerializeField] TextMeshProUGUI txt_criticProbability;

    [Header("Texts After Upgrade")]
    [SerializeField] TextMeshProUGUI txt_bonusBaseDamage;
    [SerializeField] TextMeshProUGUI txt_bonusCriticDamage;
    [SerializeField] TextMeshProUGUI txt_bonusAttackSpeed;
    [SerializeField] TextMeshProUGUI txt_bonusCriticProbability;

    [Header("Other")]
    [SerializeField] Transform tr_pickAxeModelPosition;
    [SerializeField] GameObject go_error;

    [Header("Materials")]
    [SerializeField] MaterialInfoUI[] materialsInfo;

    private Coroutine currentFade;
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
            txt_criticProbability.text = "+"+percent.ToString()+"%";
        }
    }

    int? BonusBaseDamage
    {
        set
        {
            if (value.HasValue)
                txt_bonusBaseDamage.text = "+"+value.ToString();
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

    void UpdateCurrentPickaxe(PickaxeStatsScripteableObject value)
    {
        int baseDamage = value.damage;
        BaseDamage = baseDamage;
        CriticDamage = (int)(baseDamage * value.critMultiplier);
        AttackSpeed = value.attackSpeed;
        CriticProbability = value.critProbability;
    }

    PickaxeStatsScripteableObject NextLevelPickaxe
    {
        //get => EquipmentManager.Pickaxes?[CurrentLevel+1]?.data;
        get => EquipmentManager.Pickaxes[CurrentLevel+1]?.data;
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
            BaseDamage = bonus;
            CriticDamage = (int)(value.damage * value.critMultiplier - currentDamage * CurrentPickaxe.critMultiplier);
            AttackSpeed = value.attackSpeed - CurrentPickaxe.attackSpeed;
            CriticProbability = value.critProbability - CurrentPickaxe.critProbability;
        }
    }
    int CurrentLevel
    {
        get => EquipmentManager.PickAxeLevel; //lee valor actual de la clase
    }

    #region PRIVATE FUNCS

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

    }
    #endregion
}
