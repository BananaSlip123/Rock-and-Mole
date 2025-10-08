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
    static bool _isLoaded = false;
    const int INVENTORY_SIZE = 12;

    static int _coins = -1;
    static PersistentInventory _inventory = new PersistentInventory("INV");//q materiales y en q cantidad  tienes
    static Inventory _runInventory = new Inventory();//los q consigues en cada run

    #endregion
    #region PUBLIC VARS
    public readonly static Dictionary<MaterialName, int> MaterialsPrices = new Dictionary<MaterialName, int>
    {
        { MaterialName.Hierro, 4 },
        { MaterialName.Carbon, 8 },
        { MaterialName.Bronce, 8 },
        { MaterialName.Cuarzo, 15 },
        { MaterialName.Obsidiana, 15 },
        { MaterialName.RolloTela, 30 },
        { MaterialName.Ambar, 8 },
        { MaterialName.Esmeralda, 10 },
        { MaterialName.Rubi, 15 },
        { MaterialName.Diamante, 100 }
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
    #endregion

    #region PRIVATE FUNCS

    #endregion

    #region PUBLIC FUNCS
    public static Dictionary<MaterialName, int> Put_RunInventory_Into_Inventory(int savedPercent)
    {
        if (savedPercent > 100 || savedPercent < 0) throw new Exception("Invalid percent insert");

        Dictionary<MaterialName, int> MaterialsCollected = new Dictionary<MaterialName, int>();

        foreach(KeyValuePair<MaterialName,int> materialData in RunInventory.Objects)
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
    }
    
    public int GetAmount(MaterialName key) => _objectsAmount[key];
    public void AddObject(MaterialName name, int amount)
    {
        if (amount < 0) throw new Exception("Must be positive number");

        int oldVal = _objectsAmount[name];
        _objectsAmount[name] = oldVal + amount;
        SaveMaterial(name);
        //if (oldVal == 0) _onInventoryChange?.Invoke();
        //else _dict_onSlotValueChange[name]?.Invoke(amount);
        _onInventoryChange?.Invoke();
    }
    public bool TryRemoveObject(MaterialName name, int amount)
    {
        //si tenemos 5 piedras e intentamos quitar 6 mantenemos las 5 y devolvemos false
        //se usará al comprar objetos con materiales
        if (amount < 0) throw new Exception("Must be positive number");
        
        int newAmount = _objectsAmount[name] - amount;

        if (newAmount < 0) return false;

        _objectsAmount[name] = newAmount;
        SaveMaterial(name);
        //if (newAmount == 0)
        //    _onInventoryChange?.Invoke();
        //else
        //    _dict_onSlotValueChange[name]?.Invoke(amount);
        if (newAmount == 0) _onMaterialDeleted.Invoke(name);
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
    public Inventory()
    {
        foreach (MaterialName material in Enum.GetValues(typeof(MaterialName)))
        {
            _objectsAmount.Add(material, 0);
        }

    }
    public void SubscribeToInventoryChange(Action action) => _onInventoryChange += action;//se recarga el inventario entero en la UI
    public void SubscribeToMaterialDeleted(Action<MaterialName> action) => _onMaterialDeleted += action;
    public void CleanAllCallbacks()
    {
        _onInventoryChange = null;
    }

    public int GetAmount(MaterialName key) => _objectsAmount[key];
    public void AddObject(MaterialName name, int amount)
    {
        if (amount < 0) throw new Exception("Must be positive number");

        int oldVal = _objectsAmount[name];
        _objectsAmount[name] = oldVal + amount;
        _onInventoryChange?.Invoke();
    }
    public bool TryRemoveObject(MaterialName name, int amount)
    {
        //si tenemos 5 piedras e intentamos quitar 6 mantenemos las 5 y devolvemos false
        //se usará al comprar objetos con materiales
        if (amount < 0) throw new Exception("Must be positive number");

        int newAmount = _objectsAmount[name] - amount;

        if (newAmount < 0) return false;

        _objectsAmount[name] = newAmount;
        
        if (newAmount == 0) _onMaterialDeleted.Invoke(name);
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