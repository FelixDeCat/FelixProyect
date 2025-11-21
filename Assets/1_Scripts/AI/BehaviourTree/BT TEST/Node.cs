namespace AI.BTTest
{

    using UnityEngine;
    using System.Collections.Generic;

    public enum Status
    {
        sucess,
        failure,
        running
    }

    public abstract class Node
    {
        public abstract Status Evaluate();

        [SerializeField] protected List<Node> childs;

        public Node(params Node[] _childs)
        {
            childs = new List<Node>(_childs);
        }

        public virtual void Reset()
        {
            for (int i = 0; i < childs.Count; ++i)
            {
                childs[i].Reset();
            }
        }
    }

}
