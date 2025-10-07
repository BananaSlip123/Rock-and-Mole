using System;
using UnityEngine;

public interface IDoorBehaviour
{
    void EnterToRoom();
    void ChooseBehaviour(int n);
    void ChooseEvent(int n);
    void ChangeBehaviour(Action n);
}
