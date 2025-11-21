namespace AI.BTTest
{

    using UnityEngine;

    public class Sequence : Node // AND
    {
        public Sequence(params Node[] _childs) : base(_childs) { }

        int current = 0;

        public override Status Evaluate()
        {
            while (current < childs.Count)
            {
                var status = childs[current].Evaluate();

                if (status == Status.sucess)
                {
                    current++;

                    if (current == childs.Count)
                    {
                        Reset();
                        return Status.sucess;
                    }

                    continue;
                }

                if (status == Status.running) // si al menos 1 no cumple se sale sin sucess
                {
                    return Status.running;
                }

                if (status == Status.failure)
                {
                    Reset();
                    return Status.failure;
                }
            }

            Reset();
            return Status.sucess; // cuando se haya terminado y no cortó nadie, sucess
        }

        public override void Reset()
        {
            current = 0;
            base.Reset();
        }
    }

}
