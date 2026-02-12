using System;
using UnityEngine;
using AI.Tools;

[System.Serializable]
public class MoveControl
{
    Func<bool> IsGrounded;
    public void IsGroundedCallback(Func<bool> _isGrounded) => IsGrounded = _isGrounded;

    [SerializeField] Transform camera;

    [SerializeField] Rigidbody rb;
    Vector3 input;
    [SerializeField] float speed = 5f;
    [SerializeField] float turnSpeed = 10f;
    [SerializeField] float jumpForce = 5f;
    Vector3 moveDir = Vector3.zero;
    Quaternion slerp = Quaternion.identity;
    Quaternion desiredRot = Quaternion.identity;
    float originalY = 0;
    bool ground = true;

    public float InputMagnitude { get { return input.magnitude; } }

    bool canMove = true;

    public void Tick()
    {
        input.x = Input.GetAxis("Horizontal");
        input.z = Input.GetAxis("Vertical");

        ground = IsGrounded();

        if (Input.GetButtonDown("Jump") && ground)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
    }

    Vector3 result = Vector3.zero;

    public void FixedTick(float speedMultiplier)
    {
        originalY = rb.linearVelocity.y;

        moveDir = camera.transform.TransformDirection(input);
        moveDir.y = 0;

        if (moveDir.sqrMagnitude > 0.01f)
        {
            moveDir.Normalize();

            if (ground)
            {
                result.x = moveDir.x * speed * speedMultiplier;
                result.y = originalY;
                result.z = moveDir.z * speed * speedMultiplier;

                rb.linearVelocity = result;
            }

            desiredRot = Quaternion.LookRotation(moveDir);
            slerp = Quaternion.Slerp(rb.rotation, desiredRot, turnSpeed * Time.deltaTime);
            rb.MoveRotation(slerp);
        }
        else
        {
            rb.linearVelocity = new Vector3(
                ground ? 0 : rb.linearVelocity.x,
                originalY,
                ground ? 0 : rb.linearVelocity.z);
        }
    }
}
