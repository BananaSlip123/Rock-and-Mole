using UnityEngine;
using TMPro;
using System;
public class InventoryUI : MonoBehaviour
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
    SlotUI[,] slots = new SlotUI[SIZE_Y,SIZE_X];
    #endregion
    #region PUBLIC VARS
    #endregion
    #region PRIVATE FUNCS
    private void Start()
    {
        //Crear Slots
        for(int x=0; x< SIZE_X; x++)
        {
            for (int y = 0; y < SIZE_Y; y++)
            {
                slots[y,x] = Instantiate(go_slotPrefab,this.transform).GetComponent<SlotUI>();
                float coordX = initialX + distanceX * x;
                float coordY = initialY - distanceY * y;
                slots[y, x].Init(new Vector3(coordX, coordY, 2));
            }
        }
    }

    private void UpdateInventory()
    {
        int position = 0;
        foreach (MaterialName key in GameData.Inventory.Objects.Keys)
        {
            int amount = GameData.Inventory.GetAmount(key);
            if (amount != 0)//ignoramos los huecos vacios
            {
                if (position < SIZE)
                {
                    int x = position % SIZE_X;
                    int y = position / SIZE_X;
                    slots[y, x].UpdateSlot(key, amount);

                    GameData.Inventory.SetToSlotChange(key, (int value) =>
                    { //le añadimos un callback a los materiales de la UI
                        slots[y, x].UpdateSlot(key, value);
                    });
                }
                position++;
            }   
        }
        while(position<SIZE)
        {
            throw new NotImplementedException();
            position++;
        }
    }
    private void OnEnable()//al inicio y al activar un objeto
    {
        //Actualizar inventario cada vez q se activa (al abrir menú de inventario)
        UpdateInventory();

        //Hacer callbacks para q se actualize la UI
        GameData.Inventory.SubscribeToInventoryChange(() =>
        {
            UpdateInventory();
        });
    }
    
    private void OnDisable()
    {
        //borrar los callbacks
        GameData.Inventory.CleanAllCallbacks();
    }
    #endregion
    #region PUBLIC FUNCS

    #endregion
}
