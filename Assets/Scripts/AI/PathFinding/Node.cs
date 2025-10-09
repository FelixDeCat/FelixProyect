namespace IA.PathFinding.Core
{
    using System.Collections.Generic;
    using System.Linq;

    public class Node
    {
        float x = 0f;
        float y = 0f;
        float z = 0f;
        Node[] nodes = null;

        public Node(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void AddNeighborhoods(params Node[] _nodes)
        {
            if (_nodes == null || _nodes.Length == 0) throw new System.ArgumentException("Los nodos pasados por parametro son null o vacios");
            if (nodes == null || nodes.Length == 0)
            {
                nodes = _nodes;
            }
            else
            {
                HashSet<Node> current = new HashSet<Node>(nodes);
                current.UnionWith(_nodes);
                nodes = current.ToArray();
            }
        }




    }

}
