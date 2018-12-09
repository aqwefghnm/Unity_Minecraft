using System.Collections;
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
    public float speed;
    public float JumpForce;
    bool IsJump = true;
    bool Automove;
    Vector3 move_direction;
    Vector3 click_pos;
    Animator animator;
    public float x;
    public float y;
    private Quaternion rotation;

    public bool InventorySwitch;
    public GameObject inventory;
    private int allSlots;
    private GameObject[] slot;
    private int[] obj;
    private int choose;
    public Transform[] material;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Use this for initialization
    void Start () {
        speed = 5;
        JumpForce = 200;
        IsJump = false;
        Vector3 force_direction = Vector3.up;
        Automove = false;
        x = 0;
        y = 0;

        InventorySwitch = true;
        inventory.SetActive(true);
        allSlots = 10;
        obj = new int[allSlots];
        choose = 7;
        //for (int i = 0; i < allSlots; ++i)
        //{
        //    slot[i] = inventory.transform.GetChild(i).gameObject;
        //}
    }
	
	// Update is called once per frame
	void Update () {
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

        if (Input.GetKeyUp(KeyCode.E))
        {
            if (InventorySwitch == false)
            {
                InventorySwitch = true;
                inventory.SetActive(true);
            }
            else
            {
                InventorySwitch = false;
                inventory.SetActive(false);
            }

        }
            if (Input.GetKey(KeyCode.Escape)){
                SceneManager.LoadScene(0);
            }
        if(Automove)
        {
            if(Vector3.Distance(transform.position, click_pos) <= 1)
            {    
                Automove = false;
                return;
            }
            animator.SetFloat("speed", speed);
            transform.position += Time.deltaTime * move_direction.normalized * speed;
        }
        if (Input.GetMouseButton(0)){
            Automove = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rch;
            if (Physics.Raycast(ray, out rch))
            {
                int index;
                if(choose == 7)
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

                }
                else
                {
                    Transform powerup = Instantiate(material[choose]);
                    powerup.position = rch.point + new Vector3(0, 2, 0);
                    --obj[choose];
                    inventory.transform.GetChild(choose).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = obj[choose].ToString();
                    inventory.transform.GetChild(choose).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/white");
                    choose = 7;
                    inventory.transform.GetChild(choose).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/red");
                }
            }
        }
        if (Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rch;
            if (Physics.Raycast(ray, out rch))
            {
                if (rch.collider.name == "floor" && !Automove)
                {
                    //Debug.Log(rch.point);
                    Automove = true;
                    move_direction = rch.point - transform.position;
                    Debug.Log(move_direction);
                    click_pos = rch.point;
                }     
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Time.deltaTime * speed * transform.forward;
            Automove = false;
            animator.SetFloat("speed", speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Time.deltaTime * speed * transform.forward * (-1);
            Automove = false;
            animator.SetFloat("speed", speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Quaternion.Euler(0, -90, 0) * transform.forward * Time.deltaTime * speed ;
            Automove = false;
            animator.SetFloat("speed", speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Quaternion.Euler(0, 90, 0) * transform.forward * Time.deltaTime * speed;
            Automove = false;
            animator.SetFloat("speed", speed);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            Automove = false;
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
        if (Input.GetKey(KeyCode.Alpha8))
        {
            inventory.transform.GetChild(choose).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/white");
            choose = 7;
            inventory.transform.GetChild(choose).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/red");
        }

    }
    void OnCollisionEnter(Collision c)
    {
        Debug.Log(c.transform.name);
        IsJump = false;
        //IsDoubleJump = false;

        if (c.transform.name == "g1" || c.transform.name == "g2" || c.transform.name == "g3")
        {
            Destroy(c.gameObject);
            speed += 2.0f;
            JumpForce += 50.0f;
            Debug.Log(speed);
        }
    }
}
