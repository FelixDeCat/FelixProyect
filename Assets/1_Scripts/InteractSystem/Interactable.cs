using System;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] string info;
    public string Info { get { return info; } }

    [SerializeField] UnityEvent onInteract;

    public void Interact()
    {
        onInteract.Invoke();
    }
}
