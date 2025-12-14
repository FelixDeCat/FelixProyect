using System;
using UnityEngine;
using AI.Tools;

[System.Serializable]
public class CharControl_State : StateBase, IStarteable, IFixedUpdateable
{
    Func<bool> IsGrounded;
    public void IsGroundedCallback(Func<bool> _isGrounded) => IsGrounded = _isGrounded;

    CameraFollow cam;

    PlayerView view;


    Action onHit;
    public void SetView(PlayerView view)
    {
        this.view = view;
    }

    void IStarteable.Start()
    {
        cam = CameraFollow.Instance;
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

    public override void OnEnter()
    {
        view.SubscribeToEvent("hit", ANIM_EV_Hit);
    }

    public override void OnExit()
    {
        
    }

    protected override void OnPause()
    {
        
    }
    protected override void OnResume()
    {
        
    }

    public void SubscribeToDoHit(Action onHit)
    {
        this.onHit = onHit;
    }
    void ANIM_EV_Hit()
    {
        Debug.Log("IsHitting");
        onHit?.Invoke();
    }

    public override void OnUpdate()
    {
        input.x = Input.GetAxis("Horizontal");
        input.z = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Fire1"))
        {
            view.Animate_Fire();
        }

        ground = IsGrounded();

        if (Input.GetButtonDown("Jump") && ground)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }

        view.Animate_Motion_Basic(input.magnitude);
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
