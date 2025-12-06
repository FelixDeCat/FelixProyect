using System.Collections.Generic;
using UnityEngine;

public enum Status
{
    success,
    failure,
    running
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
