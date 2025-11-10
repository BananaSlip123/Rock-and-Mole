using UnityEngine;
using TMPro;
using PickaxeStats;
using System;
using System.Collections.Generic;
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

    [Header("Materials")]
    [SerializeField] MaterialInfoUI[] materialsInfo;

    //lista de todas los picos 

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
    PickaxeStatsScripteableObject SelectedPickaxe
    {
        set
        {
            int baseDamage = value.damage;
            BaseDamage = baseDamage;
            CriticDamage = (int)(baseDamage * value.critMultiplier);
            AttackSpeed = value.attackSpeed;
            CriticProbability = value.critProbability;
        }
    }
}
