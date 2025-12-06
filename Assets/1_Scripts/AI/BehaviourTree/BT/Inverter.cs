using UnityEngine;

public class Inverter : Node  // DECORATOR 1 solo child
{
    public Inverter(Node child) : base(child)
    {

    }

    public override Status Evaluate()
    {
        var status = childs[0].Evaluate();

        if (status == Status.failure) return Status.success;
        if (status == Status.success) return Status.failure;

        return Status.running;
    }
}
