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
    static bool _init = false;
    static int? _pickAxeLevel = null;
    static int PickAxeLevel
    {
        get
        {
            if (!_pickAxeLevel.HasValue)
                _pickAxeLevel = PlayerPrefs.GetInt("PickAxeLevel", 1);
            return _pickAxeLevel.Value;
        }
        set
        {
            if (!_pickAxeLevel.HasValue || value != _pickAxeLevel.Value)
            {
                _pickAxeLevel = value;
                PlayerPrefs.SetInt("PickAxeLevel", _pickAxeLevel.Value);
                PlayerPrefs.Save();
            }
        }
    }
    public static Pickaxe[] Pickaxes; //se identifican por nivel (ya q se va mejorando el pico)
    public static SortedDictionary<string, Cloth> ChestCloths = new SortedDictionary<string, Cloth>(); //se identifican por nombre
    public static SortedDictionary<string, Cloth> Helmets = new SortedDictionary<string, Cloth>(); //se identifican por nombre
    #endregion

    #region DATA STRUCTURES
    public class Pickaxe
    {
        public PickaxeStatsScripteableObject data;
        public GameObject model;
    }
    public class Cloth
    {
        bool? _unLocked;
        public ClothStatsScripteableObject data;
        public GameObject model;
        public bool UnLocked
        {
            get
            {
                if (!_unLocked.HasValue)
                    _unLocked = PlayerPrefs.GetInt("C"+data.name, 1) == 1; //por defecto es true
                return _unLocked.Value;
            }
            set
            {
                if (!_unLocked.HasValue || value != _unLocked.Value)
                {
                    _unLocked = value;
                    int value2Int = value ? 1 : 0;
                    PlayerPrefs.SetInt("C" + data.name, value2Int);
                    PlayerPrefs.Save();
                }
            }
        }
        public Cloth(ClothStatsScripteableObject data, GameObject model)
        {
            this.data = data;
            this.model = model;
        }
    }
    #endregion
    private void Awake()
    {
        if (_init) return;

        Pickaxes = new Pickaxe[pickaxes.Length];
        for(int i = 0; i< Pickaxes.Length; i++)
        {
            Pickaxes[i].data = pickaxes[i].data;
            Pickaxes[i].model = pickaxes[i].model;
        }

        foreach(ClothAssigner chestCloth in chestCloths)
        {
            ChestCloths.Add(chestCloth.data.name,new Cloth(
                chestCloth.data,
                chestCloth.model
            ));
        }
        foreach (ClothAssigner helmet in helmets)
        {
            Helmets.Add(helmet.data.name, new Cloth(
                helmet.data,
                helmet.model
            ));
        }

        _init = true;
    }
}
