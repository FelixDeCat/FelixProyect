namespace IA.DecisionTree
{
    using System;
    using UnityEngine;

    public abstract class ActionNode : Node
    {
        public Action action;
        protected void ConfigureActionNode(Action _action)
        {
            action = _action;
        }

        public override void Execute()
        {
            action.Invoke();
        }
    }
}
