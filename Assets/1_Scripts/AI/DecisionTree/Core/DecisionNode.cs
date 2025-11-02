namespace IA.DecisionTree
{
    using System;
    using UnityEngine;

    public abstract class DecisionNode : Node
    {
        public Node nodeTrue;
        public Node nodeFalse;
        protected abstract bool Predicate();

        public override void Execute()
        {
            if (Predicate())
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

