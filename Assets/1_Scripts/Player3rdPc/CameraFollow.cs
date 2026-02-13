using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class CameraFollow : MonoSingleton<CameraFollow>
{
    float yaw;
    float pitch;
    float rotSpeed = 3;
    public enum CameraMode
    {
        none,
        thirdPersonCam,
        debug45, 
        lookAtPlayer
    }
    public CameraMode mode = CameraMode.thirdPersonCam;

    [Header("Third Person Options")]
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

    [Header("Inventory View Options")]
    [SerializeField] Transform lookAtPlayer;
    [SerializeField] GameObject[] inventoryAuxObjects;
    int cullingMaskDefault;

    [Header("Debug 45 Degrees Hard Follow")]
    public Vector3 offsetDeb = new Vector3(0, 10, -10);

    

    public override void SingletonAwake()
    {
        
    }

    void Start()
    {
        rig = target.GetComponent<Rigidbody>();
        yaw = target.eulerAngles.y;
        pitch = 0;
        cullingMaskDefault = Camera.main.cullingMask;
    }

    bool isActive;
    public void Activate(bool value) //toDO hacer una animacion si me lo pide el inventario tipo me acerco
    {
        isActive = value;

        Cursor.visible = !value;
        Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
    }

    bool oneshotposition = false;
    public void ChangeMode(CameraMode _mode)
    {
        mode = _mode;

        switch (_mode)
        {
            case CameraMode.none:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                 for (int i = 0; i < inventoryAuxObjects.Length; i++) inventoryAuxObjects[i].SetActive(false);
                break;

            case CameraMode.thirdPersonCam:
                oneshotposition = true;
                Camera.main.cullingMask = cullingMaskDefault;
                for (int i = 0; i < inventoryAuxObjects.Length; i++) inventoryAuxObjects[i].SetActive(false);
                break;

            case CameraMode.debug45:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                for (int i = 0; i < inventoryAuxObjects.Length; i++) inventoryAuxObjects[i].SetActive(false);
                break;

            case CameraMode.lookAtPlayer:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                for (int i = 0; i < inventoryAuxObjects.Length; i++) inventoryAuxObjects[i].SetActive(true);
                Camera.main.cullingMask = 1 << LayerMask.NameToLayer("PlayerRender");

                break;
        }
    }
    public void ActiveCursor(bool val)
    {
        Cursor.visible = val;
        Cursor.lockState = val ? CursorLockMode.None : CursorLockMode.Locked;
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
        if (!isActive) return;

        if (mode == CameraMode.thirdPersonCam)
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

            if (oneshotposition)
            {
                oneshotposition = false;
                transform.position = desiredPosition;
            }

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
        else if (mode == CameraMode.debug45)
        {
            transform.position = target.position + offsetDeb;
            transform.LookAt(target);
        }
        else if (mode == CameraMode.lookAtPlayer)
        {
            transform.position = lookAtPlayer.transform.position;
            transform.eulerAngles = lookAtPlayer.eulerAngles;
        }
        else if (mode == CameraMode.none)
        {
            //no hago nada
        }
    }

    
}
