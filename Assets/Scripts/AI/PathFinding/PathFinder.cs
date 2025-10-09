using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace IA.PathFinding
{
    public class PathFinder : MonoBehaviour
    {
        [SerializeField] float detection_radius;
        [SerializeField] LayerMask nodemask;

        bool walk = false;
        List<UnityNode> path = new List<UnityNode>();

        [SerializeField] float closeDist = 0.5f;
        [SerializeField] float speed = 5f;

        // Start is called before the first frame update
        void Start()
        {
            Target.instance.SubscribeToEndClick(OnClick);
        }

        void OnClick()
        {
            UnityNode initial = FindMostClosestNode(transform.position);
            UnityNode final = FindMostClosestNode(Target.Position);

            if (initial == null) Debug.LogError("<color=yellow> no se encontro el inicial");
            if (final == null) Debug.LogError("<color=yellow> no se encontro el final");

            //path.Clear();

            path = BFS(initial, final);

            if (path != null)
            {
                index = 0;
                walk = true;
            }

        }

        int index = 0;
        private void Update()
        {
            if (walk)
            {
                Vector3 direction = path[index].transform.position - transform.position;

                if (direction.magnitude < closeDist)
                {
                    index = index + 1;
                    if (index >= path.Count)
                    {
                        index = 0;
                        walk = false;
                    }
                }
                else
                {
                    transform.position = transform.position + direction * Time.deltaTime * speed;
                }
            }
        }

        List<UnityNode> BFS(UnityNode initial, UnityNode final)
        {
            //foreach (var node in NodeBuilder.Instance.Nodes)
            //{
            //    node.Clean();
            //}

            Queue<UnityNode> open = new Queue<UnityNode>();
            List<UnityNode> visited = new List<UnityNode>();

            open.Enqueue(initial);
            visited.Add(initial);

            while (open.Count > 0)
            {
                UnityNode current = open.Dequeue();

                if (current == final)
                {
                    return Reconstruct(initial, final);
                }

                foreach (UnityNode n in current.Neighbors)
                {
                    if (visited.Contains(n)) continue;

                    n.SetParent(current);
                    visited.Add(n);
                    open.Enqueue(n);
                }
            }
            return null;
        }

        List<UnityNode> Reconstruct(UnityNode initial, UnityNode final)
        {
            List<UnityNode> list = new List<UnityNode>();
            UnityNode current = final;

            while (current != null && current != initial)
            {
                list.Add(current);
                current = current.Parent;
            }

            list.Add(initial);
            list.Reverse();

            return list;
        }

        float mostClose;
        UnityNode bestNode;
        UnityNode FindMostClosestNode(Vector3 point)
        {
            Collider[] cols = Physics.OverlapSphere(point, detection_radius, nodemask);

            bestNode = null;
            mostClose = detection_radius + 1;

            for (int i = 0; i < cols.Length; i++)
            {
                UnityNode node = cols[i].GetComponent<UnityNode>();

                if (node != null)
                {
                    Vector3 dir = point - node.transform.position;

                    if (dir.magnitude < mostClose)
                    {
                        mostClose = dir.magnitude;
                        bestNode = node;
                    }
                }
            }

            return bestNode;
        }
    }
}
