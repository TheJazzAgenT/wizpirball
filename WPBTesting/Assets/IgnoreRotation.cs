using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreRotation : MonoBehaviour {

    public GameObject ship; //assign the camera in inspector or in Start()
    private Vector3 prevPos;

    void Start()
    {
        prevPos = ship.transform.position;
    }

    void Update()
    {
        Vector3 displacement = ship.transform.position - prevPos;

        prevPos = ship.transform.position;

        this.transform.position += displacement;
    }
}
