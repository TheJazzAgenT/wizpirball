using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterHit : MonoBehaviour {
    public GameObject bat;
    public Transform batSpawn;
    [SerializeField]
    private GameObject myCharacter;
    private float counterDelay = 1.2f;
    private float timer = 0.0f;
    private GameObject throwBat;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
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
            //Debug.Log("baseball entered");
            if (Input.GetKeyDown(KeyCode.Mouse1) && timer > counterDelay)
            {
                //throwBat = (GameObject)Instantiate(bat, batSpawn.position, batSpawn.rotation);
                myCharacter.GetComponent<CharacterMovement>().Fire();
                Destroy(other.gameObject);
                timer = 0;
            }
        }
    }
}
