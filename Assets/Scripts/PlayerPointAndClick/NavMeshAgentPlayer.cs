using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentPlayer : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;

    [SerializeField] LayerMask mask; // tome piso y obstaculos

    Vector3 point = Vector3.zero;

    bool move;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, int.MaxValue, mask))
            {
                point = hit.point;
                agent.SetDestination(point);
                move = true;
            }
        }
        

        //if (move)
        //{
        //    agent.enabled = true;
        //}

    }

    private void OnDrawGizmos()
    {
        if(move) Gizmos.DrawSphere(point, 0.3f);
    }
}
