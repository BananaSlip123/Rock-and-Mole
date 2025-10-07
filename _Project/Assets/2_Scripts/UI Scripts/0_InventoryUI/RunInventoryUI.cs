using UnityEngine;
using TMPro;
using System;

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
    SlotUI _selectedSlot = null;
    Action _onSelectedSlotChanged = null;
    #endregion
    #region PUBLIC VARS
    SlotUI SelectedSlot
    {
        get => _selectedSlot;
        set
        {
            _selectedSlot = value;
            _onSelectedSlotChanged?.Invoke();
        }
    }
    #endregion
    #region PRIVATE FUNCS
    private void Awake()
    {
        //Crear Slots
        for (int x = 0; x < SIZE_X; x++)
        {
            for (int y = 0; y < SIZE_Y; y++)
            {
                slots[y, x] = Instantiate(go_slotPrefab, this.transform).GetComponent<SlotUI>();
                float coordX = initialX + distanceX * x;
                float coordY = initialY - distanceY * y;
                slots[y, x].Init(new Vector3(coordX, coordY, 2));
            }
        }
    }

    private void UpdateInventory()
    {
        int position = 0;
        foreach (MaterialName key in GameData.RunInventory.Objects.Keys)
        {
            int amount = GameData.RunInventory.GetAmount(key);
            if (amount != 0 && position < SIZE)//ignoramos los huecos vacíos
            {

                int x = position % SIZE_X;
                int y = position / SIZE_X;

                slots[y, x].UpdateSlot(key, amount);

                slots[y, x].SubscribeToOnSelected(() => {
                    //Deseleccionar anterior seleccionado
                    if (SelectedSlot != null) SelectedSlot.Selected = false;
                    //Marcar nuevo seleccionado
                    SelectedSlot = slots[y, x];
                });
                position++;
            }
        }
        while (position < SIZE)
        {
            //Inactivar el resto de huecos
            int x = position % SIZE_X;
            int y = position / SIZE_X;
            slots[y, x].Enabled = false;
            slots[y, x].CleanCallBacks();
            position++;
        }
    }
    private void OnEnable()//al inicio y al activar un objeto
    {
        //Actualizar inventario cada vez q se activa (al abrir menú de inventario)
        UpdateInventory();

        //Hacer callbacks para q se actualize la UI
        GameData.RunInventory.SubscribeToInventoryChange(() =>
        {
            UpdateInventory();
        });
    }

    private void OnDisable()
    {
        //borrar los callbacks
        GameData.RunInventory.CleanAllCallbacks();

        foreach (SlotUI slot in slots)
            slot.CleanCallBacks();

    }
    #endregion
    #region PUBLIC FUNCS
    public void AddRandomMat()
    {
        System.Random r = new System.Random();
        int name = r.Next(Enum.GetValues(typeof(MaterialName)).Length);
        int amount = r.Next(5);
        GameData.RunInventory.AddObject((MaterialName)name, amount);
    }
    #endregion
}
