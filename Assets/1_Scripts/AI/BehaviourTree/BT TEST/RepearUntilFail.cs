namespace AI.BTTest
{
    using UnityEngine;
    public class RepearUntilFail : Node
    {
        public RepearUntilFail(Node child) : base(child) { }

        public override Status Evaluate()
        {
            var status = childs[0].Evaluate();

            if (status == Status.failure)
            {
                childs[0].Reset();
                return Status.sucess;
            }

            if (status == Status.sucess)
            {
                childs[0].Reset();
                return Status.running;
            }

            return Status.running;
        }
    }
}
