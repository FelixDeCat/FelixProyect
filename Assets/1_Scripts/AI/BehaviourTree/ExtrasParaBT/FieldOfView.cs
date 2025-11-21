using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] Transform root;

    [SerializeField] float distMin = 10f;
    [SerializeField] float angleMin = 120f;

    [SerializeField] LayerMask toDetect;

    [SerializeField] bool isFOV = false;

    Transform target;

    public bool FOV(Transform target)
    {
        this.target = target;
        Vector3 dir = target.position - root.position;
        float dist = Vector3.SqrMagnitude(dir); // me lo dal al cuadrado ... comparar lo que comparar con un cuadrado Radio * Radio

        if (dist < distMin * distMin)
        {
            Vector3 forward = root.forward;

            float angle = Vector3.Angle(forward, dir);

            if (angle < angleMin * 0.5f)
            {
                Ray ray = new Ray(root.position + Vector3.up/2, dir);
                RaycastHit hit = new RaycastHit();

                if (Physics.Raycast(ray, out hit, dist, toDetect))
                {
                    if ((toDetect & 1 << hit.collider.gameObject.layer) != 0)
                    {
                        isFOV = true;
                        return true;
                    }
                }
            }
        }
        isFOV = false;
        return false;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if(target) Gizmos.DrawLine(root.position, target.position);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(root.position, distMin);

        Vector3 left = Quaternion.Euler(0,-angleMin /2f, 0) * transform.forward;
        Vector3 rigth = Quaternion.Euler(0, angleMin /2f, 0) * transform.forward;
        Gizmos.color = Color.red;

        Gizmos.DrawRay(root.position, left * distMin);
        Gizmos.DrawRay(root.position, rigth * distMin);

    }
}
