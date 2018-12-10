using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
    //血量設定
    public const int maxHealth = 304;
    public int currentHealth = maxHealth;
    //血量條
    public RectTransform Health_Bar, Hurt;

    private int temp;
    private AudioSource bgm;
    void Start()
    {
        temp = maxHealth;
        bgm = GameObject.Find("DeductHP").GetComponent<AudioSource>();
        bgm.Stop();
        //bgm.Play();
    }

    void Update()
    {
        //Debug.Log(currentHealth);
        //呈現傷害量
        if (Hurt.sizeDelta.x > Health_Bar.sizeDelta.x)
        {
            //讓傷害量(紅色血條)逐漸追上當前血量
            Hurt.sizeDelta += new Vector2(-1, 0) * Time.deltaTime * 60;
        }
        if (currentHealth <= 0)
        {
            return;
        }
        //按下H鈕扣血
        if (Input.GetKeyDown(KeyCode.H))
        {
            //接受傷害
            currentHealth = currentHealth - 30;
        }

        if(temp > currentHealth)
        {
            //signal
            bgm.Play();
            temp = currentHealth;
        }
        //bgm.Pause();
        
        //將綠色血條同步到當前血量長度
        Health_Bar.sizeDelta = new Vector2(currentHealth, Health_Bar.sizeDelta.y);
    }
}
