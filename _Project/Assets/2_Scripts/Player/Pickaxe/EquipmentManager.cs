using UnityEngine;
using PickaxeStats;
using System.Collections.Generic;
public class EquipmentManager : MonoBehaviour
{
    //Se coloca en el main
    //se le asignan las referencias y las guarda de forma estática
    
    #region SERIALIZABLE FIELDS
    [System.Serializable]
    public struct PickaxeAssigner
    {
        public PickaxeStatsScripteableObject data;
        public GameObject model;
    }
    [System.Serializable]
    public struct ClothAssigner
    {
        public ClothStatsScripteableObject data;
        public GameObject model;
    }

    [SerializeField] PickaxeAssigner[] pickaxes;
    [SerializeField] ClothAssigner[] chestCloths;
    [SerializeField] ClothAssigner[] helmets;
    #endregion

    #region STATIC FIELDS

    public static PickaxeAssigner[] Pickaxes; //se identifican por nivel (ya q se va mejorando el pico)
    public static SortedDictionary<string, ClothAssigner> ChestCloths = new SortedDictionary<string, ClothAssigner>(); //se identifican por nombre
    public static SortedDictionary<string, ClothAssigner> Helmets = new SortedDictionary<string, ClothAssigner>(); //se identifican por nombre
    #endregion
    private void Awake()
    {
        Pickaxes = new PickaxeAssigner[pickaxes.Length];
        pickaxes.CopyTo(Pickaxes, 0);

        foreach(ClothAssigner chestCloth in chestCloths)
        {
            ChestCloths.Add(chestCloth.data.name,chestCloth);
        }
        foreach (ClothAssigner helmet in helmets)
        {
            Helmets.Add(helmet.data.name, helmet);
        }
    }
}
