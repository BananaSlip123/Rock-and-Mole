using UnityEngine;
using System.Collections;
using System;
using TMPro;
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
                    Disable();
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

    #region PUBLIC VARS
    public Action OnCallOutDisable = null;
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
        
      //  if (_state == State.writingText) StopWriting();
        if (_state == State.showText) AdvanceDialog();
    }

    public void Disable()
    {
        StopAllCoroutines();
        _dialogs = null;
        _currentDialog = 0;
        gameObject.SetActive(false);
    }
    #endregion

    #region PRIVATE FUNCS
    private void Awake()
    {
        this.gameObject.SetActive(false); //un tutorial script lo despierta
    }
    IEnumerator DelayedAction(Action a, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        a.Invoke();
    }
    IEnumerator Writing()
    {
        string text = "";

        
        foreach (char Character in _dialogs[_currentDialog])
        {
            text += Character;
            CurrentText = text;
            yield return new WaitForSeconds(0.06f);
        }
       
        StateValue = State.showText;
    }
    
    private void OnEnable()
    {
        //_canInteract = true;
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
            OnCallOutDisable?.Invoke();
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
