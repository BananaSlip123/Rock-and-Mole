using UnityEngine;
//using UnityEngine.InputSystem;

public class TutorialScript : MonoBehaviour
{
	#region PRIVATE VARS
    //[SerializeField] PlayerInput playerInput;
    [SerializeField] InputMapsManager playerInputMapsManager;
	[SerializeField] GameObject go_callOut;
	[SerializeField] TutorialScene scene;
	[SerializeField] string[] dialogsToShow;
    CallOut callOut;

    GameObject[] enemigosEscena;
    enum TutorialScene
	{
		Village,
		Room1,
		Room2,
		Room3, 
		Room4
	}

    public void OnPassDialog()=> callOut?.OnInteraction();

    private void OnEnable()
    {
        Debug.Log("Enable");

        playerInputMapsManager.InputMapProperty = InputMapsManager.InputMap.callOut;
    }
    private void Start()
    {
        Debug.Log("Start");
        if (dialogsToShow.Length == 0) return;

        if (!GameData.NeedsTutorial)
		{
            gameObject.SetActive(false);
			return;
        }

        if (scene == TutorialScene.Room3) FindAnyObjectByType<PlayerStats>().HealPlayer();

		callOut = go_callOut.GetComponent<CallOut>();
		if (callOut == null) throw new System.Exception("go_CallOut must have a callout component");

        enemigosEscena = GameObject.FindGameObjectsWithTag("Enemy");

        InitCallOut();
    }
    private void OnDisable()
    {
        Debug.Log("Disable");
        //playerInputMapsManager.SwitchCurrentActionMap("Player");
        playerInputMapsManager.InputMapProperty = InputMapsManager.InputMap.playerAndUi;
    }
    private void OnDestroy()
    {
        if (callOut != null)
            callOut.OnCallOutDisable = null;
    }
    private void InitCallOut()
    {
        Debug.Log("Init");
        callOut.enabled = true;

        foreach (GameObject enemy in enemigosEscena) enemy.SetActive(false);

        callOut.gameObject.SetActive(true);
        callOut.OnCallOutDisable += DialogEnded;
        callOut.StartCallOut(dialogsToShow);
    }
    void DialogEnded()
	{
		Debug.Log("fin");
        if (scene == TutorialScene.Village)
            GameData.NeedsTutorial = false;
        foreach (GameObject enemy in enemigosEscena) enemy.SetActive(true);
        this.gameObject.SetActive(false);
    }
    #endregion
}
