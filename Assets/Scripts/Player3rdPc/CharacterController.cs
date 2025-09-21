using UnityEngine;

[System.Serializable]
public class CharacterController: IStarteable ,IActivable, IUpdateable, IFixedUpdateable
{

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
    float moveY = 0;
    void IUpdateable.Tick(float delta)
    {
        input.x = Input.GetAxis("Horizontal");
        input.z = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
    }

    void IFixedUpdateable.FixedTick(float delta)
    {
        // me capturo el y en Velocity, pensando que el velocity venia con algo.
        // por ejemplo, me estaba moviendo o me estaba cayendo. 
        // esto me sirve para luego el input no me sobreescriba ese calculo que ya hizo
        // el motor de fisica
        moveY = rb.linearVelocity.y;

        // convierto ese input local a global pero con respecto a la camara
        moveDir = cam.transform.TransformDirection(input);

        // no necesito el y
        moveDir.y = 0;

        // si tengo algo de magnitud hago el resto
        if (moveDir.sqrMagnitude > 0.01f)
        {
            // este normalize vease que lo pongo aca y no antes para
            // que la comparacion del sqrmagnitude sea mas precisa
            moveDir.Normalize();

            // me muevo normal como siempre
            rb.linearVelocity = moveDir * speed;

            // le regreso el "Y" original que tenia antes de modificarlo con mi input
            // si me estoy cayendo o saltando por ejemplo, el input no interfiere con la caida
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, moveY, rb.linearVelocity.z);

            // convierto esa direccion a "Cantidad de rotacion necesaria o deseada"
            desiredRot = Quaternion.LookRotation(moveDir);

            // Roto suavemente
            slerp = Quaternion.Slerp(rb.rotation, desiredRot, turnSpeed * Time.deltaTime);


            rb.MoveRotation(slerp);
        }
        else
        {
            rb.linearVelocity = new Vector3(0, moveY, 0);
        }
    }
}
