using UnityEngine;

namespace AI.BTTest
{
    public class WaitNode : Node
    {
        float timer = 0f;
        float time = 1;

        public WaitNode(float time, params Node[] nodes) : base(nodes)
        {
            this.time = time;
        }

        public override Status Evaluate()
        {
            timer = timer + 1 * Time.deltaTime;

            if (timer < time)
            {
                return Status.running;
            }

            return Status.sucess;
        }

        public override void Reset()
        {
            timer = 0f;
            base.Reset();
        }
    }

}