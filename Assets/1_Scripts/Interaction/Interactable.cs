using System;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    Action onInteract;
    Func<string> onGetInfo;

    public string Info => onGetInfo.Invoke();

    public void SetInteractable(Action _onInteract, Func<string> _onGetInfo)
    {
        onInteract = _onInteract;
        onGetInfo = _onGetInfo;
    }

    public void Interact()
    {
        if (onInteract == null) throw new Exception("no se configuró el callback escencial del interactuable, ni tampoco el local");
        onInteract.Invoke();
    }
}
