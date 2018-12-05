using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]

public class Controller : MonoBehaviour
{

    public float speed;
    public float JumpForce;
    bool IsJump = true;
    bool IsDoubleJump = true;
    bool Automove;
    Vector3 move_direction;
    Vector3 click_pos;
    Animator animator;
    public float x;
    public float y;
    private Quaternion rotation;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Use this for initialization
    void Start () {
        speed = 5;
        JumpForce = 300;
        IsJump = false;
        Vector3 force_direction = Vector3.up;
        Automove = false;
    }
	
	// Update is called once per frame
	void Update () {
        // init
        animator.SetFloat("speed", 0f);

        x += Input.GetAxis("Mouse X") * Time.deltaTime * 400;
        y -= Input.GetAxis("Mouse Y") * Time.deltaTime * 400;
        rotation = Quaternion.Euler(y, x, 0);
        transform.rotation = rotation;

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
                if (rch.collider.name == "Cube")
                    Destroy(rch.collider.gameObject);
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
            transform.position += Time.deltaTime * speed * Vector3.forward;
            Automove = false;
            animator.SetFloat("speed", speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Time.deltaTime * speed * Vector3.back;
            Automove = false;
            animator.SetFloat("speed", speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Time.deltaTime * speed * Vector3.left;
            Automove = false;
            animator.SetFloat("speed", speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Time.deltaTime * speed * Vector3.right;
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
            //else
            //{
            //    if (!IsDoubleJump)
            //    {
            //        rb.AddForce(JumpForce * Vector3.up);
            //        IsDoubleJump = true;
            //    }
            //}
            
            
            //transform.position += Time.deltaTime * JumpForce * Vector3.up;
            
            
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
            speed += 5.0f;
            JumpForce += 50.0f;
            Debug.Log(speed);
        }
    }
}
