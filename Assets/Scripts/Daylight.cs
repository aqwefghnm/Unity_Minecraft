using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daylight : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //x = transform.rotation.x;
        wolf = GameObject.Find("Wolf").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.Log(wolf);
        //x = transform.rotation.x;
        x = transform.eulerAngles.x;
        //y = transform.rotation.y;
        //z = transform.rotation.z;

        transform.Rotate(speed, 0, 0);
        //transform.eulerAngles = new Vector3(speed,0,0);
        if (x >= 0 && x <= 180)
        {
            wolf.SetActive(false);
        }
        else
        {
            wolf.SetActive(true);
        }

    }

    //static public float x, y, z;
    private GameObject wolf;
    static public float x;
    public float speed = 1f;
}
