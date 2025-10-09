namespace IA.PathFinding
{
    using UnityEngine;

    [ExecuteInEditMode]
    public class NodeBaker : MonoBehaviour
    {
        [SerializeField] bool bake = false;
        [SerializeField] bool update = false;


        void Update()
        {
            if (bake || update)
            {
                bake = false;
                var nodes = GetComponentsInChildren<UnityNode>();

                for (int i = 0; i < nodes.Length; i++)
                {
                    nodes[i].BakeNeighbors();
                }

            }
        }
    }

}
