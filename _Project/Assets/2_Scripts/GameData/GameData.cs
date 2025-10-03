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
        }
        else
        {
            _objectsAmount.Add(name, amount);
        }
    }
    public bool TryRemoveObject(MaterialName name, int amount)
    {
        if (amount < 0) throw new Exception("Must be positive number");

        if (_objectsAmount.ContainsKey(name))
        {
            int newAmount = _objectsAmount[name] - amount;
            if (newAmount > 0)
                _objectsAmount[name] = newAmount;
            else if (newAmount == 0)
                _objectsAmount.Remove(name);
            else return false;
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