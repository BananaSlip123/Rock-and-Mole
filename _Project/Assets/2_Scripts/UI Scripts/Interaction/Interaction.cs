using UnityEngine;
using PlayerComponents;
using UnityEngine.Events;
using TMPro;
public class Interaction : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] PlayerController playerController;
    [SerializeField] TextMeshPro texto;
    [Header("Callback")]
    [SerializeField] UnityEvent onInteraction;

    Color available = new Color(1,1,0.9f,1);
    Color notAvailable = new Color(1,1,0.7f,0.6f);
    void Awake()
    {
        texto.color = notAvailable;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            texto.color = available;
            playerController.pressButtonA = Interact;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            texto.color = notAvailable;
            playerController.pressButtonA = null;
        }
    }
    private void Interact()
    {
        onInteraction.Invoke();
    }
}
