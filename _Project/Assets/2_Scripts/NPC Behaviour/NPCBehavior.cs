using UnityEngine;
using UnityEngine.InputSystem;

public class NPCBehavior : MonoBehaviour
{
    [SerializeField] CallOut callOut;
    [SerializeField] InputMapsManager playerInputMapsManager;

    int currentInteraction = 0;
    bool isRoomCleaned = false;

    string[] OnFirstInteraction = 
    { 
        "¡Necesito tu ayuda!",
        "Me perdí en las minas, ¡y estás puertas están todas cerradas!",
        "¡Llevo horas aquí dentro!",
        "¡Horas!"
    };
    string[] OnSecondInteraction =
    {
        "¡HORAS!"
    };
    string[] AfterKillingEnemies = 
    {
        "¡Eres realmente fuerte!",
        "Toma un regalo por tu ayuda",
        "Voy a descansar un rato y ahora proseguiré con mi trayecto a la aldea"
    };

    #region PRIVATE FUNCS
    private void OnEnable()
    {
        LevelManager.instance.onRoomCleaned += onRoomCleaned;
    }
    private void OnDisable()
    {
        LevelManager.instance.onRoomCleaned -= onRoomCleaned;
    }
    void onRoomCleaned()
    {
        isRoomCleaned = true;
    }
    void OnFinishInteraction()
    {

    }
    #endregion
    #region PUBLIC FUNCS
    public void OnInteraction() //Cuando pulsas E
    {
        if (isRoomCleaned)
        {

        }
        else if (currentInteraction == 0)
        {

        }
        else if(currentInteraction == 1)
        {

        }
    }
    public void OnPassDialog(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed)
            return;

        callOut.OnInteraction();
    }
    #endregion
}
