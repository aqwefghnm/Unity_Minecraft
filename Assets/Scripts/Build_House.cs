using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Build_House : MonoBehaviour, IPointerUpHandler
{
    GameObject inventory, Bag;
    
    // Use this for initialization
    void Start () {
        inventory = GameObject.Find("Inventory");
        Bag = GameObject.Find("Bag");
        //Debug.Log(inventory.transform.GetChild(2));
        //Debug.Log(Controller.obj[0]);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnPointerUp(PointerEventData e)
    {
        for (int i = 0; i < 7; ++i)
            if (Controller.obj[i] < 2)
                return;
        for (int i = 0; i < 7; ++i)
        {
            Controller.obj[i] -= 2;
            inventory.transform.GetChild(i).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = Controller.obj[i].ToString();
            Bag.transform.GetChild(i).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = Controller.obj[i].ToString();
        }
        ++Controller.obj[7];
        inventory.transform.GetChild(7).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = Controller.obj[7].ToString();
        Bag.transform.GetChild(7).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = Controller.obj[7].ToString();
    }
}
