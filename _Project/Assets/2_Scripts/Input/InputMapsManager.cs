using UnityEngine;
using UnityEngine.InputSystem;

public class InputMapsManager : MonoBehaviour
{
    [Header("Player Input")]
    [SerializeField] private PlayerInput playerInput;

    [Header("Action Maps")]
    [SerializeField] private string uiActionMap = "UI";
    [SerializeField] private string playerActionMap = "Player";
    [SerializeField] private string callOutActionMap = "CallOutDialog";

    bool _isInit = false;
    InputMap? delayedInputMap = null;
    InputActionMap map_UI;
    InputActionMap map_Player;
    InputActionMap map_CallOut;

    public InputMap InputMapProperty
    {
        set
        {
            if (!_isInit)
            {
                delayedInputMap = value;
                return;
            }
            Debug.Log("El mapa de accion es: "+ value.ToString());
            switch (value)
            {
                case InputMap.callOut:
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
        callOut,
        uiNavigation,
        playerAndUi,
    }
    private void Awake()
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
        if (map_UI == null) throw new System.Exception($"Mapa  {uiActionMap} no encontrado");

        map_Player = playerInput.actions.FindActionMap(playerActionMap, true);
        if (map_Player == null) throw new System.Exception($"Mapa  {playerActionMap} no encontrado");

        map_CallOut = playerInput.actions.FindActionMap(callOutActionMap, true);
        if (map_CallOut == null) throw new System.Exception($"Mapa  {callOutActionMap} no encontrado");

        _isInit = true;
        
        if(delayedInputMap.HasValue)
            InputMapProperty = delayedInputMap.Value;
        else
            InputMapProperty = InputMap.playerAndUi;
    }


}
