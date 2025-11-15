using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public static class GameData
{
    //Se encarga del guardado de datos en disco
    //acceso publico y estático, necesita dependencia de assemblies para ser usada
    //usado por algún singleton o algo
    #region PRIVATE VARS
    const int INVENTORY_SIZE = 12;

    static int _coins = -1;
    static bool? _needsTutorial = null;
    static PersistentInventory _inventory = new PersistentInventory("INV");//q materiales y en q cantidad  tienes
    static Inventory _runInventory = new Inventory();//los q consigues en cada run

    #endregion
    #region PUBLIC VARS
    public readonly static Dictionary<MaterialName, int> MaterialsPrices = new Dictionary<MaterialName, int>
    {
        { MaterialName.Hierro, 4 },
        { MaterialName.Carbon, 8 },
        { MaterialName.Bronce, 25 },
        { MaterialName.Cuarzo, 50 },
        { MaterialName.Obsidiana, 50 },
        { MaterialName.RolloTela, 50 },
        { MaterialName.Ambar, 8 },
        { MaterialName.Esmeralda, 50 },
        { MaterialName.Rubi, 75 },
        { MaterialName.Diamante, 100 }
    };

    public readonly static Dictionary<MaterialName, MaterialRarity> MaterialsRarity = new Dictionary<MaterialName, MaterialRarity>
    {
        { MaterialName.Hierro, MaterialRarity.Common },
        { MaterialName.Carbon, MaterialRarity.Common },
        { MaterialName.Bronce, MaterialRarity.Rare },
        { MaterialName.Cuarzo, MaterialRarity.Rare },
        { MaterialName.Obsidiana, MaterialRarity.Very_Rare },
        { MaterialName.RolloTela, MaterialRarity.Very_Rare },
        { MaterialName.Ambar, MaterialRarity.Common },
        { MaterialName.Esmeralda, MaterialRarity.Common },
        { MaterialName.Rubi, MaterialRarity.Rare },
        { MaterialName.Diamante, MaterialRarity.Very_Rare }
    };

    public static PersistentInventory Inventory => _inventory;
    public static Inventory RunInventory => _runInventory;

    public static int Coins
    {
        get 
        {
            if (_coins == -1)
                _coins = PlayerPrefs.GetInt("COINS", 0);
            return _coins;
        }
        set
        {
            if(value != _coins)
            {
                _coins = value;
                OnCoinsChange?.Invoke(value);
                PlayerPrefs.SetInt("COINS", _coins);
                PlayerPrefs.Save();
            }
        }
    }
    public static Action<int> OnCoinsChange = null;
    public static bool NeedsTutorial
    {
        get
        {
            if ( ! _needsTutorial.HasValue)
                _needsTutorial = PlayerPrefs.GetInt("TUT", 1) == 1; //por defecto es true
            return _needsTutorial.Value;
        }
        set
        {
            if(!_needsTutorial.HasValue || value != _needsTutorial.Value)
            {
                _needsTutorial = value;
                int value2Int = value ?  1 : 0;
                PlayerPrefs.SetInt("TUT", value2Int);
                PlayerPrefs.Save();
            }
        }
    }
    
    #endregion

    #region PRIVATE FUNCS
    private static MaterialRarity RandomRarity()
    {
        float random = UnityEngine.Random.Range(0f, 1f);

        if (random < 0.5)
            return MaterialRarity.Common;
        else if (random < 0.85)
            return MaterialRarity.Rare;
        else
            return MaterialRarity.Very_Rare;
    }
    #endregion

    #region PUBLIC FUNCS
    public static Dictionary<MaterialName, int> Put_RunInventory_Into_Inventory(int savedPercent)
    {
        if (savedPercent > 100 || savedPercent < 0) throw new Exception("Invalid percent insert");

        Dictionary<MaterialName, int> MaterialsCollected = new Dictionary<MaterialName, int>();

        foreach(KeyValuePair<MaterialName,int> materialData in RunInventory.Objects.ToArray())
        {
            int amount = materialData.Value;

            int savedAmount = savedPercent * amount / 100;

            if(savedAmount != 0)
            {
                MaterialsCollected.Add(materialData.Key, savedAmount);
                Inventory.AddObject(materialData.Key, savedAmount);
            }

            RunInventory.Objects[materialData.Key] = 0;
        }

        return MaterialsCollected;
    }
    public static void SaveCrucialData()
    {
        //usar player prefbs :)

        //GUARDAR INVENTARIO
        _inventory.SaveData();
    }

    public static Dictionary<MaterialName, int> MaterialsChest(int amount)
    {
        Dictionary<MaterialName, int> generated = new Dictionary<MaterialName, int>();
        MaterialRarity rarity;
        MaterialName material;

        for (int i = 0; i < amount; i++)
        {
            rarity = RandomRarity();
            material = RandomMaterial(rarity);
            if (!generated.TryAdd(material, 1))
                generated[material] += 1;
        }

        return generated;
    }

    

    public static MaterialName RandomMaterial(MaterialRarity rarity)
    {
        List<MaterialName> sortedMaterials = SortedMaterialsByRarity(rarity);
        return sortedMaterials[UnityEngine.Random.Range(0,sortedMaterials.Count)];
    }

    public static List<MaterialName> SortedMaterialsByRarity(MaterialRarity rarity)
    {
        return MaterialsRarity
            .Where(pair => pair.Value == rarity)
            .Select(pair => pair.Key)
            .ToList();
    }

    public static string MaterialName2String(MaterialName name)
    {
        switch (name)
        {
            case MaterialName.Ambar:
                return "Ámbar";
            case MaterialName.Bronce:
                return "Bronce";
            case MaterialName.Carbon:
                return "Carbón";
            case MaterialName.Cuarzo:
                return "Cuarzo";
            case MaterialName.Diamante:
                return "Diamante";
            case MaterialName.Esmeralda:
                return "Esmeralda";
            case MaterialName.Hierro:
                return "Hierro";
            case MaterialName.Obsidiana:
                return "Obsidiana";
            case MaterialName.RolloTela:
                return "Rollo de Tela";
            case MaterialName.Rubi:
                return "Rubí";
            default:
                return "NotAssigned";
        }

    }
    #endregion

}

