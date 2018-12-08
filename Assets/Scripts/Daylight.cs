using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daylight : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //x = transform.rotation.x;
	}

    // Update is called once per frame
    void Update()
    {

        //x = transform.rotation.x;
        x = transform.eulerAngles.x;
        //y = transform.rotation.y;
        //z = transform.rotation.z;

        transform.Rotate(speed, 0, 0);
        //transform.eulerAngles = new Vector3(speed,0,0);


    }

    //static public float x, y, z;
    static public float x;
    public float speed = 1f;
}
