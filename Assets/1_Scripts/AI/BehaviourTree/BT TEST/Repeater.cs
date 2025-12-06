namespace AI.BTTest
{
    using UnityEngine;
    public class Repeater : Node
    {
        int repeatCount = -1;
        int currentCount = 0;

        public Repeater( Node child, int times = -1) : base(child)
        {
            repeatCount = times;
            currentCount = 0;
        }
        public override Status Evaluate()
        {
            var childStatus = childs[0].Evaluate();

            //sigo corriendo si el child sigue corriendo
            if(childStatus == Status.running) return Status.running;

            // si llego aca es porque no es Running y es Success o Fail

            childs[0].Reset();
            currentCount++;


            if (repeatCount < 0 || currentCount < repeatCount)
            {
                return Status.running;
            }

            Reset();
            return Status.sucess;

        }

        public override void Reset()
        {
            currentCount = 0;
            base.Reset();
        }
    }
}
