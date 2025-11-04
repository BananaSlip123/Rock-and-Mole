using PickaxeStats;
using UnityEngine;

public interface IPlayerStats
{
    void ChangeClothes();
    void ChangePickaxe(PickaxeStatsScripteableObject newPickaxe);
    public void ResetStats();
}
