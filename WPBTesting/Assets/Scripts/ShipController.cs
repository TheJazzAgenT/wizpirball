using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FloatObjectScript))]
public class ShipController : MonoBehaviour
{
    public Camera ShipCamera;
    public float speed = 0.02f;
    public float steerSpeed = 0.7f;
    public float movementThreshold = 10.0f;
    public float maxSpeed = 5;
    public float maxTurnSpeed = 5;
    public float steerThreshold = 5.0f;
    static public int maxHealth = 100;
    static public int curHealth = 100;
    public bool alive;
    public float damage = 20;//not sure if this should be here or in player controller, just leaving this here though
    public Vector3 COM;
    public AudioClip crashSound;

    private Rigidbody rb;
    private Transform m_COM;
    float verticalInput;
    float movementFactor;
    float horizontalInput;
    float steerFactor;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log(rb);
        Cursor.visible = false;
        curHealth = maxHealth;
        alive = true;
    }
    void Update()
    {
        //Balance();
        if (ShipCamera.enabled)
        {
            verticalInput = Input.GetAxis("Vertical");
            horizontalInput = Input.GetAxis("Horizontal");
        }
        Steer();
        Movement();
        if (curHealth < 1)
        {
            //Destroy(gameObject);//breaks stuff if kept in
            alive = false;
        }
    }

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
        movementFactor = Mathf.Clamp(Mathf.Lerp(movementFactor, verticalInput, Time.deltaTime / movementThreshold), 0, maxSpeed);
        transform.Translate(0.0f, 0.0f, movementFactor * speed);
    }
    void Steer()
    {
        steerFactor = Mathf.Clamp(Mathf.Lerp(steerFactor, horizontalInput, Time.deltaTime / steerThreshold), -maxTurnSpeed, maxTurnSpeed);
        transform.Rotate(0.0f, steerFactor * steerSpeed, 0.0f);
    }
    public void TakeDamage(int amount)
    {
        curHealth -= amount;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "WATER")
        {
            audioSource.PlayOneShot(crashSound);
        }
    }
}