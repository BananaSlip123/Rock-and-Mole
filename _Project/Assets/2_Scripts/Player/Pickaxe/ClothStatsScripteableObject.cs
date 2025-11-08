using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClothStatsScripteableObject", menuName = "Scriptable Objects/ClothStatsScripteableObject")]
public class ClothStatsScripteableObject : ScriptableObject
{
    public List<ModifierStats> modifiers = new List<ModifierStats>();

    public int coinsPrice = 0;
    public List<MaterialCost> costs = new List<MaterialCost>();
}

public enum Stats
{
    health,
    damage,
    speed,
    critMultiplier,
    critProbability,
    attackSpeed
}

[System.Serializable]
public struct ModifierStats
{
    public Stats stat;
    public float value; 
}

[System.Serializable]
public struct MaterialCost
{
    public MaterialName material;
    public int cost;
}