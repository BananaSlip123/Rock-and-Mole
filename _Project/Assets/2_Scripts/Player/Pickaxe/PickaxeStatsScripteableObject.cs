using System.Collections.Generic;
using UnityEngine;
namespace PickaxeStats
{
    [CreateAssetMenu(fileName = "PickaxeStats", menuName = "Scriptable Objects/PickaxeStats")]

    public class PickaxeStatsScripteableObject : ScriptableObject
    {
        public int damage;

        public float critMultiplier;
        public float critProbability;

        public float attackSpeed;

        public int coinsPrice = 0;
        public List<MaterialCost> costs = new List<MaterialCost>();
    }
}
