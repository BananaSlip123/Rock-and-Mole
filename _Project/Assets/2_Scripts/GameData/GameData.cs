using System;
using System.Collections.Generic;
using UnityEngine;
public static class GameData
{
    //Se encarga del guardado de datos en disco
    //acceso publico y estático, necesita dependencia de assemblies para ser usada
    //usado por algún singleton o algo
    #region PRIVATE VARS
    static bool _isLoaded = false;
    const int INVENTORY_SIZE = 12;

    static int _coins = 0;
    static Inventory _inventory = new Inventory(INVENTORY_SIZE, "INV");//q materiales y en q cantidad  tienes
    static Inventory _runInventory = new Inventory(INVENTORY_SIZE, "R_INV");//los q consigues en cada run

    #endregion
    #region PUBLIC VARS
    public readonly static Dictionary<MaterialName, int> MaterialsValues = new Dictionary<MaterialName, int>();
    public static Inventory Inventory => _inventory;
    public static Inventory RunInventory => _runInventory;
    #endregion

    #region PRIVATE FUNCS

    static void LoadData()
    {
        //usar player prefbs :)

        //CARGAR INVENTARIO
        _inventory.LoadData();
        //CARGAR MONEDAS
        _coins = PlayerPrefs.GetInt("COINS", 0);
    }

    #endregion

    #region PUBLIC FUNCS
    public static void Init()
    {
        //INICIAR VALORES
        MaterialsValues.Add(MaterialName.Hierro, 4);
        MaterialsValues.Add(MaterialName.Carbon, 8);
        MaterialsValues.Add(MaterialName.Bronce, 8);
        MaterialsValues.Add(MaterialName.Cuarzo, 15);
        MaterialsValues.Add(MaterialName.Obsidiana, 15);
        MaterialsValues.Add(MaterialName.RolloTela, 30);
        MaterialsValues.Add(MaterialName.Ambar, 8);
        MaterialsValues.Add(MaterialName.Esmeralda, 10);
        MaterialsValues.Add(MaterialName.Rubi, 15);
        MaterialsValues.Add(MaterialName.Diamante, 100);

        LoadData();
    }
    public static void SaveData()
    {
        //usar player prefbs :)

        //GUARDAR INVENTARIO
        _inventory.SaveData();

        //GUARDAR MONEDAS
        PlayerPrefs.SetInt("COINS", _coins);
    }

    #endregion
}

public class Inventory
{
    int _maxSize;
    string _name;

    //queremos q siempre q mostremos el inventario muestre un orden coherente
    SortedDictionary<MaterialName, int> _objectsAmount = new SortedDictionary<MaterialName, int>();//q materiales y en q cantidad  tienes
    public SortedDictionary<MaterialName, int> Objects => _objectsAmount;

    Action _onInventoryChange; //cuando se borra o añade un material
    Dictionary<MaterialName, Action<int>> _dict_onSlotValueChange = new Dictionary<MaterialName, Action<int>>(); //cuando cambia un valor

    public Inventory(int maxSize, string name)
    {
        if (Enum.GetValues(typeof(MaterialName)).Length > maxSize)
            throw new Exception("The size of the inventory must >= than the number of materials");

        foreach (MaterialName material in Enum.GetValues(typeof(MaterialName)))
        {
            _objectsAmount.Add(material, 0);
            _dict_onSlotValueChange.Add(material,null);
        }
        _maxSize = maxSize;
        _name = name;
    }
    public void SubscribeToInventoryChange(Action action) => _onInventoryChange += action;//se recarga la UI entera
    public void SetToSlotChange(MaterialName name, Action<int> action)//se recarga en la UI un material especifico
    {
        _dict_onSlotValueChange[name] = action;
    }
    public void CleanAllCallbacks()
    {
        _onInventoryChange = null;
        foreach (MaterialName key in _dict_onSlotValueChange.Keys) _dict_onSlotValueChange[key] = null;
    }
    
    public int GetAmount(MaterialName key) => _objectsAmount[key];
    public void AddObject(MaterialName name, int amount)
    {
        if (amount < 0) throw new Exception("Must be positive number");

        int oldVal = _objectsAmount[name];
        _objectsAmount[name] = oldVal + amount;

        if (oldVal == 0) _onInventoryChange?.Invoke();
        else _dict_onSlotValueChange[name]?.Invoke(amount);
    }
    public bool TryRemoveObject(MaterialName name, int amount)
    {
        //si tenemos 5 piedras e intentamos quitar 6 mantenemos las 5 y devolvemos false
        //se usará al comprar objetos con materiales
        if (amount < 0) throw new Exception("Must be positive number");
        
        int newAmount = _objectsAmount[name] - amount;

        if (newAmount < 0) return false;

        _objectsAmount[name] = newAmount;

        if (newAmount == 0)
            _onInventoryChange?.Invoke();
        else
            _dict_onSlotValueChange[name]?.Invoke(amount);
        
        return true;
    }

    public void SaveData()
    {
        foreach (MaterialName key in _objectsAmount.Keys)
        {
            PlayerPrefs.SetInt(_name + key.ToString(), _objectsAmount[key]);
        }
    }

    public void LoadData()
    {
        foreach (MaterialName key in _objectsAmount.Keys)
        {
            _objectsAmount[key] = PlayerPrefs.GetInt(_name + key.ToString(), 0);
        }

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