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
    public readonly static Dictionary<MaterialNames, int> MaterialsValues = new Dictionary<MaterialNames, int>();

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
        MaterialsValues.Add(MaterialNames.Hierro, 4);
        MaterialsValues.Add(MaterialNames.Carbon, 8);
        MaterialsValues.Add(MaterialNames.Bronce, 8);
        MaterialsValues.Add(MaterialNames.Cuarzo, 15);
        MaterialsValues.Add(MaterialNames.Obsidiana, 15);
        MaterialsValues.Add(MaterialNames.RolloTela, 30);
        MaterialsValues.Add(MaterialNames.Ambar, 8);
        MaterialsValues.Add(MaterialNames.Esmeralda, 10);
        MaterialsValues.Add(MaterialNames.Rubi, 15);
        MaterialsValues.Add(MaterialNames.Diamante, 100);

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
    static Dictionary<MaterialNames, int> _objectsAmount = new Dictionary<MaterialNames, int>();//q materiales y en q cantidad  tienes
    public Inventory(int maxSize, string name)
    {
        _maxSize = maxSize;
        _name = name;
    }
    public void AddObject(MaterialNames name, int amount)
    {
        int oldAmount = 0;
        _objectsAmount.TryGetValue(name, out oldAmount);

        int newAmount = oldAmount + amount;
        if (newAmount > 0)
            _objectsAmount[name] = newAmount;
        else
            _objectsAmount.Remove(name);
    }

    public void SaveData()
    {
        foreach (MaterialNames key in _objectsAmount.Keys)
        {
            PlayerPrefs.SetInt(_name + (int)key, _objectsAmount[key]);
        }
    }

    public void LoadData()
    {
        foreach (MaterialNames key in _objectsAmount.Keys)
        {
            _objectsAmount[key] = PlayerPrefs.GetInt(_name + (int)key, 0);
        }

    }
}

public enum MaterialNames
{
    Hierro = 0,
    Carbon = 1,
    Bronce = 2,
    Cuarzo = 3,
    Obsidiana = 4,
    RolloTela = 5,
    Ambar = 6,
    Esmeralda = 7,
    Rubi = 8,
    Diamante = 9,
}