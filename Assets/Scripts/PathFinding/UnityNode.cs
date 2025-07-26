using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class UnityNode : MonoBehaviour
{
    [SerializeField] UnityNode[] neighborhoods;
    [SerializeField] bool drawGizmos;

    public void EditorBakeNeighborhoods(float radius, int layermask)
    {
        Collider[] cols = Physics.OverlapSphere(this.transform.position, radius, layermask);
        List<UnityNode> nodes = new List<UnityNode>();

        for (int i = 0; i < cols.Length; i++)
        {
            UnityNode node = cols[i].GetComponent<UnityNode>();
            if (node.Equals(this)) continue;

            Vector3 dir = node.transform.position - this.transform.position;
            dir.Normalize();

            RaycastHit hit;

            if (Physics.Raycast(transform.position, dir, out hit))
            {
                var hitted = hit.collider.GetComponent<UnityNode>();
                if (hitted != null && hitted.Equals(node))
                {
                    nodes.Add(node);
                }
            }

        }

        neighborhoods = nodes.ToArray();
    }

    private void OnDrawGizmos()
    {
        if (!drawGizmos) return;
        for (int i = 0; i < neighborhoods.Length; i++)
        {
            Gizmos.color = Color.white;

            Vector3 dir = neighborhoods[i].transform.position - transform.position;
            dir.Normalize();
            dir /= 2;

            Ray ray = new Ray(transform.position, dir);

            Gizmos.DrawLine(transform.position, transform.position + dir);

            Gizmos.color = Color.black;
            Gizmos.DrawSphere(transform.position, 0.1f);
        }
    }
}
