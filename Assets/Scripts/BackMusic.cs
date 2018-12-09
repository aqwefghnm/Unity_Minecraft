using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackMusic : MonoBehaviour {


    // Use this for initialization
    void Start()
    {
        bgm1 = GameObject.FindGameObjectWithTag("MusicMorning").GetComponent<AudioSource>();
        bgm2 = GameObject.FindGameObjectWithTag("MusicNight").GetComponent<AudioSource>();
        //Debug.Log(bgm1);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Daylight.x);
        if (Daylight.x >= 0 && Daylight.x <= 180)
        {
            bgm1.UnPause();
            bgm2.Pause();
        }
        else
        {
            bgm1.Pause();
            bgm2.UnPause();
        }
        /*if (Daylight.x < 0 || Daylight.x > 200)
        {
            bgm1.Pause();
            bgm2.UnPause();
        }
        else
        {
            bgm1.UnPause();
            bgm2.Pause();
        }*/
    }

    private AudioSource bgm1, bgm2;
}
