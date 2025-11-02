using System;
using UnityEngine;

[System.Serializable]
public class CharacterController : IStarteable, IActivable, IUpdateable, IFixedUpdateable
{
    Func<bool> IsGrounded;
    public void IsGroundedCallback(Func<bool> _isGrounded) => IsGrounded = _isGrounded;

    CameraFollow cam;
    void IStarteable.Start()
    {
        cam = CameraFollow.instance;
    }

    bool active = false;
    void IActivable.Active()
    {
        active = true;
    }
    void IActivable.Deactivate()
    {
        active = false;
    }

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
    void IUpdateable.Tick(float delta)
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
    void IFixedUpdateable.FixedTick(float delta)
    {
        originalY = rb.linearVelocity.y;

        moveDir = cam.transform.TransformDirection(input);
        moveDir.y = 0;

        if (moveDir.sqrMagnitude > 0.01f)
        {
            moveDir.Normalize();

            if (ground)
            {
                result.x = moveDir.x * speed;
                result.y = originalY;
                result.z = moveDir.z * speed;

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
