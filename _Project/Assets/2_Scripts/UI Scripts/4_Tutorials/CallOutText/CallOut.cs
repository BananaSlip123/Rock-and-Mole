using UnityEngine;
using System.Collections;
using TMPro;
using NUnit.Framework;
public class CallOut : MonoBehaviour
{
    //Dentro de un prefab (con un cuadro de texto y un botón)

    //le pasas un array de diálogos
    //empieza a escribir el primero, si das a click se interrumpe para mostrarlo entero
    //si no das click se escribe hasta completarse
    //una vez q esta el texto entero, si das click comienza a escribir el siguiente de la lista
    //si das click sobre el ultimo elemento de la lista te lo desactiva

    #region PRIVATE VARS
    [SerializeField] TextMeshProUGUI txt_callOutTextBox;

    string[] _dialogs;
    int _currentDialog = 0;
    State _state = State.disabled;

    #endregion

    #region PUBLIC PROPERTIES
    string CurrentText
    {
        set => txt_callOutTextBox.text = value;
    }
    State StateValue
    {
        set
        {
            if (_state == value) return; //el setter funciona ante cambios de valor solo
            
            if(_state == State.disabled) gameObject.SetActive(true); //si antes estaba desactivado lo activamos

            _state = value; //actualizamos el valor
            switch (value) //acción especifica a cada nuevo valor
            {
                case State.disabled:
                    gameObject.SetActive(false);
                    _dialogs = null;
                    break;
                case State.writingText:
                    StartCoroutine(Writing());
                    break;
                case State.showText:
                    CurrentText = _dialogs[_currentDialog];
                    break;
            }
        }
    }
    #endregion

    #region PUBLIC FUNCS
    public void StartCallOut(string[] dialogs)
    {
        if (dialogs == null || dialogs.Length < 1)
            throw new System.Exception("_dialogs must have at least 1 dialog");

        _dialogs = dialogs;
        StateValue = State.writingText;
    }
    public void OnInteraction()
    {
        //llamada al hacer click o con alguna tecla 
        //pasa los diálogos
        if (_state == State.writingText) StopWriting();
        else if (_state == State.showText) AdvanceDialog();
    }
    #endregion

    #region PRIVATE FUNCS
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    IEnumerator Writing()
    {
        string text = "";

        try
        {
            foreach (char Character in _dialogs[_currentDialog])
            {
                text += Character;
                CurrentText = text;
                yield return new WaitForSeconds(0.2f);
            }
        }
        finally //tanto si usas stop como si acaba el for
        {
            StateValue = State.showText;
        }
    }
    void StopWriting()
    {
        StopCoroutine(Writing());
        StateValue = State.showText;
    }

    void AdvanceDialog()
    {
        if(_currentDialog == _dialogs.Length-1) //saltas el ultimo
        {
            StateValue = State.disabled;
        }
        else
        {
            _currentDialog++;
            StateValue = State.writingText;
        }
    }

    #endregion
    public enum State
    {
        disabled, //oculto
        writingText, //se llena letra a letra
        showText, //muestra el texto completo
    }
}
