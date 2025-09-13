namespace IA.DecisionTree
{
    using System;
    using UnityEngine;

    public abstract class ActionNode : Node
    {
        public abstract void ExecuteAction();

        public override void Execute()
        {
            ExecuteAction();
        }
    }
}
