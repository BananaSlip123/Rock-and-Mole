using UnityEngine;

public class CartInteractionVillage : ACartInteraction
{
    //Al inicio: ocultar el interaction si no hay materiales en el carro
    //cuando se de a guardar en baúl, se cambia de menu en VillageUI a main, y se oculta el interaction

    [SerializeField] GameObject go_interactionComponent;
    [SerializeField] VillageMenuUI villageMenuUI;

    private void Awake()
    {
        go_interactionComponent.SetActive(!GameData.CartInventory.IsEmpty);
    }

    public override void OnCartMenuClose()
    {
        go_interactionComponent.SetActive(false);
        villageMenuUI.Button_OpenMain();
    }
}
