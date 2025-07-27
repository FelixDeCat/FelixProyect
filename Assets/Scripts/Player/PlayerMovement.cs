using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    Vector3 input;
    [SerializeField] float speed;
    
    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.z = Input.GetAxis("Vertical");
        input.Normalize();
    }
    private void FixedUpdate()
    {
       
        rb.linearVelocity = input * Time.fixedDeltaTime * speed;
    }
}
