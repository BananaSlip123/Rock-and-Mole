using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [SerializeField] GameObject go_RunInventory;
    [SerializeField] GameObject go_LifeBar;
    [SerializeField] GameObject go_RunInventoryButton;

    [SerializeField]  bool showLifeBar = true;
    [SerializeField]  bool showInventory = true;

    private void Awake()
    {
        go_LifeBar.SetActive(showLifeBar);
        go_RunInventoryButton.SetActive(showInventory);
        go_RunInventory.SetActive(false);
    }
    public void OnInventoryButtonClick()
    {
        go_RunInventory.SetActive(showInventory);
        go_LifeBar.SetActive(false);
        go_RunInventoryButton.SetActive(false);
    }

    public void OnCloseInventoryClick()
    {
        go_RunInventoryButton.SetActive(showInventory);
        go_LifeBar.SetActive(showLifeBar);
        go_RunInventory.SetActive(false);
    }


}
