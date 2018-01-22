using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float speed;
    private Rigidbody rb;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log(rb);
    }
    void Update()
    {
        //var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        //var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        //transform.Rotate(0, x, 0);
        //transform.Translate(0, 0, z);
        //commenting out to try and use a different control method.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }
    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");
        //if (transform.rotation.eulerAngles.y > 80 && transform.rotation.eulerAngles.y < 100)
        if (moveVertical > 0)
        {
            //Vector3 movement = new Vector3(0.0f, 0.0f, moveVertical);
            Vector3 boatRotation = transform.rotation.eulerAngles;
            Debug.Log(boatRotation.y);
            //Vector3 movementLock = new Vector3(1, 0.0f, 0.0f);
            rb.AddForce(-transform.right * speed, ForceMode.Force);
            //rb.AddForce(moveVertical * speed,0,0);
        }
        transform.Rotate(0, moveHorizontal, 0);
        //using this for movement now, think it might be better in the long term, might.
    }
    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

        // not destroying bullet yet, letting it go free
        // Destroy the bullet after 2 seconds
        // Destroy(bullet, 2.0f);
    }
}