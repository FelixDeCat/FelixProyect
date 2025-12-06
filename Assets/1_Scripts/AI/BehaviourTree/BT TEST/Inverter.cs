namespace AI.BTTest
{
    using UnityEngine;
    public class Inverter : Node // decorator
    {
        public Inverter(Node child = null) : base(child)
        {

        }
        public override Status Evaluate()
        {
            var status = childs[0].Evaluate();

            if(status == Status.failure) return Status.sucess;
            if(status == Status.sucess) return Status.failure;

            return Status.running;
        }
    }
}
