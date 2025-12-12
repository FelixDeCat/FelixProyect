using UnityEngine;
using System;

public class ParticleStopListener : MonoBehaviour
{
    Action OnStopped;

    [SerializeField] ParticleSystem ps;
    public void Initialize(Action _OnStopped)
    {
        var main = ps.main;
        main.stopAction = ParticleSystemStopAction.Callback;
        this.OnStopped = _OnStopped;
    }

    private void OnParticleSystemStopped()
    {
        OnStopped.Invoke();
    }
}