using UnityEngine;
using TMPro;
using PickaxeStats;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class WardrobeUI : MonoBehaviour
{
    #region SERIALIZABLE FIELDS
    [Header("Texts Stats")]
    [SerializeField] TextMeshProUGUI txt_name;
    [SerializeField] TextMeshProUGUI txt_lifePoints;
    [SerializeField] TextMeshProUGUI txt_attackSpeed;
    [SerializeField] TextMeshProUGUI txt_movementSpeed;

    [Header("Texts After Change")]
    [SerializeField] TextMeshProUGUI txt_bonusLifePoints;
    [SerializeField] TextMeshProUGUI txt_bonusAttackSpeed;
    [SerializeField] TextMeshProUGUI txt_bonusMovementSpeed;

    [Header("Dynamic Button Texts")]
    [SerializeField] TextMeshProUGUI txt_button_BuyOrEquip;
    [SerializeField] TextMeshProUGUI txt_button_Change;

    [Header("Game Objects & Transforms")]
    [SerializeField] GameObject go_error;
    [SerializeField] GameObject go_materialsWindow;

    [Header("Colors on stats")]
    [SerializeField] Color color_buff;
    [SerializeField] Color color_deBuff;

    [Header("Materials")]
    [SerializeField] MaterialInfoUI[] materialsInfo;

    [Header("Transforms Models")]
    [SerializeField] Transform t_helmetTransform;
    [SerializeField] Transform t_chestClothTransform;

    #endregion
    #region PRIVATE FIELDS
    private Coroutine currentFade;
    private GameObject currentChestClothPrefab = null;
    private GameObject currentHelmetPrefab = null;
    enum WardrobeMode
    {
        helmet = 0, chestCloth = 1
    }
    WardrobeMode _wardrobeMode = WardrobeMode.chestCloth;
    enum ButtonState
    {
        buy = 0, equip = 1, equipped = 2
    }
    ButtonState? _buttonState = null;

    string _selectedHelmetID;
    string _selectedChestClothID;

    #endregion
    #region PRIVATE PROPERTIES
    WardrobeMode WardrobeModeProperty
    {
        get => _wardrobeMode;
        set
        {
            _wardrobeMode = value;
            switch (value)
            {
                case WardrobeMode.helmet:
                    OnHelmetMode();
                    break;
                case WardrobeMode.chestCloth:
                    OnChestClothMode();
                    break;
            }
        }
    }
    string Name
    {
        set => txt_name.text = value;
    }
    int LifePoints
    {
        set => txt_lifePoints.text = value.ToString();
    }
    float AttackSpeed
    {
        set
        {
            int percent = (int)(value * 100);
            if(value >= 0)
                txt_attackSpeed.text = "+" + percent.ToString() + "%";
            else
                txt_attackSpeed.text = percent.ToString() + "%";
        }
    }
    float MovementSpeed
    {
        set
        {
            int percent = (int)(value * 100);
            if (value >= 0)
                txt_movementSpeed.text = "+" + percent.ToString() + "%";
            else
                txt_movementSpeed.text = percent.ToString() + "%";
        }
    }

    int? BonusLifePoints
    {
        set
        {
            if (value.HasValue)
            {
                if (value > 0)
                {
                    txt_bonusLifePoints.color = color_buff;
                    txt_bonusLifePoints.text = "+" + value.ToString();
                    return;
                }
                if (value < 0)
                {
                    txt_bonusLifePoints.color = color_deBuff;
                    txt_bonusLifePoints.text = value.ToString();
                    return;
                }
            }
            txt_bonusLifePoints.text = "";
        }
    }
    float? BonusAttackSpeed
    {
        set
        {
            if (value.HasValue)
            {
                int percent = (int)(value * 100);

                if (value > 0)
                {
                    txt_bonusAttackSpeed.color = color_buff;
                    txt_bonusAttackSpeed.text = "+" + percent.ToString() + "%";
                    return;
                }
                if (value < 0)
                {
                    txt_bonusAttackSpeed.color = color_deBuff;
                    txt_bonusAttackSpeed.text = percent.ToString() + "%";
                    return;
                }
            }
            txt_bonusAttackSpeed.text = "";
        }
    }
    float? BonusMovementSpeed
    {
        set
        {
            if (value.HasValue)
            {
                int percent = (int)(value * 100);
                if (value > 0)
                {
                    txt_bonusMovementSpeed.color = color_buff;
                    txt_bonusMovementSpeed.text = "+" + percent.ToString() + "%";
                    return;
                }
                if (value < 0)
                {
                    txt_bonusMovementSpeed.color = color_deBuff;
                    txt_bonusMovementSpeed.text = percent.ToString() + "%";
                    return;
                }
            }
            txt_bonusMovementSpeed.text = "";

        }
    }

    string SelectedHelmetID
    {
        get => _selectedHelmetID;
        set
        {
            _selectedHelmetID = value;
            UpdateUI();
        }
    }
    string SelectedChestClothID
    {
        get => _selectedChestClothID;
        set
        {
            _selectedChestClothID = value;
            UpdateUI();
        }
    }
    ClothStatsScripteableObject SelectedHelmet
    {
        get => EquipmentManager.Helmets[SelectedHelmetID].data;
    }
    ClothStatsScripteableObject SelectedChestCloth
    {
        get => EquipmentManager.ChestCloths[SelectedChestClothID].data;
    }
    ClothStatsScripteableObject SelectedCloth
    {
        get
        {
            switch (WardrobeModeProperty)
            {
                case WardrobeMode.helmet: return SelectedHelmet;
                case WardrobeMode.chestCloth: return SelectedChestCloth;
                default: return null;
            }
        }
    }
    ClothStatsScripteableObject EquippedCloth
    {
        get
        {
            switch (WardrobeModeProperty)
            {
                case WardrobeMode.helmet: return EquipmentManager.CurrentHelmetData;
                case WardrobeMode.chestCloth: return EquipmentManager.CurrentChestClothData;
                default: return null;
            }
        }
    }

    bool IsUnlockedSelectedCloth
    {
        get
        {
            switch (WardrobeModeProperty)
            {
                case WardrobeMode.helmet:
                    return EquipmentManager.Helmets[SelectedHelmetID].UnLocked;
                case WardrobeMode.chestCloth:
                    return EquipmentManager.ChestCloths[SelectedChestClothID].UnLocked;
                default: throw new NotImplementedException();
            }
        }
    }
    ButtonState ButtonStateProperty
    {
        get => _buttonState.Value;
        set
        {
            if (!_buttonState.HasValue || _buttonState.Value != value)
            {
                _buttonState = value;
                go_materialsWindow.SetActive(value == ButtonState.buy);
                switch (value)
                {
                    case ButtonState.buy:
                        txt_button_BuyOrEquip.text = "Comprar";
                        break;
                    case ButtonState.equip:
                        txt_button_BuyOrEquip.text = "Equipar";
                        break;
                    case ButtonState.equipped:
                        txt_button_BuyOrEquip.text = "Equipado";
                        break;
                }
            }
        }
    }

    string[] OrderedChestCloths
    {
        get => EquipmentManager.keysOrdered_chestCloth;
    }
    string[] OrderedHelmets
    {
        get => EquipmentManager.keysOrdered_helmet;
    }


    #endregion
    #region PRIVATE FUNCS
    private void Awake()
    {
        go_error.SetActive(false);
    }
    private void OnEnable()
    {
        _selectedHelmetID = EquipmentManager.CurrentHelmetID;
        _selectedChestClothID = EquipmentManager.CurrentChestClothID;
        WardrobeModeProperty = WardrobeMode.chestCloth;
    }
    private void OnChestClothMode()
    {
        txt_button_Change.text = "Mostrar Cascos";
        EquipmentManager.OnCurrentChestClothChange += UpdateUI;
        EquipmentManager.OnCurrentHelmetChange -= UpdateUI; //si no lo tiene asignado no hace nada
        EquipmentManager.OnUnlockedChestCloth += OnUnlocked;
        EquipmentManager.OnUnlockedHelmet -= OnUnlocked;

        UpdateUI();
    }
    private void OnHelmetMode()
    {
        txt_button_Change.text = "Mostrar Petos";
        EquipmentManager.OnCurrentChestClothChange -= UpdateUI;
        EquipmentManager.OnCurrentHelmetChange += UpdateUI;
        EquipmentManager.OnUnlockedChestCloth -= OnUnlocked;
        EquipmentManager.OnUnlockedHelmet += OnUnlocked;

        UpdateUI();
    }

    private void OnDisable()
    {
        EquipmentManager.OnCurrentChestClothChange -= UpdateUI;
        EquipmentManager.OnCurrentHelmetChange -= UpdateUI;

        EquipmentManager.OnUnlockedHelmet -= OnUnlocked;
        EquipmentManager.OnUnlockedChestCloth -= OnUnlocked;
    }
    private void OnUnlocked(string name)
    {
        if(name == SelectedCloth.name)
            ButtonStateProperty = ButtonState.equip;
    }
    private void UpdateUI()
    {
        ClothStatsScripteableObject equipped = EquippedCloth;
        ClothStatsScripteableObject selected = SelectedCloth;

        Dictionary<Stats, float> equippedDict = equipped.modifiers
             .GroupBy(mod => mod.stat)
             .ToDictionary(group => group.Key, group => group.Sum(mod => mod.value));

        Dictionary<Stats, float> selectedDict = selected.modifiers
             .GroupBy(mod => mod.stat)
             .ToDictionary(group => group.Key, group => group.Sum(mod => mod.value));

        UpdateEquippedCloth(equipped, equippedDict); //refleja los stats base
        UpdateSelectedCloth(selected, equipped, selectedDict, equippedDict); //refleja las mejoras etc

        UpdatePrice(selected); //precio si no esta comprado

        ActualiceModel();
    }

    void UpdateEquippedCloth(ClothStatsScripteableObject equipped, Dictionary<Stats, float> equippedDict)
    {
        foreach (Stats stat in typeof(Stats).GetEnumValues())
        {
            switch (stat)
            {
                case Stats.health:
                    LifePoints = (equippedDict.ContainsKey(stat)) ? (int)equippedDict[Stats.health] : 0;
                    break;
                case Stats.speed:
                    MovementSpeed = (equippedDict.ContainsKey(stat)) ? equippedDict[Stats.speed] : 0;
                    break;
                case Stats.attackSpeed:
                    AttackSpeed = (equippedDict.ContainsKey(stat)) ? equippedDict[Stats.attackSpeed] : 0;
                    break;
            }
        }

    }
    private void UpdateSelectedCloth(ClothStatsScripteableObject selected, ClothStatsScripteableObject equipped,
        Dictionary<Stats, float> selectedDict, Dictionary<Stats, float> equippedDict)
    {
        Name = selected.name;

        if (selected == equipped)
        {
            BonusLifePoints = null;
            BonusMovementSpeed = null;
            BonusAttackSpeed = null;

            ButtonStateProperty = ButtonState.equipped;
            return;
        }
        if (IsUnlockedSelectedCloth)
            ButtonStateProperty = ButtonState.equip;
        else
            ButtonStateProperty = ButtonState.buy;

        foreach (Stats stat in typeof(Stats).GetEnumValues())
        {
            float equippedValue = (equippedDict.ContainsKey(stat)) ? equippedDict[stat] : 0;
            float selectedValue = (selectedDict.ContainsKey(stat)) ? selectedDict[stat] : 0;

            switch (stat)
            {
                case Stats.health:
                    BonusLifePoints = (int)(selectedValue - equippedValue);
                    break;
                case Stats.speed:
                    BonusMovementSpeed = selectedValue - equippedValue;
                    break;
                case Stats.attackSpeed:
                    BonusAttackSpeed = selectedValue - equippedValue;
                    break;
            }
        }
    }
    private void UpdatePrice(ClothStatsScripteableObject selected)
    {
        bool isUnlocked = IsUnlockedSelectedCloth;

        if (isUnlocked) return;

        materialsInfo[0].Amount = selected.coinsPrice;

        for (int i = 0; i < selected.costs.Count; i++)
        {
            if (i + 1 < materialsInfo.Length)
            {
                materialsInfo[i + 1].gameObject.SetActive(true);
                materialsInfo[i + 1].Amount = selected.costs[i].cost;
                materialsInfo[i + 1].MaterialAssigned = selected.costs[i].material;
            }
            else throw new Exception("Debes meter más huecos de material en el array materialsInfo");
        }
        for (int i = selected.costs.Count; i < materialsInfo.Length - 1; i++)
        {
            materialsInfo[i + 1].gameObject.SetActive(false);
        }
    }
    private bool HaveEnoughMaterials()
    {
        bool hasEnoughMaterials = true;

        foreach (MaterialCost materialCost in SelectedCloth.costs)
        {
            bool enough = GameData.Inventory.GetAmount(materialCost.material) >= materialCost.cost;
            if (!enough) hasEnoughMaterials = false;
        }
        return hasEnoughMaterials;
    }
    private void DecreaseMaterials()
    {
        foreach (MaterialCost materialCost in SelectedCloth.costs)
        {
            bool success = GameData.Inventory.TryRemoveObject(materialCost.material, materialCost.cost);
            if (!success) throw new Exception("Not enough materials");
        }
    }
    private bool HaveEnoughCoins()
    {
        return GameData.Coins >= SelectedCloth.coinsPrice;
    }
    private void DecreaseCoins()
    {
        GameData.Coins -= SelectedCloth.coinsPrice;
    }

    private void ActualiceModel()
    {
        if (currentChestClothPrefab != null) Destroy(currentChestClothPrefab);
        if (currentHelmetPrefab != null) Destroy(currentHelmetPrefab);

        currentHelmetPrefab = Instantiate(EquipmentManager.Helmets[SelectedHelmetID].model);

        switch (WardrobeModeProperty)
        {
            case WardrobeMode.helmet:
                //En este caso se verá el casco en grande
                AssignParent(currentHelmetPrefab.transform, t_helmetTransform);

                break;
            case WardrobeMode.chestCloth:
                //En este caso se verá cuerpo conpleto

                currentChestClothPrefab = Instantiate(EquipmentManager.ChestCloths[SelectedChestClothID].model);
                AssignParent(currentChestClothPrefab.transform, t_chestClothTransform);

                Transform transformForHelmet = currentChestClothPrefab.GetComponent<ChestClothGetter>().bone_Helmet;
                AssignParent(currentHelmetPrefab.transform, transformForHelmet);

                break;
        }
    }

    private void ChangeSelectedCloth(int placesToMove)
    {
        int currentIndex, numberOfElements, nextIndex;
        switch (WardrobeModeProperty)
        {
            case WardrobeMode.helmet:
                currentIndex = EquipmentManager.GetOrderHelmet(SelectedHelmetID);
                numberOfElements = EquipmentManager.NumberOfHelmets;

                nextIndex = (currentIndex + placesToMove + numberOfElements) % numberOfElements;

                SelectedHelmetID = OrderedHelmets[nextIndex];
                break;
            case WardrobeMode.chestCloth:
                currentIndex = EquipmentManager.GetOrderChestCloth(SelectedChestClothID);
                numberOfElements = EquipmentManager.NumberOfChestCloths;

                nextIndex = (currentIndex + placesToMove + numberOfElements) % numberOfElements;

                SelectedChestClothID = OrderedChestCloths[nextIndex];
                break;
        }
    }

    private void BuySelectedCloth()
    {

        if (HaveEnoughMaterials() && HaveEnoughCoins())
        {
            //restar costo
            DecreaseMaterials();
            DecreaseCoins();

            //Comprar objeto
            switch (WardrobeModeProperty)
            {
                case WardrobeMode.helmet:
                    EquipmentManager.Helmets[SelectedHelmetID].UnLocked = true;
                    break;
                case WardrobeMode.chestCloth:
                    EquipmentManager.ChestCloths[SelectedChestClothID].UnLocked = true;
                    break;
            }
        }
        else
        {
            ShowErrorMessage();
        }
    }
    private void EquipSelectedCloth()
    {
        switch (WardrobeModeProperty)
        {
            case WardrobeMode.helmet:
                EquipmentManager.CurrentHelmetID = SelectedHelmetID;
                break;
            case WardrobeMode.chestCloth:
                EquipmentManager.CurrentChestClothID = SelectedChestClothID;
                break;
        }
        //ButtonStateProperty = ButtonState.equipped;
    }
    private void ShowErrorMessage()
    {
        // Detener fade anterior y comenzar uno nuevo
        if (currentFade != null)
            StopCoroutine(currentFade);

        currentFade = StartCoroutine(FadeSequenceCoroutine());
    }
    private IEnumerator FadeSequenceCoroutine()
    {
        if (!go_error.activeInHierarchy)
        {
            go_error.SetActive(true);
        }
        TextMeshProUGUI textToFade = go_error.GetComponent<TextMeshProUGUI>();
        Color originalColor = textToFade.color;
        textToFade.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

        yield return null;//esperar un frame para q se active sin problemas

        float duration = 2f;
        float interval = 0.2f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            textToFade.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            elapsedTime += interval;
            yield return new WaitForSeconds(interval);
        }

        textToFade.gameObject.SetActive(false);

        currentFade = null;
    }
    #endregion
    #region PUBLIC FUNCS
    public void OnButtonBuyOrEquip()
    {
        switch (ButtonStateProperty)
        {
            case ButtonState.buy:
                BuySelectedCloth();
                break;
            case ButtonState.equip:
                EquipSelectedCloth();
                break;
            case ButtonState.equipped: break;//el boton no hace nada
            default: break;
        }
    }
    public void OnButtonChange()
    {
        int currentMode = (int)WardrobeModeProperty;
        int nextMode = (currentMode + 1) % 2;

        WardrobeModeProperty = (WardrobeMode)nextMode;
    }
    public void OnButtonLeftArrow()
    {
        ChangeSelectedCloth(-1);
    }
    public void OnButtonRightArrow()
    {
        ChangeSelectedCloth(1);
    }

    void AssignParent(Transform objectTransform, Transform parentTransform)
    {
        objectTransform.SetParent(parentTransform);
        objectTransform.localPosition = new Vector3();
        objectTransform.localEulerAngles = new Vector3();
        objectTransform.localScale = new Vector3(1, 1, 1);
    }
    #endregion
}