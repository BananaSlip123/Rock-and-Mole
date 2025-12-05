using UnityEngine;
using System.Collections.Generic;

public class BiomeSelectorUI : MonoBehaviour
{
    [SerializeField] GameObject go_selectBiomeButton; //ocultar si no se puede seleccionar
    [SerializeField] List<DoorSprites> spritesDoors; //ocultar si no se puede seleccionar

    [System.Serializable]
    struct DoorSprites
    {
        [SerializeField] Sprite UnlockedDoorSprite;
        [SerializeField] Sprite LockedDoorSprite;
    }
    #region PRIVATE FUNCS
    private void Awake()
    {
         
    }
    void OnEnable()
    {

    }
    void OnDisable()
    {

    }
    #endregion

    #region PUBLIC FUNCS

    #endregion
    public void ButtonLeft()
    {

    }
    public void ButtonRight()
    {

    }
}
