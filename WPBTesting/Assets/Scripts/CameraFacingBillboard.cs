using UnityEngine;
using System.Collections;

public class CameraFacingBillboard : MonoBehaviour
{
    public Camera Cam;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Cam.transform.position, Vector3.up);
    }
}