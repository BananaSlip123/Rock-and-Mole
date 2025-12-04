using System;
using System.Collections.Generic;
using UnityEngine;

public static class BiomeManager
{
    public static UnlockedBiomes unlockedBiomes;

    private static BiomeName? _currentBiome;
    public static BiomeName CurrentBiome
    {
        get;
        set;
    }

}
public class UnlockedBiomes
{
    private Dictionary<BiomeName, bool?> biomesUnlocked { get; set; }//persistencia y métodos para usarlo

    public bool this[int index]
    {
        get
        {
            throw new NotImplementedException();
        }
    }
}
public enum BiomeName
{
    starterMines,
    undergroundForest,
}
