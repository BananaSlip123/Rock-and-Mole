using PickaxeStats;
using UnityEngine;

public interface IPlayerStats
{
    void ChangeClothes(ClothStatsScripteableObject newCloth);
    void ChangePickaxe(PickaxeStatsScripteableObject newPickaxe);
    public void ResetStats();
    void OnEquipmentChange();
}
