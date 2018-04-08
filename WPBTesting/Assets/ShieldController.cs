using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour {
    public int Blocks;//# of balls shield can block

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Blocks == 0)
        {
            gameObject.SetActive(false);
            Debug.Log("shield ticking down");
        }
    }
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Checking trigger");
        if (col.gameObject.tag == "Bullet")//no way to tell if enemy or ally bullet
        {
            //col.GetComponent<Rigidbody>().velocity *= -1;
            Destroy(col.gameObject);
            Debug.Log("shield deflecting");
            Blocks--;
        }
    }
}
