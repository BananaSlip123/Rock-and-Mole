using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClothStatsScripteableObject", menuName = "Scriptable Objects/ClothStatsScripteableObject")]
public class ClothStatsScripteableObject : ScriptableObject
{
    public List<ModifierStats> modifiers = new List<ModifierStats>();
}

public enum Stats
{
    health,
    damage,
    speed,
    critMultiplier,
    critProbability,
}

[System.Serializable]
public struct ModifierStats
{
    public Stats stat;
    public float value; 
}
