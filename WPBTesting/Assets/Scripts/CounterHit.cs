using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterHit : MonoBehaviour {
    //public GameObject bat; // To be used for animation purposes.
    //public Transform batSpawn; // Where to spawn the bat, probably Hand.R
    //public Transform enemyShip;
    public GameObject BatPrefab;
    public Transform target;

    [SerializeField]
    private GameObject myCharacter; // Whichever character should be doing the counter hitting

    private float counterDelay = 1.2f;
    private float timer = 0.0f;
    private GameObject throwBat;
    private CharacterMovement controller;

    // Use this for initialization
    void Start () {
        controller = GetComponentInParent<CharacterMovement>();
        transform.position = myCharacter.transform.position;
        transform.rotation = myCharacter.transform.rotation;
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
            if ((Input.GetKeyDown(KeyCode.Mouse1) || (Input.GetAxis(controller.playerInput[10]) > 0)) && timer > counterDelay)
            {
                //Debug.Log("CounterHit!-----");
                Vector3 vel = other.GetComponent<Rigidbody>().velocity;
                //other.GetComponent<Rigidbody>().velocity = (-other.GetComponent<Rigidbody>().velocity + 1.5f * (enemyShip.position - other.transform.position));
                other.GetComponent<Rigidbody>().velocity = 1 * vel.magnitude * ((target.position - other.transform.position).normalized + new Vector3(0, 0.3f, 0));
                var bat = (GameObject)Instantiate(BatPrefab, other.transform.position, other.transform.rotation);
                var batHolder = new GameObject();
                bat.transform.SetParent(batHolder.transform);
                batHolder.transform.position = other.transform.position;
                batHolder.transform.LookAt(other.transform);
                batHolder.transform.localScale = Vector3.Scale(batHolder.transform.localScale, new Vector3(0.1f, 0.1f, 0.1f));
                //bat.transform.position = new Vector3(100, 500, 800);
                bat.GetComponent<Animation>().Play();
                Destroy(batHolder.gameObject, bat.GetComponent<Animation>().clip.length);
                //throwBat = (GameObject)Instantiate(bat, batSpawn.position, batSpawn.rotation);
                //controller.Fire();
                controller.mana -= 10;
                //Destroy(other.gameObject);
                timer = 0;
            }
        }
    }
}
