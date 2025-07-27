namespace IA.DecisionTree
{
    using UnityEngine;

    public class LN_Patrol : ActionNode
    {
        private void Start()
        {
            ConfigureActionNode(OnAction);
        }

        void OnAction()
        {
            print("Patrolling");
        }
    }
}

