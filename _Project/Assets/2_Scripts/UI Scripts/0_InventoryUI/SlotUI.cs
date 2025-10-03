using UnityEngine;
using TMPro;
using System.Collections.Generic;
public class SlotUI : MonoBehaviour
{
    //A inventory has many slots, there is a slot prefab
    #region SERIALIZABLE
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] TextMeshProUGUI txt_amount;
    [SerializeField] EnumSpritePair[] inspectorIcons;

    [System.Serializable]
    public class EnumSpritePair
    {
        // Sin constructor público - se inicializa desde inspector
        // Pero desde código no se puede modificar una vez creado
        [SerializeField] private MaterialName _name;
        [SerializeField] private Sprite _sprite;
        public MaterialName Name => _name;
        public Sprite Sprite => _sprite;
    }
    #endregion
    #region STATIC VARS
    static bool IsInit = false;
    static Dictionary<MaterialName, Sprite> Icons;
    #endregion

    #region PRIVATE VARS
    int _amount;
    
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
    #endregion
    #region PRIVATE FUNCS
    private void Awake()
    {
        if (!IsInit) //las variables estáticas queremos q las inicialice solo uno, nos da igual quien sea
        {
            IsInit = true;
            foreach (EnumSpritePair elem in inspectorIcons)
            {
                Icons.TryAdd(elem.Name, elem.Sprite);
            }
        }
    }
    #endregion
    #region PUBLIC FUNCS
    public void UpdateSlot(MaterialName name, int amount)
    {
        spriteRenderer.sprite = Icons[name];
        Amount = amount;
    }
    public void Init(Vector3 pos)
    {
        transform.position = pos;
    }
    #endregion
}
