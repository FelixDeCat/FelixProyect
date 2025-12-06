using UnityEngine;

public class RepeatUntilFail : Node
{
    public RepeatUntilFail(Node child) : base(child) { }

    public override Status Evaluate()
    {
        var status = childs[0].Evaluate();

        if (status == Status.failure)
        {
            childs[0].Reset();
            return Status.success;
        }

        if (status == Status.success)
        {
            childs[0].Reset();
            return Status.running;
        }

        return Status.running;
    }
}
