using System.Collections.Generic;
using UnityEngine;

public class GameDataPlayModeInit : MonoBehaviour
{
    static bool _init = false;

    [SerializeField] bool needsTutorial = false;
    [SerializeField] bool getFromDisc = false;
    [SerializeField] int materialsAmount = 200;
    [SerializeField] int coinsAmount = 4000;

#if UNITY_EDITOR
    private void Awake()
    {
        if (_init) return;
        _init = true;

        GameData.NeedsTutorial = needsTutorial;

        if (getFromDisc) return;
        foreach (MaterialName key in typeof(MaterialName).GetEnumValues())
        {
            
            GameData.Inventory.ResetObjectAmount(key); //vuelve a 0 (persistencia ejem)
            if (materialsAmount > 0)
                GameData.Inventory.AddObject(key, materialsAmount);
        }

        GameData.Coins = coinsAmount;
    }
#endif
}
