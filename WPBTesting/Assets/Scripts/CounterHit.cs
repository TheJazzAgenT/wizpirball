using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterHit : MonoBehaviour {
    [SerializeField]
    private GameObject myCharacter;
    private float timer = 0.0f;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /*void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
           
        }
    }*/

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Bullet")
        {
            Debug.Log("baseball entered");
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Debug.Log("MMM BABY");
                myCharacter.GetComponent<CharacterMovement>().Fire();
            }
        }
    }
}
