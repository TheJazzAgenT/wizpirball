using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterHit : MonoBehaviour {

    private float timer = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {

        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Bullet")
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                counterFire();
            }
        }
    }

    void counterFire()
    {
        Debug.Log("HIT ME BABY ONE MORE TIME");
    }
}
