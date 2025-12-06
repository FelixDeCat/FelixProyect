using System;
using UnityEngine;

public class Leaf : Node
{
    Func<Status> toDo;
    Action onReset;

    public Leaf(Func<Status> _toDo, Action _onReset = null)
    {
        toDo = _toDo;
        onReset = _onReset;
    }

    public override Status Evaluate()
    {
        return toDo.Invoke();
    }

    public override void Reset()
    {
        onReset?.Invoke();
    }
}
