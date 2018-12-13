using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    //血量設定
    public const int maxHealth = 300;
    public int currentHealth = maxHealth;
    //血量條
    public RectTransform Health_Bar, Hurt;
    static public bool IsAttacked;

    private int temp;
    private AudioSource bgm;
    private float timeacuum;
    public float interval;
    private bool IsHurt;
    private bool Invincible;
    void Start()
    {
        interval = 1.0f;
        timeacuum = 0;
        temp = maxHealth;
        IsHurt = true;
        IsAttacked = false;
        bgm = GameObject.Find("DeductHP").GetComponent<AudioSource>();
        bgm.Stop();
        Invincible = false;
        //bgm.Play();
    }

    void Update()
    {
        if (Invincible == true)
            return;
        //Debug.Log(currentHealth);
        //呈現傷害量
        if (Hurt.sizeDelta.x > Health_Bar.sizeDelta.x)
        {
            //讓傷害量(紅色血條)逐漸追上當前血量
            Hurt.sizeDelta += new Vector2(-1, 0) * Time.deltaTime * 60;
        }

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(3);
            return;
        }

        timeacuum += Time.deltaTime;
        if(timeacuum >= interval)
        {
            IsHurt = true;
        }
        //按下H鈕扣血
        if (Input.GetKeyUp(KeyCode.G) && IsHurt)
        {
            //接受傷害
            currentHealth = currentHealth - 30;
            IsHurt = false;
            timeacuum = 0;
        }
        if (Input.GetKeyUp(KeyCode.H))
        {
            //接受傷害
            currentHealth = maxHealth;
            IsHurt = false;
            timeacuum = 0;
            Hurt.sizeDelta = new Vector2(maxHealth, Health_Bar.sizeDelta.y);
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            Invincible = true;
        }
        if (IsAttacked && IsHurt)
        {
            //接受傷害
            currentHealth = currentHealth - 30;
            IsHurt = false;
            IsAttacked = false;
            timeacuum = 0;
        }
        if (temp > currentHealth)
        {
            
            //signal
            bgm.Play();
            temp = currentHealth;
        }
        if(temp < currentHealth)
        {
            temp = currentHealth;
        }
        //bgm.Pause();
        
        //將綠色血條同步到當前血量長度
        Health_Bar.sizeDelta = new Vector2(currentHealth, Health_Bar.sizeDelta.y);
    }
}
