using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Camera PlayerCamera;
    public float mouseSensitivity = 100.0f;
    public float clampAngle = 80.0f;
    public bool useX, useY;

    private float rotY = 0.0f; // rotation around the up/y axis
    private float rotX = 0.0f; // rotation around the right/x axis

    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        if (useY)
        {
            rotY = rot.y;
        }
        if (useX)
        {
            rotX = rot.x;
        }
    }

    void Update()
    {
        if (PlayerCamera.enabled)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = -Input.GetAxis("Mouse Y");

            if (useY)
            {
                rotY += mouseX * mouseSensitivity * Time.deltaTime;
            }
            if (useX)
            {
                rotX += mouseY * mouseSensitivity * Time.deltaTime;
                rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
            }

            Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
            transform.rotation = localRotation;
        }
    }
}