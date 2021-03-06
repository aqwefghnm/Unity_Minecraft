﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]

public class Controller : MonoBehaviour
{
    static public Vector3 nowPos;
    public float speed;
    public float JumpForce;
    bool IsJump = true;
    Animator animator;
    public float x;
    public float y;
    private Quaternion rotation;
    Rigidbody rb;
    public bool BagSwitch;
    public GameObject inventory;
    public GameObject Bag;
    private int allSlots;
    private GameObject[] slot;
    static public int[] obj;
    private int choose;
    public Transform[] material;
    public float power;
    private bool gold_finger1, gold_finger2, gold_finger3;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Use this for initialization
    void Start () {
        speed = 4;
        JumpForce = 300;
        IsJump = false;
        Vector3 force_direction = Vector3.up;
        x = 0;
        y = 0;
        rb = GetComponent<Rigidbody>();
        BagSwitch = false;
        Bag.SetActive(false);
        inventory.SetActive(true);
        allSlots = 10;
        obj = new int[allSlots];
        choose = 8;
        power = 200.0f;
        gold_finger1 = false;
        gold_finger2 = false;
        gold_finger3 = false;
    }
	
	// Update is called once per frame
	void Update () {
        nowPos = transform.position;
        if(nowPos.y <= -20)
            SceneManager.LoadScene(3);
        // init
        animator.SetFloat("speed", 0f);

        x += Input.GetAxis("Mouse X") * Time.deltaTime * 150;
        y -= Input.GetAxis("Mouse Y") * Time.deltaTime * 150;
        if (y <= -120f)
            y = 120f;
        else if (y >= 20f)
            y = 20f;
        rotation = Quaternion.Euler(y, x, 0);
        transform.rotation = rotation;

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            if (BagSwitch == false)
            {
                BagSwitch = true;
                Bag.SetActive(true);
            }
            else
            {
                BagSwitch = false;
                Bag.SetActive(false);
            }

        }
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetMouseButton(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rch;
            if (Physics.Raycast(ray, out rch))
            {
                int index;
                if(choose == 8)
                {
                    if (rch.collider.name == "dirt(Clone)")
                        index = 0;
                    else if (rch.collider.name == "brick(Clone)")
                        index = 1;
                    else if (rch.collider.name == "glass(Clone)")
                        index = 2;
                    else if (rch.collider.name == "gold(Clone)")
                        index = 3;
                    else if (rch.collider.name == "log(Clone)")
                        index = 4;
                    else if (rch.collider.name == "grass(Clone)")
                        index = 5;
                    else if (rch.collider.name == "stone(Clone)")
                        index = 6;
                    else
                        return;
                    ++obj[index];
                    Destroy(rch.collider.gameObject);
                    inventory.transform.GetChild(index).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = obj[index].ToString();
                    Bag.transform.GetChild(index).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = obj[index].ToString();
                    return;
                }
                if(choose == 9)
                {
                    if(rch.collider.name == "Wolf")
                    {
                        Debug.Log("Attack");
                        rch.collider.gameObject.GetComponent<Rigidbody>().AddForce(power * transform.forward);
                    }
                }

                
            }
        }
        if (Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rch;
            if (Physics.Raycast(ray, out rch))
            {
                if(choose == 0 || choose == 1 || choose == 2 || choose == 3 || choose == 4 || choose == 5 || choose == 6 || choose == 7)
                {
                    Debug.Log(choose);
                    Transform powerup = Instantiate(material[choose]);
                    powerup.position = rch.point + new Vector3(0, 5, 0);
                    --obj[choose];
                    inventory.transform.GetChild(choose).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = obj[choose].ToString();
                    Bag.transform.GetChild(choose).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = obj[choose].ToString();
                    inventory.transform.GetChild(choose).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/white");
                    choose = 8;
                    inventory.transform.GetChild(choose).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/red");
                }
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Time.deltaTime * speed * transform.forward;
            animator.SetFloat("speed", speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Time.deltaTime * speed * transform.forward * (-1);
            animator.SetFloat("speed", speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Quaternion.Euler(0, -90, 0) * transform.forward * Time.deltaTime * speed ;
            animator.SetFloat("speed", speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Quaternion.Euler(0, 90, 0) * transform.forward * Time.deltaTime * speed;
            animator.SetFloat("speed", speed);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (!IsJump)
            {
                rb.AddForce(JumpForce * Vector3.up);
                IsJump = true;
            }
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            int num = int.Parse(inventory.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text);
            if (num <= 0)
                return;
            inventory.transform.GetChild(choose).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/white");
            choose = 0;
            inventory.transform.GetChild(choose).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/red");
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            int num = int.Parse(inventory.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text);
            if (num <= 0)
                return;
            inventory.transform.GetChild(choose).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/white");
            choose = 1;
            inventory.transform.GetChild(choose).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/red");
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            int num = int.Parse(inventory.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text);
            if (num <= 0)
                return;
            inventory.transform.GetChild(choose).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/white");
            choose = 2;
            inventory.transform.GetChild(choose).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/red");
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            int num = int.Parse(inventory.transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text);
            if (num <= 0)
                return;
            inventory.transform.GetChild(choose).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/white");
            choose = 3;
            inventory.transform.GetChild(choose).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/red");
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            int num = int.Parse(inventory.transform.GetChild(4).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text);
            if (num <= 0)
                return;
            inventory.transform.GetChild(choose).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/white");
            choose = 4;
            inventory.transform.GetChild(choose).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/red");
        }
        if (Input.GetKey(KeyCode.Alpha6))
        {
            int num = int.Parse(inventory.transform.GetChild(5).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text);
            if (num <= 0)
                return;
            inventory.transform.GetChild(choose).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/white");
            choose = 5;
            inventory.transform.GetChild(choose).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/red");
        }
        if (Input.GetKey(KeyCode.Alpha7))
        {
            int num = int.Parse(inventory.transform.GetChild(6).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text);
            if (num <= 0)
                return;
            inventory.transform.GetChild(choose).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/white");
            choose = 6;
            inventory.transform.GetChild(choose).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/red");
        }
        if (Input.GetKey(KeyCode.Q))
        {
            inventory.transform.GetChild(choose).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/white");
            choose = 8;
            inventory.transform.GetChild(choose).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/red");
        }
        if (Input.GetKey(KeyCode.E))
        {
            inventory.transform.GetChild(choose).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/white");
            choose = 9;
            inventory.transform.GetChild(choose).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/red");
        }
        if (Input.GetKey (KeyCode.Z))
        {
            int num = int.Parse(inventory.transform.GetChild(7).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text);
            if (num <= 0)
                return;
            inventory.transform.GetChild(choose).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/white");
            choose = 7;
            inventory.transform.GetChild(choose).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/red");
        }
        if (Input.GetKeyUp(KeyCode.T))
        {
            if (!gold_finger1)
            {
                speed += 20;
                gold_finger1 = true;
            }
            else
            {
                speed -= 20;
                gold_finger1 = false;
            }
                
        }
        if (Input.GetKeyUp(KeyCode.Y))
        {
            if (!gold_finger2)
            {
                JumpForce += 600;
                gold_finger2 = true;
            }
            else
            {
                JumpForce -= 600;
                gold_finger2 = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.U))
        {
            if (!gold_finger3)
            {
                power += 500;
                gold_finger3 = true;
            }
            else
            {
                power -= 500;
                gold_finger3 = false;
            }
            
        }
        if (Input.GetKeyUp(KeyCode.KeypadMinus) && Daylight.speed > 0.015f)
        {
            Daylight.speed -= 0.01f;
            Debug.Log(Daylight.speed);
        }
        if (Input.GetKeyUp(KeyCode.KeypadPlus))
        {
            Daylight.speed += 0.01f;
            Debug.Log(Daylight.speed);
        }
    }
    void OnCollisionEnter(Collision c)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Debug.Log(c.transform.name);
        IsJump = false;
        //IsDoubleJump = false;
        if(c.transform.name == "Wolf")
        {
            HealthBar.IsAttacked = true;
            rb.AddForce(500 * GameObject.Find("Wolf").gameObject.transform.forward);
            //transform.position += 5 * GameObject.Find("Wolf").gameObject.transform.forward;
        }
        if (c.transform.name == "g1" || c.transform.name == "g2" || c.transform.name == "g3" || c.transform.name == "g4" || c.transform.name == "g5" || c.transform.name == "g6")
        {
            Destroy(c.gameObject);
            speed += 0.6f;
            JumpForce += 50.0f;
            power += 30.0f;
            Debug.Log(speed);
        }
    }
}
