using System.Collections.Generic;
using UnityEngine;

public class GameDataPlayModeInit : MonoBehaviour
{
    static bool _init = false;

    [SerializeField] bool needsTutorial = false;
    [SerializeField] int materialsAmount = 200;
    private void Awake()
    {
        if (_init) return;
        _init = true;

#if UNITY_EDITOR
        GameData.NeedsTutorial = needsTutorial;

        foreach (MaterialName key in typeof(MaterialName).GetEnumValues())
        {
            GameData.Inventory.AddObject(key, materialsAmount);
        }
#endif
    }
}
