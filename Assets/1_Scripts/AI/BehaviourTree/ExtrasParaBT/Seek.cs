using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : MonoBehaviour
{
    Vector3 desired = Vector3.zero; // vector deseado que apunta al target
    Vector3 velocity = Vector3.zero; // Direccion y Magnitud del vector
    Vector3 steering = Vector3.zero; // Vector de ajuste / steering

    Vector3 dir = Vector3.zero;

    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float steeringForce = 0.1f;
    
    public void DoSeek(Transform target, float speed = 3f)
    {
        dir = target.position - transform.position;
        desired = dir.normalized * moveSpeed;
        steering = desired - velocity;
        steering = Vector3.ClampMagnitude(steering, steeringForce);
        velocity = Vector3.ClampMagnitude(velocity + steering, moveSpeed);
        transform.position += velocity * Time.deltaTime;
        transform.forward = desired;
    }
    public void DoSeek(Vector3 pos, float speed = 3f)
    {
        dir = pos - transform.position;
        desired = dir.normalized * moveSpeed;
        steering = desired - velocity;
        steering = Vector3.ClampMagnitude(steering, steeringForce);
        velocity = Vector3.ClampMagnitude(velocity + steering, moveSpeed);
        transform.position += velocity * Time.deltaTime;
        transform.forward = desired;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green; //desired
        Gizmos.DrawLine(transform.position, transform.position + desired);

        Gizmos.color = Color.red; //velocity
        Gizmos.DrawLine(transform.position, transform.position + velocity);

        Gizmos.color = Color.blue; //steering
        Gizmos.DrawLine(transform.position, transform.position + steering * 50);
    }
}
