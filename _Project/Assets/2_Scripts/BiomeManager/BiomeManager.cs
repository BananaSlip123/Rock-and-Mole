using System.Collections.Generic;
using UnityEngine;

public static class BiomeManager
{
    public static UnlockedBiomes unlockedBiomes;
    public static int NUMBER_OF_BIOMES = typeof(BiomeName).GetEnumNames().Length;

    private static BiomeName? _currentBiome;
    public static BiomeName CurrentBiome
    {
        get
        {
            if (!_currentBiome.HasValue)
                _currentBiome = (BiomeName)PlayerPrefs.GetInt("CURRENT_BIOME", (int)BiomeName.starterMines);
            return _currentBiome.Value;
        }
        set
        {
            if (!unlockedBiomes[value])
            {
                Debug.Log("No puedes seleccionar un bioma no desbloqueado");
                return;
            }
            _currentBiome = value;
            PlayerPrefs.SetInt("CURRENT_BIOME", (int)value);

        }
    }

    public static string BiomeNameToString()
    {
        return BiomeNameToString(CurrentBiome);
    }
    public static string BiomeNameToString(BiomeName biome)
    {
        switch (biome)
        {
            case BiomeName.starterMines:
                return "Minas de principiante";
            case BiomeName.undergroundForest:
                return "El bosque subterráneo";
            default:
                return "UNDEFINED biome name";
        }
    }

}
public class UnlockedBiomes
{
    private Dictionary<BiomeName, bool> biomesUnlockedCache { get; set; }

    public UnlockedBiomes()
    {
        biomesUnlockedCache = new Dictionary<BiomeName, bool>();
    }
    public bool this[BiomeName key]
    {
        get
        {
            if (!biomesUnlockedCache.ContainsKey(key))
            {
                switch (key)
                {
                    case BiomeName.starterMines:
                        biomesUnlockedCache.Add(key, true); //siempre desbloqueado
                        break;
                    default:
                        biomesUnlockedCache.Add(key, PlayerPrefs.GetInt("BIOME_UNLOCKED_" + (int)key, 0) == 1); //por defecto es false
                        break;
                }
            }
            return biomesUnlockedCache[key];
        }
        set
        {
            if(!biomesUnlockedCache.ContainsKey(key))
                biomesUnlockedCache.Add(key, value);
            else
                biomesUnlockedCache[key] = value;

            int value2Int = value ? 1 : 0;
            PlayerPrefs.SetInt("BIOME_UNLOCKED_" + (int)key, value2Int);
            PlayerPrefs.Save();
            
        }
    }
}
public enum BiomeName
{
    starterMines = 0,
    undergroundForest = 1,
}
