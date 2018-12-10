using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]

public class Monster_controller : MonoBehaviour {
    //private Vector3 nowPos;
    private Vector3 disPos;
    public float speed;
    // Use this for initialization
    void Start () {
        speed = 5f;
    }
	
	// Update is called once per frame
	void Update () {
        
        disPos = Controller.nowPos;
        transform.LookAt(disPos);
        move();

    }
    void move()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }
}
