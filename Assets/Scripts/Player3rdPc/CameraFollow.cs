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
    [SerializeField] Vector3 positionRelativeOffset = new Vector3(0.5f, 0, 0f);
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
    Vector3 camForwardFlat = Vector3.zero;
    Vector3 camRightFlat = Vector3.zero;
    Vector3 basePosition = Vector3.zero;
    Vector3 desiredPosition = Vector3.zero;
    Quaternion desiredRot = Quaternion.identity;
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

        basePosition =
            target.position -
             rotation * Vector3.forward * distance +
             Vector3.up * height;

        desiredPosition = basePosition + rotation * positionRelativeOffset;

        transform.position = Vector3.SmoothDamp
            (transform.position,
            desiredPosition,
            ref velocity,
            smoothTime);




        camForwardFlat = transform.forward;
        camForwardFlat.y = 0f;
        camForwardFlat.Normalize();

        // Derecha estable
        camRightFlat = Vector3.Cross(Vector3.up, camForwardFlat);


        Vector3 baseTarget = target.position;

        loookTarget = baseTarget +
            camRightFlat * lookRelativeOffset.x + 
            Vector3.up * lookRelativeOffset.y;


        desiredRot = Quaternion.LookRotation(loookTarget - transform.position);
        transform.rotation = desiredRot;
    }


    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawSphere(loookTarget, 0.1f);

    //    Gizmos.color = Color.green;
    //    Gizmos.DrawLine(transform.position, transform.position + camForwardFlat * 5);

    //    Gizmos.color = Color.black;
    //    Gizmos.DrawLine(transform.position, transform.position + camRightFlat * 5);
    //}
}
