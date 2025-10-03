using UnityEngine;
public class InventoryUI : MonoBehaviour
{
    #region SERIALIZABLE
    [SerializeField] SlotUI slotUI_slotPrefab;
    [SerializeField] float initialX;
    [SerializeField] float initialY;
    [SerializeField] float distanceX;
    [SerializeField] float distanceY;

    #endregion
    #region PRIVATE VARS
    const int SIZE_X = 3;
    const int SIZE_Y = 4;
    const int SIZE = SIZE_X * SIZE_Y;

    SlotUI[,] slots = new SlotUI[SIZE_Y,SIZE_X]; 
    #endregion
    #region PUBLIC VARS
    #endregion
    #region PRIVATE FUNCS
    private void Awake()
    {
        
    }
    #endregion
    #region PUBLIC FUNCS
    
    #endregion
}
