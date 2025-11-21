using System.Collections.Generic;
using UnityEngine;

public enum Status
{
    success,
    failure,
    running
}


class example
{
    void Main()
    {
        //Sequence root = new Sequence();
    }
}


public class Sequence : Node
{
    public Sequence(params Node[] _child) : base(_child)
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
}


public abstract class Node
{
    protected List<Node> childs;
    public abstract Status Evaluate();
    public Node(params Node[] _childs)
    {
        childs = new List<Node>(_childs);
    }
    public virtual void Reset()
    {
        for (int i = 0; i < childs.Count; i++)
        {
            childs[i].Reset();
        }
    }
}
