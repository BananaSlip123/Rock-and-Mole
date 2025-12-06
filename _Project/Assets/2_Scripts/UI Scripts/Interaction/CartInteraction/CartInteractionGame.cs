using UnityEngine;

public class CartInteractionGame : ACartInteraction
{
    //Al inicio: ocultar el interaction si no hay materiales en la mochila
    //cuando se de a guardar en carreta, se cambia de menu en GameMenu a main, y se oculta el interaction

    [SerializeField] GameObject go_interactionComponent;
    [SerializeField] GameMenu gameMenu;

    void Awake()
    {
        go_interactionComponent.SetActive(!GameData.RunInventory.IsEmpty);
    }

    public override void OnCartMenuClose()
    {
        go_interactionComponent.SetActive(false);
        gameMenu.Button_OpenMain();
    }
}
