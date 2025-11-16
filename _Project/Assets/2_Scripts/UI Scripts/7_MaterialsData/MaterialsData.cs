using System.Collections.Generic;
using UnityEngine;

public class MaterialsData: MonoBehaviour
{
    
    [SerializeField] MaterialsData_SO materials;
    public static Dictionary<MaterialName, IconData> Icons { get; private set; } = new Dictionary<MaterialName, IconData>();
    static bool IsInit = false;
    MaterialsData_SO.IconDataInspector[] inspectorIcons
    {
        get => materials.inspectorIcons;
    }
    private void Awake()
    {
        if (!IsInit)
        {
            IsInit = true;
            foreach (MaterialsData_SO.IconDataInspector elem in inspectorIcons)
            {
                Icons.TryAdd(elem.Name, new IconData(elem.Sprite, elem.Color));
            }
        }
    }
    public readonly struct IconData
    {
        private readonly Sprite _sprite;
        private readonly Color _color;
        public IconData(Sprite sprite, Color color) { _sprite = sprite; _color = color; }
        public Sprite Sprite => _sprite;
        public Color Color => _color;
    }
}
