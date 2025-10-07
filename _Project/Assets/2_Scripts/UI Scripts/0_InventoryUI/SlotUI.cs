using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
public class SlotUI : MonoBehaviour
{
    //A inventory has many slots, there is a slot prefab
    #region SERIALIZABLE
    [SerializeField] Image img_imageComponent;
    [SerializeField] TextMeshProUGUI txt_amount;
    [SerializeField] GameObject go_Info;//cuando desactivemos el objeto del inventario, q ocultamos
    [SerializeField] IconDataInspector[] inspectorIcons;

    [System.Serializable]
    public class IconDataInspector
    {
        // Sin constructor público - se inicializa desde inspector
        // Pero desde código no se puede modificar una vez creado
        [SerializeField] private MaterialName _name;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private Color _color;
        public MaterialName Name => _name;
        public Sprite Sprite => _sprite;
        public Color Color => _color;
    }
    public readonly struct IconData
    {
        private readonly Sprite _sprite;
        private readonly Color _color;
        public IconData(Sprite sprite, Color color){ _sprite = sprite; _color = color; }
        public Sprite Sprite => _sprite;
        public Color Color => _color;
    }
    #endregion
    #region STATIC VARS
    static bool IsInit = false;
    static Dictionary<MaterialName, IconData> Icons = new Dictionary<MaterialName, IconData>();
    #endregion

    #region PRIVATE VARS
    int _amount;
    bool _selected = false;
    bool _enabled = true;
    Action _onSelection, _onUnselection;
    MaterialName _materialAssigned;
    #endregion
    #region PUBLIC VARS
    public int Amount
    {
        get => _amount;
        set
        {
            if(value != _amount)
            {
                _amount = value;
                txt_amount.text = value.ToString();
            }
        }
    }
    public bool Enabled
    {
        get => _enabled;
        set
        {
            _enabled = value;
            go_Info.SetActive(value);
            if (!value)
                Selected = false;
        }
    }

    public bool Selected
    {
        get => _selected;
        set
        {
            if(value != _selected && Enabled)
            {
                _selected = value;
                GetComponent<Image>().enabled = value;

                if (value) _onSelection?.Invoke();
                else _onUnselection?.Invoke();
            }
            
        }
    }
    public MaterialName MaterialAssigned
    {
        get => _materialAssigned;
        set
        {
            if (value != _materialAssigned && Enabled)
            {
                _materialAssigned = value;
                img_imageComponent.sprite = Icons[value].Sprite;
                img_imageComponent.color = Icons[value].Color;
            }
        }
    }
    #endregion
    #region PRIVATE FUNCS
    void Init()
    {
        Selected = false;
        if (!IsInit) //las variables estáticas queremos q las inicialice solo uno, nos da igual quien sea
        {
            IsInit = true;
            foreach (IconDataInspector elem in inspectorIcons)
            {
                Icons.TryAdd(elem.Name, new IconData(elem.Sprite, elem.Color));
            }
        }
    }
    #endregion
    #region PUBLIC FUNCS
    public void UpdateSlot(MaterialName name, int amount)
    {
        Enabled = true;
        Amount = amount;
        MaterialAssigned = name;
    }
    public void Init(Vector3 pos)
    {
        Init();
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = pos;
    }

    public void Button_Select() => Selected = !Selected;
    public void SubscribeToOnSelected(Action onSelectedCallback) =>_onSelection = onSelectedCallback;
    public void SubscribeToOnUnselected(Action onUnselectedCallback) => _onUnselection = onUnselectedCallback;
    public void CleanCallBacks()
    {
        _onSelection = null;
        _onUnselection = null;
    }
    #endregion
}
