using UnityEngine;

public class CartUI : MonoBehaviour
{
    //En la villa muestra si tienes materiales q recoger, y cuales son. Y boton de recoger
    //En partida muestra materiales conseguidos en mochila, y botón de enviar a la aldea

    [SerializeField] Scene scene;
    [SerializeField] MaterialInfoUI[] materialInfoUIs;
    [SerializeField] GameObject go_button;
    [SerializeField] GameObject go_noMaterialsSignal;
    enum Scene { village, game}
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }

    public void OnButtonPressed()
    {

    }
}