public class PersistentInventory
{
    string _name;

    //queremos q siempre q mostremos el inventario muestre un orden coherente
    SortedDictionary<MaterialName, int> _objectsAmount = new SortedDictionary<MaterialName, int>();//q materiales y en q cantidad  tienes
    public SortedDictionary<MaterialName, int> Objects => _objectsAmount;

    Action _onInventoryChange; //cuando se borra o añade un material
    // Dictionary<MaterialName, Action<int>> _dict_onSlotValueChange = new Dictionary<MaterialName, Action<int>>(); //cuando cambia un valor
    Action<MaterialName> _onMaterialDeleted;
    Action _onMaterialAdded;
    public PersistentInventory(string name)
    {
        _name = name;

        foreach (MaterialName material in Enum.GetValues(typeof(MaterialName)))
        {
            int savedValue = PlayerPrefs.GetInt(_name + material.ToString(), 0);
            _objectsAmount.Add(material, savedValue);

           // _dict_onSlotValueChange.Add(material,null);
        }
        
    }
    public void SubscribeToInventoryChange(Action action) => _onInventoryChange += action;//se recarga el inventario entero en la UI
    public void SubscribeToMaterialDeleted(Action<MaterialName> action) => _onMaterialDeleted += action;
    public void SubscribeToMaterialAdded(Action action) => _onMaterialAdded += action;

    //public void SetToSlotChange(MaterialName name, Action<int> action)//se recarga en la UI un material especifico
    //{
    //    _dict_onSlotValueChange[name] = action;
    //}
    public void CleanAllCallbacks()
    {
        _onInventoryChange = null;
        //foreach (MaterialName key in _dict_onSlotValueChange.Keys.ToList())
        //    _dict_onSlotValueChange[key] = null;
        _onMaterialDeleted = null;
        _onMaterialAdded = null;
    }
    
