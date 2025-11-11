using System.Collections.Generic;
using UnityEngine;

public class GameDataPlayModeInit : MonoBehaviour
{
    static bool _init = false;

    [SerializeField] bool needsTutorial = false;
    [SerializeField] int materialsAmount = 200;

#if UNITY_EDITOR
    private void Awake()
    {
        if (_init) return;
        _init = true;

        GameData.NeedsTutorial = needsTutorial;

        foreach (MaterialName key in typeof(MaterialName).GetEnumValues())
        {
            GameData.Inventory.AddObject(key, materialsAmount);
        }

        Debug.Log("GameData inicializado para Play Mode en Editor");
    }
#endif
}
