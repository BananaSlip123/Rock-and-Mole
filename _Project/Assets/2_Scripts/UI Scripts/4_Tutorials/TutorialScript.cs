using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialScript : MonoBehaviour
{
	#region PRIVATE VARS
    [SerializeField] PlayerInput playerInput;
	[SerializeField] GameObject go_callOut;
	[SerializeField] TutorialScene scene;
	[SerializeField] string[] dialogsToShow;
    CallOut callOut;
	enum TutorialScene
	{
		Village,
		Room1,
		Room2,
		Room3, 
		Room4
	}

    public void OnPassDialog()=> callOut.OnInteraction();

    private void OnEnable()
    {
        playerInput.SwitchCurrentActionMap("CallOutDialog");
    }
    private void Start()
    {
		if (dialogsToShow.Length == 0) return;
		if (!GameData.NeedsTutorial)
		{
            gameObject.SetActive(false);
			return;
        }
		callOut = go_callOut.GetComponent<CallOut>();
		if (callOut == null) throw new System.Exception("go_CallOut must have a callout component");

        InitCallOut();
    }
    private void OnDisable()
    {
        playerInput.SwitchCurrentActionMap("Player");
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
        callOut.gameObject.SetActive(true);
        callOut.OnCallOutDisable += DialogEnded;
        callOut.StartCallOut(dialogsToShow);
    }
    void DialogEnded()
	{
		Debug.Log("fin");
        if (scene == TutorialScene.Village)
            GameData.NeedsTutorial = false;
        this.gameObject.SetActive(false);
    }
    #endregion
}
