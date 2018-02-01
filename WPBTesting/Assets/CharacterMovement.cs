using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    public Camera PlayerCamera;
    public float speed = 0.02f;
    public GameObject spawnPoint;

    float verticalInput;
    float horizontalInput;
    bool canRespawn = true;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void FixedUpdate () {
        if (PlayerCamera.enabled)
        {
            verticalInput = Input.GetAxis("Vertical");
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(horizontalInput * speed, 0.0f, verticalInput * speed);
            if (verticalInput != 0 || horizontalInput !=0)
            {
                anim.SetTrigger("isRunning");
            }
            else
            {
                anim.SetTrigger("StoppedRunning");
            }
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "WATER" && canRespawn)
        {
            canRespawn = false;
            Invoke("respawnPlayer", 5);
        }
    }
    void respawnPlayer()
    {
        this.transform.position = spawnPoint.transform.position;
        canRespawn = true;
    }
}
