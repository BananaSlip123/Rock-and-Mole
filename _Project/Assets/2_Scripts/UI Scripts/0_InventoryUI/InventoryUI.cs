using UnityEngine;
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
    SlotUI[,] slots = new SlotUI[SIZE_Y, SIZE_X];
    SlotUI _selectedSlot = null;

    #endregion
    #region PUBLIC VARS
    SlotUI SelectedSlot
    {
        get => _selectedSlot;
        set
        {
            _selectedSlot = value;
            if (value != null)
            {
                OnSelectedMaterial?.Invoke(SelectedMaterial);
            }
            else
                OnUnSelectedMaterial?.Invoke();
        }
    }
    public bool HasMaterialSelected => _selectedSlot != null;
    public MaterialName SelectedMaterial => _selectedSlot.MaterialAssigned;
    public Action<MaterialName> OnSelectedMaterial { get; set; } = null;
    public Action OnUnSelectedMaterial { get; set; } = null;
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
    private void OnEnable()//al inicio y al activar un objeto
    {
        //Actualizar inventario cada vez q se activa (al abrir menú de inventario)
        UpdateInventory();

        //Hacer callbacks para q se actualize la UI
        GameData.Inventory.SubscribeToInventoryChange(() =>
        {
            UpdateInventory();
        });
        GameData.Inventory.SubscribeToMaterialDeleted((MaterialName name) =>
        {
            if (SelectedSlot == null) return;
            if (SelectedMaterial == name)
            {
                SelectedSlot.Selected = false;
                SelectedSlot = null;
            }
        });
        
        GameData.Inventory.SubscribeToMaterialAdded(() =>
        {
            if (SelectedSlot == null) return;
            SelectedSlot.Selected = false;
            SelectedSlot = null;
        });
    }

    private void OnDisable()
    {
        //borrar los callbacks
        GameData.Inventory.CleanAllCallbacks();

        foreach (SlotUI slot in slots)
            slot.CleanCallBacks();
    }
    private void UpdateInventory()
    {
        int position = 0;
        foreach (MaterialName key in GameData.Inventory.Objects.Keys)
        {
            int amount = GameData.Inventory.GetAmount(key);
            if (amount != 0 && position < SIZE)//ignoramos los huecos vacíos
            {
                int x = position % SIZE_X;
                int y = position / SIZE_X;

                slots[y, x].UpdateSlot(key, amount);

                slots[y, x].SubscribeToOnSelected(()=>{
                    //Deseleccionar anterior seleccionado
                    if(SelectedSlot != null) SelectedSlot.Selected = false;
                    //Marcar nuevo seleccionado
                    SelectedSlot = slots[y,x];
                });
                slots[y, x].SubscribeToOnUnselected(() =>
                {
                    if (SelectedSlot == slots[y, x])
                        SelectedSlot = null;
                });

                
                //GameData.Inventory.SetToSlotChange(key, (int value) =>
                //{ //le añadimos un callback a los materiales de la UI
                //    slots[y, x].UpdateSlot(key, value);
                //});
                position++;
            }
            //else
               // GameData.Inventory.SetToSlotChange(key, null);
        }
        while(position < SIZE)
        {
            //Inactivar el resto de huecos
            int x = position % SIZE_X;
            int y = position / SIZE_X;
            slots[y, x].Enabled = false;
            slots[y, x].CleanCallBacks();
            position++;
        }
    }
    
    #endregion
    #region PUBLIC FUNCS
    public void AddRandomMat()
    {
        
        System.Random r = new System.Random();
        int name = r.Next(Enum.GetValues(typeof(MaterialName)).Length);
        int amount = r.Next(5)+1;
        GameData.Inventory.AddObject((MaterialName)name, amount);
    }
    #endregion
}
