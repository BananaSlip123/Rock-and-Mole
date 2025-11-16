using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MaterialsData_SO", menuName = "Scriptable Objects/MaterialsData_SO")]
public class MaterialsData_SO : ScriptableObject
{
    public  IconDataInspector[] inspectorIcons;
    
    [System.Serializable]
    public class IconDataInspector
    {
        [SerializeField] private MaterialName _name;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private Color _color;
        public MaterialName Name => _name;
        public Sprite Sprite => _sprite;
        public Color Color => _color;
    }
}
