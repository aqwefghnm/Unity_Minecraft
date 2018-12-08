using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorningMusic : MonoBehaviour {

	// Use this for initialization
	void Start () {
        bgm1 = GameObject.FindGameObjectWithTag("MusicMorning").GetComponent<AudioSource>();
        bgm2 = GameObject.FindGameObjectWithTag("MusicNight").GetComponent<AudioSource>();
        //Debug.Log(bgm1);
	}
	
	// Update is called once per frame
	void Update () {
        /*if (Daylight.x >= 0 && Daylight.x <= 200)
        {
            bgm.UnPause();
        }
        else
        {
            bgm.Pause();          
        }*/
        if (Daylight.x < 0 || Daylight.x > 200)
        {
            bgm1.Pause();
            bgm2.UnPause();
        }
        else
        {
            bgm1.UnPause();
            bgm2.Pause();
        }
    }

    private AudioSource bgm1,bgm2;
}
