using UnityEngine;
using TMPro;

public class RunInventoryUI : MonoBehaviour
{
    //used in the village scene
    #region SERIALIZABLE
    [SerializeField] GameObject go_slotPrefab;//duplicamos este prefab
    [SerializeField] float initialX;
    [SerializeField] float initialY;
    [SerializeField] float distanceX;
    [SerializeField] float distanceY;
    #endregion
    #region PRIVATE VARS
    const int SIZE_X = 3;
    const int SIZE_Y = 4;
    const int SIZE = SIZE_X * SIZE_Y;
    SlotUI[,] slots = new SlotUI[SIZE_Y, SIZE_X];
    #endregion
    #region PUBLIC VARS
    #endregion
    #region PRIVATE FUNCS
    private void Awake()
    {
        for (int x = 0; x < SIZE_X; x++)
        {
            for (int y = 0; y < SIZE_Y; y++)
            {
                slots[y, x] = Instantiate(go_slotPrefab).GetComponent<SlotUI>();
                float coordX = initialX + distanceX * x;
                float coordY = initialY + distanceY * y;
                slots[y, x].Init(new Vector3(coordX, coordY, 2));
            }
        }
    }
    private void OnEnable()//al inicio y al activar un objeto
    {
        int position = 0;
        foreach (MaterialName key in GameData.RunInventory.Objects.Keys)
        {
            position++;

            if (position < SIZE)
            {
                int x = position % SIZE_X;
                int y = position / SIZE_X;
                slots[y, x].UpdateSlot(key, GameData.RunInventory.GetAmount(key));
            }

        }

    }
    #endregion
    #region PUBLIC FUNCS

    #endregion
}
