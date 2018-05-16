using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterHitTutorial : MonoBehaviour
{
    public GameObject bat; // To be used for animation purposes.
    public Transform batSpawn; // Where to spawn the bat, probably Hand.R

    [SerializeField]
    private GameObject myCharacter; // Whichever character should be doing the counter hitting

    private float counterDelay = 1.2f;
    private float timer = 0.0f;
    private GameObject throwBat;
    private CharacterMovementTutorial controller;

    // Use this for initialization
    void Start()
    {
        controller = GetComponentInParent<CharacterMovementTutorial>();
        transform.position = myCharacter.transform.position;
        transform.rotation = myCharacter.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
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
                //throwBat = (GameObject)Instantiate(bat, batSpawn.position, batSpawn.rotation);
                myCharacter.GetComponent<CharacterMovementTutorial>().Fire();
                myCharacter.GetComponent<CharacterMovementTutorial>().counterHits += 1;
                Destroy(other.gameObject);
                timer = 0;
            }
        }
    }
}
