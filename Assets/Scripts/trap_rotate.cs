using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

public class trap_rotate : MonoBehaviour {
    public Vector3 Rotate_direction;
    public int Rotate_speed;
    private Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        Rotate_direction = Vector3.up;
        Rotate_speed = 100;
        rb.AddTorque(Rotate_direction * Rotate_speed);

     }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter (Collider c)
    {
        Debug.Log(c.name);
        if(c.name == "player")
        {
            Rigidbody rb = c.GetComponent<Rigidbody>();
            //c.transform.position = new Vector3(c.transform.position.x, 10.0f, c.transform.position.z);
            rb.AddForce(400 * Vector3.up);
        }
       
    }
}
