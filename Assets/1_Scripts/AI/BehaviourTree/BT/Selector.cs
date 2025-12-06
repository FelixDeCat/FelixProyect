using UnityEngine;

public class Selector : Node // OR
{
    public Selector(params Node[] _childs) : base(_childs)
    {
    }

    int current = 0;

    public override Status Evaluate()
    {
        while (current < childs.Count)
        {
            var status = childs[current].Evaluate();

            if (status == Status.success)
            {
                Reset();
                return Status.success;
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

        Reset();
        return Status.failure;

    }

    public override void Reset()
    {
        current = 0;
        base.Reset();
    }
}
