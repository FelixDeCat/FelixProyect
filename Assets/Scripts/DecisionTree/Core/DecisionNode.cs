namespace IA.DecisionTree
{
    using System;
    using UnityEngine;

    public abstract class DecisionNode : Node
    {
        protected void ConfigureDecision(Func<bool> _predicate)
        {
            predicate = _predicate;
        }
        Func<bool> predicate = delegate { return false; };
        public Node nodeTrue;
        public Node nodeFalse;

        public override void Execute()
        {
            if (predicate.Invoke())
            {
                nodeTrue.Execute();
            }
            else
            {
                nodeFalse.Execute();
            }
        }
    }
}

