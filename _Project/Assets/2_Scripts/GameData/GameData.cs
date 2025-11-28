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
    static PersistentInventory _cartInventory = new PersistentInventory("CART");//los q consigues en cada run
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
        { MaterialName.Bronce, MaterialRarity.Common },
        { MaterialName.Carbon, MaterialRarity.Rare },
        { MaterialName.Cuarzo, MaterialRarity.Rare },
        { MaterialName.Ambar, MaterialRarity.Rare },
        { MaterialName.Esmeralda, MaterialRarity.Rare },
        { MaterialName.RolloTela, MaterialRarity.Very_Rare },
        { MaterialName.Obsidiana, MaterialRarity.Very_Rare },
        { MaterialName.Rubi, MaterialRarity.Very_Rare },
        { MaterialName.Diamante, MaterialRarity.Very_Rare }
    };

    public readonly static Dictionary<EnemyName, List<MaterialName>> MaterialsByEnemy = new Dictionary<EnemyName, List<MaterialName>>
    {
        { EnemyName.Mouse,new List<MaterialName>() { MaterialName.Ambar, MaterialName.Hierro, MaterialName.Bronce, MaterialName.Carbon, MaterialName.Cuarzo, MaterialName.RolloTela } },
        { EnemyName.Bunny, new List<MaterialName>() { MaterialName.Ambar, MaterialName.Hierro, MaterialName.Bronce, MaterialName.Carbon, MaterialName.Rubi, MaterialName.Obsidiana }},
        { EnemyName.Golem, new List<MaterialName>() { MaterialName.Ambar, MaterialName.Hierro, MaterialName.Carbon, MaterialName.Esmeralda, MaterialName.Bronce } },
        { EnemyName.GolemBoss,new List<MaterialName>() { MaterialName.Rubi, MaterialName.Esmeralda, MaterialName.Hierro, MaterialName.Cuarzo, MaterialName.Obsidiana, MaterialName.Bronce } }
    };

    public static PersistentInventory Inventory => _inventory;
    public static PersistentInventory CartInventory => _cartInventory;
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

        if (random < 0.7)
            return MaterialRarity.Common;
        else if (random < 0.90)
            return MaterialRarity.Rare;
        else
            return MaterialRarity.Very_Rare;
    }

    static MaterialName EnemyMaterial(EnemyName type, MaterialRarity rarity)
    {
        List<MaterialName> sortedMaterials;
        do
        {
            sortedMaterials = SortedMaterialsByRarityAndEnemy(rarity, type);
            Debug.Log(sortedMaterials.Count);
        } while (sortedMaterials.Count == 0);
        return sortedMaterials[UnityEngine.Random.Range(0, sortedMaterials.Count)];
    }

    static MaterialName RandomMaterial(MaterialRarity rarity)
    {
        List<MaterialName> sortedMaterials = SortedMaterialsByRarity(rarity);
        return sortedMaterials[UnityEngine.Random.Range(0, sortedMaterials.Count)];
    }

    private static int RandomAmount(MaterialRarity rarity)
    {
        switch (rarity)
        {
            case MaterialRarity.Common: return UnityEngine.Random.Range(3, 5);
            case MaterialRarity.Rare: return UnityEngine.Random.Range(1, 3);
            case MaterialRarity.Very_Rare: return 1;
            default: return 1;
        }
    }
    static List<MaterialName> SortedMaterialsByRarity(MaterialRarity rarity)
    {
        return MaterialsRarity
            .Where(pair => pair.Value == rarity)
            .Select(pair => pair.Key)
            .ToList();
    }

    static List<MaterialName> SortedMaterialsByRarityAndEnemy(MaterialRarity rarity, EnemyName type)
    {
        return MaterialsByEnemy[type]
            .Where(material => MaterialsRarity.TryGetValue(material, out var matRarity) && matRarity == rarity)
            .ToList();
    }

    #endregion

    #region PUBLIC FUNCS
    public static SortedDictionary<MaterialName, int> Put_RunInventory_Into_Inventory(int savedPercent)
    {
        if (savedPercent > 100 || savedPercent < 0) throw new Exception("Invalid percent insert");

        SortedDictionary<MaterialName, int> MaterialsCollected = new SortedDictionary<MaterialName, int>();

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

   // public static Dictionary<MaterialName, int> MaterialsChest(int amount)
    public static void MaterialsChest(int amount)
    {
        //Dictionary<MaterialName, int> generated = new Dictionary<MaterialName, int>();
        MaterialRarity rarity;
        MaterialName material;
        int materialAmount;
        for (int i = 0; i < amount; i++)
        {
            rarity = RandomRarity();
            material = RandomMaterial(rarity);
            materialAmount = RandomAmount(rarity);

            //if (!generated.TryAdd(material, materialAmount))
            //    generated[material] += materialAmount;
            RunInventory.AddObject(material,materialAmount);
        }

       // return generated;
    }

    public static void MaterialsRock(MaterialName material)
    {
        int materialAmount = RandomAmount(MaterialsRarity[material]);
        RunInventory.AddObject(material, materialAmount);
    }

    //public static Dictionary<MaterialName, int> EnemyLoot(int amount, EnemyName type)
    public static void EnemyLoot(int amount, EnemyName type)
    {
        Dictionary<MaterialName, int> generated = new Dictionary<MaterialName, int>();
        MaterialRarity rarity;
        MaterialName material;
        int materialAmount;

        for (int i = 0; i < amount; i++)
        {
            rarity = RandomRarity();
            material = EnemyMaterial(type,rarity);
            materialAmount = RandomAmount(rarity);
            //if (!generated.TryAdd(material, 1))
            //    generated[material] += 1;
            RunInventory.AddObject(material, materialAmount);
        }

        //return generated;
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

public readonly struct PairMaterialAmount
{
    public MaterialName materialName { get; }
    public int amount { get; }
    public PairMaterialAmount(MaterialName materialName, int amount)
    {
        this.materialName = materialName;
        this.amount = amount;
    }
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
        }
        
    }

    public bool IsEmpty
    {
        get
        {
            if (Objects == null || Objects.Count == 0) return true;

            foreach (int amount in Objects.Values)
                if (amount > 0) return false;
            

            return true;
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
    public void TransferObjects(Inventory inventory)
    {
        foreach (KeyValuePair<MaterialName, int> objectToAdd in inventory.Objects)
        {
            if (objectToAdd.Value > 0)
            {
                AddObject(objectToAdd.Key, objectToAdd.Value);
            }
        }
        inventory.RemoveAll();
    }
    public void TransferObjects(PersistentInventory inventory)
    {
        foreach (KeyValuePair<MaterialName, int> objectToAdd in inventory.Objects)
        {
            if(objectToAdd.Value > 0)
            {
                AddObject(objectToAdd.Key, objectToAdd.Value);
            }
        }
        inventory.RemoveAll();
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
        if (amount <= 0) return false;

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

    void SaveMaterial(MaterialName material)
    {
        PlayerPrefs.SetInt(_name + material.ToString(), _objectsAmount[material]);
        PlayerPrefs.Save();
    } 

    public void RemoveAll()
    {
        Objects.Keys.ToList().ForEach(key => Objects[key] = 0);

        foreach(MaterialName material in Objects.Keys)
            SaveMaterial(material);

        _onInventoryChange?.Invoke();
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
    public bool IsEmpty
    {
        get
        {
            if (Objects == null || Objects.Count == 0) return true;

            foreach (int amount in Objects.Values)
                if (amount > 0) return false;


            return true;
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
        int amount = GetAmount(name);
        if (amount > 0)
            TryRemoveObject(name, amount);
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
        if (amount <= 0) return false;

        int newAmount = _objectsAmount[name] - amount;

        if (newAmount < 0) return false;

        _objectsAmount[name] = newAmount;
        
        if (newAmount == 0) _onMaterialDeleted?.Invoke(name);
        _onInventoryChange?.Invoke();

        return true;
    }

    public void RemoveAll()
    {
        Objects.Keys.ToList().ForEach(key => Objects[key] = 0);
        _onInventoryChange?.Invoke();
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

public enum EnemyName
{
    Bunny,
    Golem,
    Mouse,
    GolemBoss
}

