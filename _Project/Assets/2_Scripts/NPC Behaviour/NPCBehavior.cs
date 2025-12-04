using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class NPCBehavior : MonoBehaviour
{
    [SerializeField] CallOut callOut;
    [SerializeField] GameMenu gameMenu;
    [SerializeField] InputMapsManager playerInputMapsManager;
    [SerializeField] Interaction interaction;

    int currentInteraction = 0;
    bool isRoomCleaned = false;
    List<IStateMachineComponent> enemies = new List<IStateMachineComponent>();


    string[] dialog_FirstInteraction = 
    { 
        "¡Necesito tu ayuda!",
        "Me perdí en las minas, ¡y estás puertas están todas cerradas!",
        "¡Llevo horas aquí dentro!",
        "¡Horas!"
    };
    string[] dialog_SecondInteraction =
    {
        "¡HORAS!"
    };
    string[] dialog_AfterKillingEnemies = 
    {
        "¡Eres realmente fuerte!",
        "Toma un regalo por tu ayuda",
        "Voy a descansar un rato y ahora proseguiré con mi trayecto a la aldea"
    };

    #region PRIVATE FUNCS
    private void Start()
    {
        // Recorre todos los GameObjects raíz de la escena
        GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();

        foreach (GameObject rootObject in rootGameObjects)
        {
            // Obtiene todos los componentes que implementan la interfaz
            // El parámetro 'true' incluye objetos inactivos
            enemies.AddRange(rootObject.GetComponentsInChildren<IStateMachineComponent>(true));
        }

    }
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
    private void OnStartInteraction()
    {
        if (!isRoomCleaned && currentInteraction > 1) return;

        gameMenu.CurrentWindow = GameMenu.Windows.MainLifeBarInvisible;

        callOut.gameObject.SetActive(true);
        callOut.OnCallOutDisable += OnFinishInteraction;

        playerInputMapsManager.InputMapProperty = InputMapsManager.InputMap.callOut;

        foreach (IStateMachineComponent enemy in enemies)
        {
            if(enemy != null)
                enemy.IsPaused = true;
        }
        if (isRoomCleaned)
        {
            callOut.StartCallOut(dialog_AfterKillingEnemies);
        }
        else if (currentInteraction == 0)
        {
            callOut.StartCallOut(dialog_FirstInteraction);
        }
        else if (currentInteraction == 1)
        {
            callOut.StartCallOut(dialog_SecondInteraction);
        }
    }
    void OnFinishInteraction()
    {
        gameMenu.CurrentWindow = GameMenu.Windows.Main;

        callOut.gameObject.SetActive(false);
        playerInputMapsManager.InputMapProperty = InputMapsManager.InputMap.playerAndUi;

        foreach (IStateMachineComponent enemy in enemies)
        {
            if (enemy != null)
                enemy.IsPaused = false;
        }

        if (isRoomCleaned)
        {
            GameData.MaterialsChest(4);
        }
        else if (currentInteraction == 0)
        {
            currentInteraction++;
        }
        else if (currentInteraction == 1)
        {
            
        }

    }

    
    #endregion
    #region PUBLIC FUNCS
    public void OnInteraction() //Cuando pulsas E
    {
        OnStartInteraction();
    }
    public void OnPassDialog(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed)
            return;

        callOut.OnInteraction();   
    }
    #endregion
}
