using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daylight : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

        x = transform.rotation.x;
        y = transform.rotation.y;
        z = transform.rotation.z;

        transform.Rotate(speed, 0, 0);


    }

    static public float x, y, z;
    private bool open = false;
    private float _time = 5;
    public float speed = 1f;
}
