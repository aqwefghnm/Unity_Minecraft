using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_rotate : MonoBehaviour
{
    public float x;
    public float y;
    private Quaternion rotation;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        x += Input.GetAxis("Mouse X") * Time.deltaTime*400;
        y -= Input.GetAxis("Mouse Y") * Time.deltaTime*400;
        rotation = Quaternion.Euler(y, x, 0);
        transform.rotation = rotation;

    }
}
