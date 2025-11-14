using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Billboard : MonoBehaviour
{
    public enum LookAxis { forward, backward, rigth, left, up, down };
    public LookAxis axis;
    Transform cam;
    private void Start()
    {
        cam = Camera.main.transform;
    }

    void Update()
    {
        switch (axis)
        {
            case LookAxis.forward:
                transform.LookAt(cam, Vector3.forward);
                break;
            case LookAxis.backward:
                transform.LookAt(cam, Vector3.back);
                break;
            case LookAxis.rigth:
                transform.LookAt(cam, Vector3.right);
                break;
            case LookAxis.left:
                transform.LookAt(cam, Vector3.left);
                break;
            case LookAxis.up:
                transform.LookAt(cam, Vector3.up);
                break;
            case LookAxis.down:
                transform.LookAt(cam, Vector3.down);
                break;
        }
    }
}
