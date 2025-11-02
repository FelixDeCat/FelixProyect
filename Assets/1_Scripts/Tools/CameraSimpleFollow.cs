using UnityEngine;

public class CameraSimpleFollow : MonoBehaviour
{
    public Transform target;

    Vector3 offset = Vector3.zero;
    void Start()
    {
        offset = transform.position - target.position;
    }
    void LateUpdate()
    {
        transform.position = Vector3.Slerp(transform.position, target.position + offset, 0.1f);
    }
}
