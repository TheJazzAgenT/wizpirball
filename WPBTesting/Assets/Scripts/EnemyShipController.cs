using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FloatObjectScript))]
public class EnemyShipController : MonoBehaviour {

    public float speed = 0.5f;
    public float steerSpeed = 1.0f;
    public float movementThreshold = 10.0f;
    public float maxSpeed = 1;
    public float maxTurnSpeed = 1;
    public float steerThreshold = 10.0f;
    static public int maxhealth = 100;
    static public int curhealth = 100;
    public bool alive;
    public float damage = 20;//not sure if this should be here or in player controller, just leaving this here though
    public Vector3 COM;
    public GameObject PlayerShip;

    private Rigidbody rb;
    private Transform m_COM;
    float verticalInput;
    float movementFactor;
    float horizontalInput;
    float steerFactor;
	[SerializeField]
	private BarScriptEnemy BarE;
     
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log(rb);
        curhealth = maxhealth;
        alive = true;
    }
    void Update()
    {
        //Balance();
        float angle1 = Vector3.Angle(transform.position - PlayerShip.transform.position, transform.right);
        float angle2 = Vector3.Angle(transform.position - PlayerShip.transform.position, -transform.right);
        if (angle1 > angle2)
        {
            horizontalInput = 1;
        }
        else
        {
            horizontalInput = -1;
        }
        verticalInput = 1;
        if(Time.timeScale == 1)
        {
            SteerAndMove();
        }
        
        if (curhealth < 1)
        {
            //Destroy(gameObject);
            alive = false;
        }
        //Debug.Log("ship health is " + curhealth);
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

    void SteerAndMove()
    {
        //verticalInput = 1;
        movementFactor = Mathf.Clamp(Mathf.Lerp(movementFactor, verticalInput, Time.deltaTime / movementThreshold), 0, maxSpeed);
        transform.Translate(0.0f, 0.0f, movementFactor * speed);

        //horizontalInput = 1;
        steerFactor = Mathf.Clamp(Mathf.Lerp(steerFactor, horizontalInput, Time.deltaTime / movementThreshold), -maxTurnSpeed, maxTurnSpeed);
        transform.Rotate(0.0f, steerFactor * steerSpeed, 0.0f);
    }

    public void TakeDamage( int amount)
    {
        curhealth -= amount;
		BarE.fillAmountE = curhealth;
    }
}
