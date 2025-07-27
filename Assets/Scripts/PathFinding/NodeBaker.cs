namespace IA.PathFinding
{
    using UnityEngine;

    [ExecuteInEditMode]
    public class NodeBaker : MonoBehaviour
    {
        [SerializeField] bool bake = false;
        [SerializeField] bool update = false;
        [SerializeField] float radius = 1.8f;
        [SerializeField] LayerMask mask;


        void Update()
        {
            if (bake || update)
            {
                bake = false;
                var nodes = GetComponentsInChildren<UnityNode>();

                for (int i = 0; i < nodes.Length; i++)
                {
                    nodes[i].EditorBakeNeighborhoods(radius, mask);
                }

            }
        }
    }

}
