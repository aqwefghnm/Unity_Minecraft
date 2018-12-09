using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class powerup_generator : MonoBehaviour {
    public Transform obj;
	// Use this for initialization
	void Start () {
        Transform powerup = Instantiate(obj);
        float x = Random.Range(-3, 3);
        float z = Random.Range(-3, 3);
        powerup.parent = transform;
        powerup.localPosition = new Vector3(x, 10, z);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
