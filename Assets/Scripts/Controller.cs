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
    private int indexSlots;
    private GameObject[] slot;

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

        InventorySwitch = false;
        inventory.SetActive(false);
        allSlots = 10;
        indexSlots = 0;
        //slot = new GameObject[allSlots];
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

        if (Input.GetKeyDown(KeyCode.E))
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
                if (rch.collider.name == "dirt")
                {
                    if (indexSlots >= allSlots)
                        return;
                    Destroy(rch.collider.gameObject);
                    inventory.transform.GetChild(indexSlots).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/coarse_dirt");
                    ++indexSlots;
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
                    Debug.Log(rch.point);
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
