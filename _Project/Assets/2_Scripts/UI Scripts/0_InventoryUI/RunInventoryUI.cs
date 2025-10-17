using UnityEngine;
using System;
using UnityEngine.UI;
public class RunInventoryUI : MonoBehaviour
{
    //used in the village scene
    #region SERIALIZABLE
    [SerializeField] GameObject go_slotPrefab;//duplicamos este prefab
    [SerializeField] Selectable objectToNavigate;
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
    #endregion
    #region PUBLIC VARS
    public Selectable FirstElementToSelect
    {
        get => slots[0, 0].GetSelectable();
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
        AddNavigationToSlots();
    }
    private void AddNavigationToSlots()
    {
        Navigation nav = objectToNavigate.navigation; //el menu de tienda
        nav.selectOnLeft = slots[0, SIZE_X - 1].GetSelectable();
        objectToNavigate.navigation = nav;

        for (int x = 0; x < SIZE_X; x++)
        {
            for (int y = 0; y < SIZE_Y; y++)
            {
                if (x == 0)//izq del todo
                {
                    slots[y, x].SetNavigation(objectToNavigate, SlotUI.Direction.Left);
                    slots[y, x].SetNavigation(slots[y, 1].GetSelectable(), SlotUI.Direction.Right);
                }
                else if (x == SIZE_X - 1)//derecha del todo
                {
                    slots[y, x].SetNavigation(slots[y, x - 1].GetSelectable(), SlotUI.Direction.Left);
                    slots[y, x].SetNavigation(objectToNavigate, SlotUI.Direction.Right);
                }
                else //medio
                {
                    slots[y, x].SetNavigation(slots[y, x - 1].GetSelectable(), SlotUI.Direction.Left);
                    slots[y, x].SetNavigation(slots[y, x + 1].GetSelectable(), SlotUI.Direction.Right);
                }

                if (y == 0)
                {
                    slots[y, x].SetNavigation(slots[SIZE_Y - 1, x].GetSelectable(), SlotUI.Direction.Up);
                    slots[y, x].SetNavigation(slots[y + 1, x].GetSelectable(), SlotUI.Direction.Down);
                }
                else if (y == SIZE_Y - 1)
                {
                    slots[y, x].SetNavigation(slots[y - 1, x].GetSelectable(), SlotUI.Direction.Up);
                    slots[y, x].SetNavigation(slots[0, x].GetSelectable(), SlotUI.Direction.Down);
                }
                else
                {
                    slots[y, x].SetNavigation(slots[y - 1, x].GetSelectable(), SlotUI.Direction.Up);
                    slots[y, x].SetNavigation(slots[y + 1, x].GetSelectable(), SlotUI.Direction.Down);
                }
            }
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
        GameData.RunInventory.SubscribeToMaterialDeleted((MaterialName name) =>
        {
            if (SelectedSlot == null) return;
            if (SelectedMaterial == name)
            {
                SelectedSlot.Selected = false;
                SelectedSlot = null;
            }
        });

        GameData.RunInventory.SubscribeToMaterialAdded(() =>
        {
            if (SelectedSlot == null) return;
            SelectedSlot.Selected = false;
            SelectedSlot = null;
        });
    }

    private void OnDisable()
    {
        //borrar los callbacks
        GameData.RunInventory.CleanAllCallbacks();

        foreach (SlotUI slot in slots)
            slot.CleanCallBacks();

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
                slots[y, x].SubscribeToOnUnselected(() =>
                {
                    if (SelectedSlot == slots[y, x])
                        SelectedSlot = null;
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

    #endregion
    #region PUBLIC FUNCS
    public void AddRandomMat()
    {
        System.Random r = new System.Random();
        int name = r.Next(Enum.GetValues(typeof(MaterialName)).Length);
        int amount = r.Next(5)+1;
        GameData.RunInventory.AddObject((MaterialName)name, amount);
    }
    #endregion
}
