using UnityEngine;
using System.Collections;

[System.Serializable]
public class MinMax
{
    public float min;
    public float max;

    public MinMax(float min, float max)
    {
        this.min = Mathf.Min(min, max);
        this.max = Mathf.Max(min, max);
    }
    public float Clamp(float value) { return Mathf.Clamp(value, this.min, this.max); }
    public float Repeat(float value) { return this.min + Mathf.Repeat(value - this.min, this.max - this.min); }
}


[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{

    public Transform target;
    public Camera PlayerCamera;
    public Vector3 offSetLookAt = new Vector3(0f, 1f, 0f);
    public Vector3 offSetPosition = new Vector3(0f, 1f, -2.0f);
    public MinMax MinMaxRotationX = new MinMax(-45f, 3f); //Up-Down rotation
    public MinMax MinMaxRotationY = new MinMax(-90f, 90f); //Left-Right rotation
    public MinMax MinMaxCameraDistance = new MinMax(5f, 20f); //Zoom distance

    public float angleX = -10f; //Up-Down angle
    public float angleY = 0f; //Left-Right angle
    public float cameraDistance = 5f;

    Vector3 normal; //OffSetPosition normalized

    void Start()
    {
        if (target == null)
        {
            Debug.Log("Add target to CameraController script");
            Destroy(this);
        }
        else
        {
            normal = offSetPosition.normalized;
        }
    }

    void Update()
    {
        if (PlayerCamera.enabled)
        {
            this.angleX = MinMaxRotationX.Clamp(this.angleX - Input.GetAxis("Mouse Y"));
            this.angleY = MinMaxRotationY.Clamp(this.angleY + Input.GetAxis("Mouse X"));
            this.cameraDistance = MinMaxCameraDistance.Clamp(this.cameraDistance + Input.GetAxis("Mouse ScrollWheel") * 10f);

            Quaternion upRotation = Quaternion.LookRotation(new Vector3(target.forward.x, 0f, target.forward.z)) * Quaternion.Euler(0f, this.angleY, 0f);
            transform.position = target.position + Vector3.Scale(upRotation * normal, new Vector3(1, this.angleX/25, 1)) * this.cameraDistance;
            transform.rotation = Quaternion.LookRotation((target.position + offSetLookAt) - transform.position) * Quaternion.Euler(this.angleX, 0f, 0f);
            Debug.Log(transform.position);
        }
    }
}