    public int GetAmount(MaterialName key) => _objectsAmount[key];
    public void ResetObjectAmount(MaterialName name)
    {
        int amount = GetAmount(name);
        if(amount>0)
            TryRemoveObject(name, amount);
    }
    public void AddObject(MaterialName name, int amount)
    {
        if (amount <= 0) throw new Exception("Must be positive number");

        int oldVal = _objectsAmount[name];
        _objectsAmount[name] = oldVal + amount;
        SaveMaterial(name);
        //if (oldVal == 0) _onInventoryChange?.Invoke();
        //else _dict_onSlotValueChange[name]?.Invoke(amount);
        if (oldVal == 0) _onMaterialAdded?.Invoke();
        _onInventoryChange?.Invoke();
    }
    public bool TryRemoveObject(MaterialName name, int amount)
    {
        //si tenemos 5 piedras e intentamos quitar 6 mantenemos las 5 y devolvemos false
        //se usará al comprar objetos con materiales
        if (amount <= 0) throw new Exception("Must be positive number");
        
        int newAmount = _objectsAmount[name] - amount;

        if (newAmount < 0) return false;

        _objectsAmount[name] = newAmount;
        SaveMaterial(name);
        //if (newAmount == 0)
        //    _onInventoryChange?.Invoke();
        //else
        //    _dict_onSlotValueChange[name]?.Invoke(amount);
        if (newAmount == 0) _onMaterialDeleted?.Invoke(name);
        _onInventoryChange?.Invoke();
        

        return true;
    }

    public void SaveData()
    {
        foreach (MaterialName key in _objectsAmount.Keys)
        {
            PlayerPrefs.SetInt(_name + key.ToString(), _objectsAmount[key]);
        }
        PlayerPrefs.Save();
    }

    void SaveMaterial(MaterialName material)
    {
        PlayerPrefs.SetInt(_name + material.ToString(), _objectsAmount[material]);
        PlayerPrefs.Save();
    } 
    
}

public class Inventory
{
    //queremos q siempre q mostremos el inventario muestre un orden coherente
    SortedDictionary<MaterialName, int> _objectsAmount = new SortedDictionary<MaterialName, int>();//q materiales y en q cantidad  tienes
    public SortedDictionary<MaterialName, int> Objects => _objectsAmount;

    Action _onInventoryChange; //cuando se borra o añade un material
                               // Dictionary<MaterialName, Action<int>> _dict_onSlotValueChange = new Dictionary<MaterialName, Action<int>>(); //cuando cambia un valor
    Action<MaterialName> _onMaterialDeleted;
    public Action<MaterialName, int> OnMaterialsEarned;
    Action _onMaterialAdded;
    public Inventory()
    {
        foreach (MaterialName material in Enum.GetValues(typeof(MaterialName)))
        {
            _objectsAmount.Add(material, 0);
        }

    }
    public void SubscribeToInventoryChange(Action action) => _onInventoryChange += action;//se recarga el inventario entero en la UI
    public void SubscribeToMaterialDeleted(Action<MaterialName> action) => _onMaterialDeleted += action;
    public void SubscribeToMaterialAdded(Action action) => _onMaterialAdded += action;
    public void CleanAllCallbacks()
    {
        _onInventoryChange = null;
        _onMaterialDeleted = null;
        _onMaterialAdded = null;
    }

    public int GetAmount(MaterialName key) => _objectsAmount[key];
    public void ResetObjectAmount(MaterialName name)
    {
        TryRemoveObject(name, GetAmount(name));
    }
    public void AddObject(MaterialName name, int amount)
    {
        if (amount <= 0) throw new Exception("Must be positive number");

        OnMaterialsEarned?.Invoke(name,amount);

        int oldVal = _objectsAmount[name];
        _objectsAmount[name] = oldVal + amount;

        if (oldVal == 0) _onMaterialAdded?.Invoke();
        _onInventoryChange?.Invoke();
    }
    public bool TryRemoveObject(MaterialName name, int amount)
    {
        //si tenemos 5 piedras e intentamos quitar 6 mantenemos las 5 y devolvemos false
        //se usará al comprar objetos con materiales
        if (amount <= 0) throw new Exception("Must be positive number");

        int newAmount = _objectsAmount[name] - amount;

        if (newAmount < 0) return false;

        _objectsAmount[name] = newAmount;
        
        if (newAmount == 0) _onMaterialDeleted?.Invoke(name);
        _onInventoryChange?.Invoke();

        return true;
    }
}
public enum MaterialName
{
    Hierro,
    Carbon,
    Bronce,
    RolloTela,
    Cuarzo,
    Obsidiana,
    Ambar,
    Esmeralda,
    Rubi,
    Diamante,
}

public enum MaterialRarity
{
    Common,
    Rare,
    Very_Rare
}

