using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [Header("Player Input")]
    [SerializeField] private PlayerInput playerInput;

    [Header("Action Maps")]
    [SerializeField] private string uiActionMap = "UI";
    [SerializeField] private string playerActionMap = "Player";
    [SerializeField] private string callOutActionMap = "CallOutDialog";

    InputActionMap map_UI;
    InputActionMap map_Player;
    InputActionMap map_CallOut;

    InputMap? _inputMap = null;
    public InputMap InputMapProperty
    {
        set
        {
            _inputMap = value;
            switch (value)
            {
                case InputMap.tutorialCallOut:
                    map_UI.Disable();
                    map_Player.Disable();
                    map_CallOut.Enable();
                    break;
                case InputMap.uiNavigation:
                    map_UI.Enable();
                    map_Player.Disable();
                    map_CallOut.Disable();
                    break;
                case InputMap.playerAndUi:
                    map_UI.Enable();
                    map_Player.Enable();
                    map_CallOut.Disable();
                    break;
                default:
                    break;
            }
        }
    }
    public enum InputMap
    {
        tutorialCallOut,
        uiNavigation,
        playerAndUi,
    }
    private void Start()
    {
        // Validar que tenemos PlayerInput
        if (playerInput == null)
        {
            playerInput = GetComponent<PlayerInput>();
            if (playerInput == null)
            {
                Debug.LogError("No PlayerInput component found!");
                return;
            }
        }
        map_UI = playerInput.actions.FindActionMap(uiActionMap, true);
        map_Player = playerInput.actions.FindActionMap(playerActionMap, true);
        map_CallOut = playerInput.actions.FindActionMap(callOutActionMap, true);

        InputMapProperty = InputMap.playerAndUi;
    }


}
