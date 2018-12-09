using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class material_generator : MonoBehaviour {
    public Transform obj;
	// Use this for initialization
	void Start () {
        Transform[] material = new Transform[10];
        for(int i=0; i<10; ++i)
        {
            material[i] =  Instantiate(obj);
            float x = Random.Range(-40, 40);
            float z = Random.Range(-40, 40);
            material[i].position = new Vector3(x, 10, z);
        }
            
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
