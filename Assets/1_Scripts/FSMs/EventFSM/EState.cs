using AI.Tools;
using System;
using UnityEngine;

public class EState : IState, IStarteable
{
    Action onInitialize;
    Action onEnter;
    Action onExit;
    Action onUpdate;
    public EState OnInitialize(Action onInitialize)
    {
        this.onInitialize = onInitialize;
        return this;
    }
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

    void IStarteable.Start()
    {
        onInitialize?.Invoke();
    }
}
