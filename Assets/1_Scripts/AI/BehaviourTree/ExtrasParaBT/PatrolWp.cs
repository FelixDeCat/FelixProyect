using UnityEngine;

public class PatrolWp : MonoBehaviour
{
    [SerializeField] Transform[] waypoints;
    int _i = 0;
    int I
    {
        get => _i;
        set
        {
            if (value >= waypoints.Length) _i = 0;
            else _i = value;
        }
    }

    [SerializeField] float distToChange = 1f;

    public Vector3 GetWp(Vector3 posOrigin)
    {
        Vector3 dir = waypoints[I].position - posOrigin;

        if (dir.sqrMagnitude <= distToChange * distToChange)
        {
            I++;

        }
        return waypoints[I].position;
    }

    private void OnDrawGizmos()
    {
        if (waypoints == null) return;
        if (waypoints.Length <= 0) return;

        for (int i = 0; i < waypoints.Length; i++)
        {
            if (i >= waypoints.Length -1)
            {
                Gizmos.DrawLine(waypoints[i].position, waypoints[0].position);
            }
            else
            {
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
            }
        }
    }
}
