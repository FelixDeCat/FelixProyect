namespace IA.DecisionTree
{
    using UnityEngine;

    public class LN_Attack : ActionNode
    {
        private void Start()
        {
            ConfigureActionNode(OnAction);
        }

        void OnAction()
        {
            print("Attacking...");
        }
    }

}