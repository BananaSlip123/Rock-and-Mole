using UnityEngine;
using PickaxeStats;
using System.Collections.Generic;
using System;
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
    #region PRIVATE FIELDS
    static bool _init = false;
    static int? _pickAxeLevel = null;
    static string _currentHelmet = null;
    static string _currentChestCloth = null;
    static string _defaultHelmet = null;
    static string _defaultChestCloth = null;
    #endregion
    #region PUBLIC PROPERTIES
    public static int MaxLevel
    {
        get => Pickaxes.Length - 1;
    }
    public static int PickAxeLevel
    {
        get
        {
            if (!_pickAxeLevel.HasValue)
                _pickAxeLevel = PlayerPrefs.GetInt("PickAxeLevel", 0);
            return _pickAxeLevel.Value;
        }
        set
        {
            if (!_pickAxeLevel.HasValue || value != _pickAxeLevel.Value)
            {
                _pickAxeLevel = value;
                PlayerPrefs.SetInt("PickAxeLevel", value);
                PlayerPrefs.Save();
                OnPickaxeLevelChange?.Invoke();
                OnEquipmentChange?.Invoke();
            }
        }
    }
    public static string CurrentHelmet
    {
        get
        {
            if (_currentHelmet == null || _currentHelmet == "")
                _currentHelmet = PlayerPrefs.GetString("C_Helmet", _defaultHelmet);
            return _currentHelmet;
        }
        set
        {
            if (value != _currentHelmet)
            {
                _currentHelmet = value;
                PlayerPrefs.SetString("C_Helmet", value);
                PlayerPrefs.Save();
                OnCurrentHelmetChange?.Invoke(value);
                OnEquipmentChange?.Invoke();
            }
        }
    }
    public static string CurrentChestCloth
    {
        get
        {
            if (_currentChestCloth == null || _currentChestCloth == "")
                _currentChestCloth = PlayerPrefs.GetString("C_Chest", _defaultChestCloth);
            return _currentChestCloth;
        }
        set
        {
            if (value != _currentChestCloth)
            {
                _currentChestCloth = value;
                PlayerPrefs.SetString("C_Chest", value);
                PlayerPrefs.Save();
                OnCurrentChestClothChange?.Invoke(value);
                OnEquipmentChange?.Invoke();
            }
        }
    }
    public static Pickaxe CurrentPickaxe
    {
        get => Pickaxes[PickAxeLevel];
    }
    public static PickaxeStatsScripteableObject CurrentPickaxeData
    {
        get => Pickaxes[PickAxeLevel].data;
    }
    public static ClothStatsScripteableObject CurrentHelmetData
    {
        get => Helmets.ContainsKey(CurrentHelmet) ? Helmets[CurrentHelmet].data : null;
    }
    public static ClothStatsScripteableObject CurrentChestClothData
    {
        get => ChestCloths.ContainsKey(CurrentChestCloth) ? ChestCloths[CurrentChestCloth].data : null;
    }
    #endregion
    #region PUBLIC FIELDS
    #region DATA
    public static Pickaxe[] Pickaxes { get; private set; }
    //se identifican por nivel (ya q se va mejorando el pico)
    public static SortedDictionary<string, ChestCloth> ChestCloths { get; private set; } = new SortedDictionary<string, ChestCloth>();
    //se identifican por nombre
    public static SortedDictionary<string, Helmet> Helmets { get; private set; } = new SortedDictionary<string, Helmet>();
    //se identifican por nombre
    #endregion
    #region CALLBACKS
    public static Action OnPickaxeLevelChange = null;
    public static Action<string> OnCurrentChestClothChange = null;
    public static Action<string> OnCurrentHelmetChange = null;
    public static Action OnEquipmentChange = null;
    public static Action<string> OnUnlockedHelmet = null;
    public static Action<string> OnUnlockedChestCloth = null;

    #endregion

    #endregion
    #endregion

    #region DATA STRUCTURES
    public class Pickaxe
    {
        public PickaxeStatsScripteableObject data;
        public GameObject model;
        public string Name { get => data.name; }
    }
    public abstract class Cloth
    {
        bool? _unLocked;
        public ClothStatsScripteableObject data;
        public GameObject model;

        public string Name { get => data.name; }
        public bool UnLocked
        {
            get
            {
                if (!_unLocked.HasValue)
                    _unLocked = PlayerPrefs.GetInt("C"+ Name, 1) == 1; //por defecto es true
                return _unLocked.Value;
            }
            set
            {
                if (!_unLocked.HasValue || value != _unLocked.Value)
                {
                    _unLocked = value;
                    int value2Int = value ? 1 : 0;
                    PlayerPrefs.SetInt("C" + Name, value2Int);
                    PlayerPrefs.Save();
                    OnUnlockedChanged(value);
                }
            }
        }
        protected abstract void OnUnlockedChanged(bool newValue);
        public Cloth(ClothStatsScripteableObject data, GameObject model)
        {
            this.data = data;
            this.model = model;
        }
    }

    public class Helmet : Cloth
    {
        public Helmet(ClothStatsScripteableObject data, GameObject model) : base(data, model)
        {
        }

        protected override void OnUnlockedChanged(bool newValue)
        {
            OnUnlockedHelmet?.Invoke(Name);
        }
    }

    public class ChestCloth : Cloth
    {
        public ChestCloth(ClothStatsScripteableObject data, GameObject model) : base(data, model)
        {
        }

        protected override void OnUnlockedChanged(bool newValue)
        {
            OnUnlockedChestCloth?.Invoke(Name);
        }
    }
    #endregion
    private void Awake()
    {
        if (_init) return;

        Pickaxes = new Pickaxe[pickaxes.Length];
        for(int i = 0; i< Pickaxes.Length; i++)
        {
            Pickaxes[i] = new Pickaxe
            {
                data = pickaxes[i].data,
                model = pickaxes[i].model
            };
        }

        foreach(ClothAssigner chestCloth in chestCloths)
        {
            ChestCloths.Add(chestCloth.data.name,new ChestCloth(
                chestCloth.data,
                chestCloth.model
            ));
        }
        foreach (ClothAssigner helmet in helmets)
        {
            Helmets.Add(helmet.data.name, new Helmet(
                helmet.data,
                helmet.model
            ));
        }

        _defaultChestCloth = chestCloths[0].data.name;
        _defaultHelmet = helmets[0].data.name;

        _init = true;
    }
}
