using UnityEngine;

[CreateAssetMenu(fileName = "SO_DoorsProbabilities", menuName = "Scriptable Objects/SO_DoorsProbabilities")]
public class SO_DoorsProbabilities : ScriptableObject
{
    [SerializeField] public float combatProb;
    [SerializeField] public float miningProb;
    [SerializeField] public float eventProb;

    [SerializeField] public float campamentProb;
    [SerializeField] public float rescueProb;
    [SerializeField] public float tresaureProb;
    [SerializeField] public float darkProb;
}
