using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class FloatObjectScript : MonoBehaviour {
    public float waterLevel = 0.0f;
    public float floatThreshold = 2.0f;
    public float waterDensity = 0.125f;
    public float downForce = 4.0f;

    private Rigidbody rb;
    float forceFactor;
    Vector3 floatForce;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Debug.Log(rb);
    }

    // Update is called once per frame
    void FixedUpdate () {
        forceFactor = 1.0f - ((transform.position.y - waterLevel) / floatThreshold);

        if (forceFactor > 0.0f)
        {
            floatForce = -Physics.gravity * rb.mass * (forceFactor - rb.velocity.y * waterDensity);
            floatForce += new Vector3(0.0f, -downForce * rb.mass, 0.0f);
            rb.AddForceAtPosition(floatForce, transform.position);
        }
	}
}
