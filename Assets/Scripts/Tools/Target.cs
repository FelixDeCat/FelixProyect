using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target: MonoBehaviour
{
    public static Vector3 Position { get => instance.transform.position; }
    [SerializeField] float speed = 1f;
    [SerializeField] GameObject onClickGO;

    Vector3 velocity;
    public static Vector3 Velocity
    {
        get 
        {
            return instance.velocity;
        }
    }

    public static Target instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else throw new System.Exception("No puede existir dos Targeter");

        transform.localScale = Vector3.one * 0.5f;
        onClickGO.SetActive(false);
    }

    Vector3 dir = Vector3.zero;
    Vector3 adjust = Vector3.zero;

    Action onEndClick;
    public void SubscribeToEndClick(Action onEndClick)
    {
        this.onEndClick = onEndClick;
    }

    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, float.MaxValue, 1 << 6))
            {
                transform.position = hit.point;
                transform.localScale = Vector3.one;
                onClickGO.SetActive(true);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            onEndClick?.Invoke();
        }

        dir.x = Input.GetAxisRaw("Horizontal");
        dir.z = Input.GetAxisRaw("Vertical");

        dir.Normalize();

        transform.localScale = Vector3.one;
        onClickGO.SetActive(true);

        //velocity tiene 2 informaciones : direccion y longitud

        velocity = dir * speed;

        transform.position += velocity * Time.deltaTime;
        adjust = transform.position;
        //adjust.y = 0;
        transform.position = adjust;


        if (!Input.GetMouseButton(0) && dir.magnitude < 0.1f)
        {
            transform.localScale = Vector3.one * 0.5f;
            onClickGO.SetActive(false);
        }


    }
}
