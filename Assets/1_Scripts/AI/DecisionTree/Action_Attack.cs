namespace AI.DecisionTree
{
    using UnityEngine;

    public class Action_Attack : ActionNode
    {
        public override void ExecuteAction()
        {
            print("Attacking...");
        }
    }

}