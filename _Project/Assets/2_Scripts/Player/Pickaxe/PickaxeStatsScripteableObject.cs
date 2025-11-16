using UnityEngine;
namespace PickaxeStats
{
    [CreateAssetMenu(fileName = "PickaxeStats", menuName = "Scriptable Objects/PickaxeStats")]

    public class PickaxeStatsScripteableObject : ScriptableObject
    {
        public int damage;

        public float critMultiplier;
        public float critProbability;
    }
}
