using UnityEngine;

public class Sequencer : Node  /// AND - Conexion en Serie
{
    public Sequencer(params Node[] _child) : base(_child)
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
                current++;

                if (current == childs.Count)
                {
                    Reset();
                    return Status.success;
                }

                continue;
            }

            if (status == Status.running)
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
        return Status.success;

    }

    public override void Reset()
    {
        current = 0;
        base.Reset();
    }
}
