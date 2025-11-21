namespace AI.BTTest
{
    using UnityEngine;

    public class Selector : Node // OR
    {
        int current = 0;
        public Selector(params Node[] _childs) : base(_childs)
        {
        }

        public override Status Evaluate()
        {
            while (current < childs.Count)
            {
                Status status = childs[current].Evaluate();

                if (status == Status.sucess)
                {
                    Reset();
                    return Status.sucess;
                }

                if (status == Status.running)
                {
                    return Status.running;
                }

                if (status == Status.failure)
                {
                    childs[current].Reset();
                    current++;
                    continue;
                }
            }

            //todos fallaron
            Reset();
            return Status.failure;
        }

        public override void Reset()
        {
            current = 0;
            base.Reset();
        }
    }

}
