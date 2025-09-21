using UnityEditor;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;
    private void Awake()
    {
        instance = this;
    }

    float yaw;
    float pitch;
    float rotSpeed = 3;

    [SerializeField] float minPitch = -30;
    [SerializeField] float maxPitch = 60;

    [SerializeField] Transform target;

    [SerializeField] float distance = 5;
    [SerializeField] float height = 2;

    [SerializeField] float smoothTime = 0.1f;

    Vector3 velocity = Vector3.zero;

    [SerializeField] Vector3 lookRelativeOffset = new Vector3(0.5f, 0.2f, 0f);
    Vector3 loookTarget;

    Rigidbody rig;


    void Start()
    {
        rig = target.GetComponent<Rigidbody>();
        yaw = target.eulerAngles.y;
        pitch = 0;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public Vector3 CameraForward
    {
        get
        {
            return transform.forward;
        }
    }

    private void LateUpdate()
    {
        //capturo los inputs
        float mouseX = Input.GetAxis("Mouse X");
        float mousey = Input.GetAxis("Mouse Y");

        //se los sumo a yaw y pitch, ademas lo clampeo para no pasarse
        yaw += mouseX * rotSpeed;
        pitch -= mousey * rotSpeed;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // calculo la rotacion final en quaternion
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        Vector3 desiredPosition =
            rig.position -
             rotation * Vector3.forward * distance +
             Vector3.up * height;

        //transform.position = desiredPosition;

        transform.position = Vector3.SmoothDamp
            (transform.position,
            desiredPosition,
            ref velocity,
            smoothTime);


        Vector3 baseTarget = target.position + Vector3.up * 0.5f ;

        loookTarget = baseTarget + transform.right * lookRelativeOffset.x 
            + transform.up * lookRelativeOffset.y
            + transform.forward * lookRelativeOffset.z;

        Quaternion desiredRot = Quaternion.LookRotation(loookTarget - transform.position);
        transform.rotation = desiredRot;
    }
}
