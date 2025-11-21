namespace AI.BTTest
{

    using System;
    using UnityEngine;

    public class Leaf : Node
    {
        Func<Status> action;
        Action onReset;
        public Leaf(Func<Status> _action, Action _onReset = null, params Node[] _childs) : base(_childs)
        {
            this.action = _action;
            this.onReset = _onReset;
        }

        public override Status Evaluate()
        {
            return action.Invoke();
        }

        public override void Reset()
        {
            onReset?.Invoke();
        }
    }

}
