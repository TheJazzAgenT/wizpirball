using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballistics : MonoBehaviour
{
    //Drags
    public Transform targetObj;
    public Transform gunObj;
    public bool useMouse;

    public static float bulletSpeed = 50.0f;

    //The step size
    static float h;
    private LineRenderer lineRenderer;
    private Vector3 shipVel;
    private Vector2 mousePos;
    private float aimSensitivity = 2.2f;
    private CharacterMovement controller;

    [SerializeField]
    private Camera cam;

    void Awake()
    {
        //Can use a less precise h to speed up calculations
        //Or a more precise to get a more accurate result
        //But lower is not always better because of rounding errors
        h = Time.fixedDeltaTime * 1f;
        //gunObj.rotation = transform.rotation;
        lineRenderer = gunObj.GetComponent<LineRenderer>();
        mousePos = Vector2.zero;
        controller = GetComponent<CharacterMovement>();
    }

    void Update()
    {
        if (useMouse)
        {
            //Debug.Log("??????????");
            mousePos += new Vector2(Input.GetAxis(controller.playerInput[4]) * aimSensitivity, Input.GetAxis(controller.playerInput[5]) * aimSensitivity);
            mousePos = useMouse ? (Vector2)Input.mousePosition : Clamp(ref mousePos);
            // this creates a horizontal plane passing through this object's center adjusted downwards so its on the waters surface
            Plane plane = new Plane(Vector3.up, transform.position - new Vector3(0.0f, 4.1f, 0.0f));
            // create a ray from the mousePosition
            Ray ray = cam.ScreenPointToRay(mousePos);
            // plane.Raycast returns the distance from the ray start to the hit point
            float range = 0.0f;
            if (plane.Raycast(ray, out range))
            {
                // some point of the plane was hit - get its coordinates
                Vector3 hitPoint = ray.GetPoint(range);
                // use the hitPoint to aim your cannon
                targetObj.position = hitPoint;
            }
        }
        else
        {
            //shipVel = myShip.GetComponent<ShipFixedPathing>().getShipVelocity();
            Vector3 direction = Vector3.Scale((targetObj.position - gunObj.position).normalized, new Vector3(1, 0, 1));
            //direction.y = targetObj.position.y;
            targetObj.position += direction * Input.GetAxis(controller.playerInput[5]) * aimSensitivity;
            targetObj.position += new Vector3(direction.z, direction.y, -direction.x) * Input.GetAxis(controller.playerInput[4]) * aimSensitivity;
            float distance = Vector3.Distance(targetObj.position, gunObj.position);
            while (distance >= 250)
            {
                targetObj.position -= direction * aimSensitivity;
                distance = Vector3.Distance(targetObj.position, gunObj.position);
            }
            while (distance <= 5)
            {
                targetObj.position += direction * aimSensitivity;
                distance = Vector3.Distance(targetObj.position, gunObj.position);
            }
            
            // Check if object is within camera counds
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);
            // Get a ray from targetObj to Left and Right planes of the camera view.
            Vector3 directionL = targetObj.position - planes[0].ClosestPointOnPlane(targetObj.position);
            Vector3 directionR = targetObj.position - planes[1].ClosestPointOnPlane(targetObj.position);
            // Check the angle between each ray and its plane. If the angle is less than 90 degrees that means its on the wrong side of the plane.
            if (Vector3.Angle(directionL, planes[0].normal) < 90)
            {
                //Debug.Log(Vector3.Angle(directionL, planes[0].normal));
                targetObj.position += new Vector3(direction.z, direction.y, -direction.x) * -1 *  aimSensitivity;
                //targetObj.position += directionL * aimSensitivity;
            }
            if (Vector3.Angle(directionR, planes[1].normal) < 90)
            {
                //Debug.Log(Vector3.Angle(directionR, planes[1].normal));
                targetObj.position += new Vector3(direction.z, direction.y, -direction.x) * aimSensitivity;
                //targetObj.position += directionR * aimSensitivity;
            }
        }
        RotateGun();
        DrawTrajectoryPath();
    }

    void RotateGun()
    {
        //Get the 2 angles
        float? highAngle = 0f;
        float? lowAngle = 0f;

        CalculateAngleToHitTarget(out highAngle, out lowAngle);
        /*if (lowAngle == Mathf.Infinity)
        {
            Debug.Log(Vector3.Distance(gunObj.position, targetObj.position));
        }*/

        //Artillery
        //float angle = (float)highAngle;
        //Regular gun
        float angle = (float)lowAngle;

        //If we are within range
        if (angle != Mathf.Infinity)
        {
            //Rotate the gun
            //The equation we use assumes that if we are rotating the gun up from the
            //pointing "forward" position, the angle increase from 0, but our gun's angles
            //decreases from 360 degress when we are rotating up
            gunObj.localEulerAngles = new Vector3(360f - angle, 0f, 0f);

            //Rotate the turret towards the target
            transform.LookAt(targetObj);
            transform.eulerAngles = new Vector3(0f, transform.rotation.eulerAngles.y, 0f);
        }
    }

    //Returns 0, 1, or 2 angles depending on if we are within range
    void CalculateAngleToHitTarget(out float? theta1, out float? theta2)
    {
        //Initial speed
        float v = bulletSpeed;
        Vector3 targetVec = targetObj.position - gunObj.position;
        //Vertical distance
        float y = targetVec.y;
        //Reset y so we can get the horizontal distance x
        targetVec.y = 0f;
        //Horizontal distance
        float x = targetVec.magnitude;
        //Gravity
        float g = 9.81f;
        //Calculate the angles
        float vSqr = v * v;
        float underTheRoot = (vSqr * vSqr) - g * (g * x * x + 2 * y * vSqr);
        //Check if we are within range
        if (underTheRoot >= 0f)
        {
            float rightSide = Mathf.Sqrt(underTheRoot);
            float top1 = vSqr + rightSide;
            float top2 = vSqr - rightSide;
            float bottom = g * x;

            theta1 = Mathf.Atan2(top1, bottom) * Mathf.Rad2Deg;
            theta2 = Mathf.Atan2(top2, bottom) * Mathf.Rad2Deg;
        }
        else
        {
            theta1 = Mathf.Infinity;
            theta2 = Mathf.Infinity;
        }
    }

    //Display the trajectory path with a line renderer
    void DrawTrajectoryPath()
    {
        //How long did it take to hit the target?
        float timeToHitTarget = CalculateTimeToHitTarget();
        //How many segments we will have
        int maxIndex = Mathf.RoundToInt(timeToHitTarget / h);
        lineRenderer.positionCount = maxIndex;
        //Start values
        Vector3 currentVelocity = gunObj.transform.forward * bulletSpeed;
        Vector3 currentPosition = gunObj.transform.position;

        Vector3 newPosition = Vector3.zero;
        Vector3 newVelocity = Vector3.zero;

        //Build the trajectory line
        for (int index = 0; index < maxIndex; index++)
        {
            lineRenderer.SetPosition(index, currentPosition);
            //Calculate the new position of the bullet
            BackwardEuler(h, currentPosition, currentVelocity, out newPosition, out newVelocity);

            currentPosition = newPosition;
            currentVelocity = newVelocity;
        }
    }

    //How long did it take to reach the target (splash in artillery terms)?
    public float CalculateTimeToHitTarget()
    {
        //Init values
        Vector3 currentVelocity = gunObj.transform.forward * bulletSpeed;
        Vector3 currentPosition = gunObj.transform.position;
        Vector3 newPosition = Vector3.zero;
        Vector3 newVelocity = Vector3.zero;

        //The total time it will take before we hit the target
        float time = 0f;
        //Limit to 30 seconds to avoid infinite loop if we never reach the target
        for (time = 0f; time < 30f; time += h)
        {
            BackwardEuler(h, currentPosition, currentVelocity, out newPosition, out newVelocity);
            //If we are moving downwards and are below the target, then we have hit
            if (newPosition.y < currentPosition.y && newPosition.y < targetObj.position.y)
            {
                //Add 2 times to make sure we end up below the target when we display the path
                time += h * 2f;
                break;
            }
            currentPosition = newPosition;
            currentVelocity = newVelocity;
        }
        return time;
    }

    public static void BackwardEuler(
                float h,
                Vector3 currentPosition,
                Vector3 currentVelocity,
                out Vector3 newPosition,
                out Vector3 newVelocity)
    {
        //Init acceleration
        //Gravity
        Vector3 acceleartionFactor = Physics.gravity;
        //Main algorithm
        newVelocity = currentVelocity + h * acceleartionFactor;
        newPosition = currentPosition + h * newVelocity;
    }

    public Vector2 Clamp(ref Vector2 value)
    {
        value.x = Mathf.Clamp(value.x, 0, Screen.width);
        value.y = Mathf.Clamp(value.y, 0, Screen.height);
        return value;
    }
}
