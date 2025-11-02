namespace IA.PathFinding
{
    using System.Linq;
    using UnityEngine;
    using System.Collections.Generic;

    public class UnityNode : MonoBehaviour
    {

        [SerializeField] float detectionRadius = 2f;

        [SerializeField] List<UnityNode> neighbors;
        public List<UnityNode> Neighbors { get { return neighbors; } }

        UnityNode parent = null;
        public UnityNode Parent
        {
            get { return parent; }
        }
        public void SetParent(UnityNode _p)
        {
            parent = _p;
        }

        public void Clean()
        {
            parent = null;
        }

        [SerializeField] LayerMask nodeMask;
        [SerializeField] LayerMask maskview;
        [SerializeField] LayerMask floorAndObstacles;

        [SerializeField] float maxSlope = 0.5f;

        [Header("Gizmos")]
        [SerializeField] bool drawRadius = false;
        [SerializeField] bool drawsphere = false;
        [SerializeField] bool drawConnections = false;

        [SerializeField] float floorUPOffset = 0.1f;

        public void BakeNeighbors()
        {
            Adjust();
            Detect();
        }

        void Adjust()
        {

            if (Physics.Raycast(transform.position + Vector3.up * 10, Vector3.down, out RaycastHit hit, 20, floorAndObstacles))
            {
                transform.position = hit.point + Vector3.up * floorUPOffset;
            }
        }

        void Detect()
        {
            neighbors = new List<UnityNode>();
            Collider[] colls = Physics.OverlapSphere(transform.position, detectionRadius, nodeMask);

            for (int i = 0; i < colls.Length; i++)
            {
                UnityNode node = colls[i].GetComponent<UnityNode>();
                if (node != null && node != this)
                {
                    Vector3 dir = node.transform.position - transform.position;

                    Ray ray = new Ray();
                    ray.origin = transform.position;
                    ray.direction = dir;

                    if (Physics.Raycast(ray, out RaycastHit hit, dir.magnitude, maskview))
                    {
                        UnityNode hitNode = hit.collider.GetComponent<UnityNode>();
                        if (hitNode != null && hitNode == node)
                        {
                            float h = node.transform.position.y - transform.position.y;

                            if (Mathf.Abs(h) < maxSlope)
                            {
                                neighbors.Add(node);
                            }
                        }
                    }
                }
            }
        }


        private void OnDrawGizmos()
        {
            if (drawRadius)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(transform.position, detectionRadius);
            }

            if (drawConnections)
            {
                Gizmos.color = Color.white;

                if (neighbors != null)
                {
                    for (int i = 0; i < neighbors.Count; i++)
                    {
                        Vector3 dir = neighbors[i].transform.position - transform.position;

                        dir /= 3;

                        Gizmos.DrawLine(transform.position, transform.position + dir);

                    }
                }
            }

            if (drawsphere)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawSphere(transform.position, 0.05f);
            }
        }
    }

}
