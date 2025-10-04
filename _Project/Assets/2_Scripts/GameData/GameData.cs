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
    Dictionary<MaterialName, Action<int>> _dict_onSlotValueChange; //cuando cambia un valor

    public void SubscribeToInventoryChange(Action action) => _onInventoryChange += action;//se recarga la UI entera
    public void SetToSlotChange(MaterialName name, Action<int> action)//se recarga en la UI un material especifico
    {
        if (_dict_onSlotValueChange.ContainsKey(name))
            _dict_onSlotValueChange[name] = action;
        else
            _dict_onSlotValueChange.Add(name, action);
    }
    public void CleanAllCallbacks()
    {
        _onInventoryChange = null;
        foreach (MaterialName key in _dict_onSlotValueChange.Keys) _dict_onSlotValueChange[key] = null;
    }
    public Inventory(int maxSize, string name)
    {
        _maxSize = maxSize;
        _name = name;
    }
    public int GetAmount(MaterialName key) => _objectsAmount[key];
    public void AddObject(MaterialName name, int amount)
    {
        if (amount < 0) throw new Exception("Must be positive number");

        if (_objectsAmount.ContainsKey(name))
        {
            _objectsAmount[name] += amount;
            _dict_onSlotValueChange[name].Invoke(amount);
        }
        else
        {
            _objectsAmount.Add(name, amount);
            _onInventoryChange?.Invoke();
        }
    }
    public bool TryRemoveObject(MaterialName name, int amount)
    {
        //si tenemos 5 piedras e intentamos quitar 6 mantenemos las 5 y devolvemos false
        //se usará al comprar objetos con materiales
        if (amount < 0) throw new Exception("Must be positive number");

        if (!_objectsAmount.ContainsKey(name)) return false;
        
        int newAmount = _objectsAmount[name] - amount;
        if (newAmount > 0)
        {
            _objectsAmount[name] = newAmount;
            _dict_onSlotValueChange[name].Invoke(amount);
        }
        else if (newAmount == 0)
        {
            _objectsAmount.Remove(name);
            _dict_onSlotValueChange.Remove(name); //no queremos callback para el material en la ui ya q no existe
            _onInventoryChange?.Invoke(); //avisamos de q se actualize el inventario entero
        }
        else return false;

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