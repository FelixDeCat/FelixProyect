using AI.Tools;
using System;
using UnityEngine;

public class EState : IState
{
    Action onEnter;
    Action onExit;
    Action onUpdate;
    public EState OnEnter(Action onEnter)
    {
        this.onEnter = onEnter;
        return this;
    }
    public EState OnExit(Action onExit)
    {
        this.onExit = onExit;
        return this;
    }
    public EState OnUpdate(Action onUpdate)
    {
        this.onUpdate = onUpdate;
        return this;
    }

    public EState SetCallbacks(Action _enter, Action _exit = null, Action _tick = null)
    {
        this.onEnter = _enter;
        this.onExit = _exit;
        this.onUpdate = _tick;
        return this;
    }

    void IState.Enter()
    {
        onEnter?.Invoke();
    }

    void IState.Exit()
    {
        onExit?.Invoke();
    }

    void IState.Update()
    {
       onUpdate?.Invoke();
    }
}
