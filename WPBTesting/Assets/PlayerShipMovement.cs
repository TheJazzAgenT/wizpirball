using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(FloatObjectScript))]
public class ShipMovement : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Camera ShipCamera;
    public float speed = 0.02f;
    public float steerSpeed = 1.0f;
    public float movementThreshold = 10.0f;
    public Vector3 COM;

    private Rigidbody rb;
    private Transform m_COM;
    float verticalInput;
    float movementFactor;
    float horizontalInput;
    float steerFactor;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log(rb);
    }
    void Update()
    {
        Balance();
        if (ShipCamera.enabled)
        {
            Movement();
            Steer();
        }

        //transform.Rotate(0, x, 0);
        //transform.Translate(0, 0, z);
        //commenting out to try and use a different control method.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }
    /*void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");
        //if (transform.rotation.eulerAngles.y > 80 && transform.rotation.eulerAngles.y < 100)
        if (moveVertical > 0)
        {
            Vector3 boatRotation = transform.rotation.eulerAngles;
            //Debug.Log(boatRotation.y);
            rb.AddForce(-transform.right * speed, ForceMode.Force);
        }
        //transform.Rotate(0, moveHorizontal, 0);
        Debug.Log(transform.forward);
        rb.AddTorque(transform.up * turnSpeed * moveHorizontal);
    }*/

    void Balance()
    {
        if (!m_COM)
        {
            m_COM = new GameObject("COM").transform;
            m_COM.SetParent(transform);
        }
        m_COM.position = COM + transform.position;
        rb.centerOfMass = m_COM.position;
    }

    void Movement()
    {
        verticalInput = Input.GetAxis("Vertical");
        movementFactor = Mathf.Lerp(movementFactor, verticalInput, Time.deltaTime / movementThreshold);
        transform.Translate(0.0f, 0.0f, movementFactor * speed);
    }
    void Steer()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        steerFactor = Mathf.Lerp(steerFactor, horizontalInput * verticalInput, Time.deltaTime / movementThreshold);
        transform.Rotate(0.0f, steerFactor * steerSpeed, 0.0f);
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