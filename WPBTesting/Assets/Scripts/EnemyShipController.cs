using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FloatObjectScript))]
public class EnemyShipController : MonoBehaviour {

    static public int maxhealth = 100;
    static public int curhealth = 100;
    public bool alive;
    //public Vector3 COM;
    //public GameObject PlayerShip;

    //private Rigidbody rb;
    //private Transform m_COM;
    //float verticalInput;
    //float movementFactor;
    //float horizontalInput;
    //float steerFactor;
	[SerializeField]
	private BarScriptEnemy BarE;
     
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //Debug.Log(rb);
        curhealth = maxhealth;
        alive = true;
    }
    void Update()
    {
        /*//Balance();
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
        */
        if (curhealth < 1)
        {
            //Destroy(gameObject);
            alive = false;
        }
        //Debug.Log("ship health is " + curhealth);
    }
    
    /*void Balance()
    {
        if (!m_COM)
        {
            m_COM = new GameObject("COM").transform;
            m_COM.SetParent(transform);
        }
        m_COM.position = COM + transform.position;
        rb.centerOfMass = m_COM.position;
    }*/

    public void TakeDamage( int amount)
    {
        curhealth -= amount;
		BarE.fillAmountE = curhealth;
    }
}
