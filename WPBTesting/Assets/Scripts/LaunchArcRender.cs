using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class LaunchArcRender : MonoBehaviour
{


    LineRenderer lr;
    public Vector3 aimer;
    public float angle;
    public int resolution = 10;
    private float velocity = 0;
    float g;// force of gravity on y axis
    float radianAngle;
    private Vector3 velDir;
    Vector3[] points = new Vector3[2];


    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        g = Mathf.Abs(Physics2D.gravity.y);
        points[0] = GameObject.Find("Hand.R").transform.position;
        //points[1] = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void OnValidate()
    {
        //check that lr is not null and game playing
        if (lr != null && Application.isPlaying)
        {
           RenderArc();
        }
    }
    // Use this for initialization
    void Start()
    {
        RenderArc();
    }
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

        }
        points[0] = GameObject.Find("Hand.R").transform.position;
        //points[1] = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //velDir = VelocityFinder.BallisticVel(aimer, angle);
        lr.SetPositions(CalculateArcArray());
        /*Vector3[] curvePoints = Curver.MakeSmoothCurve(points, 3.0f);
        Debug.Log("CURVE=============");
        for (int i = 0; i < curvePoints.Length; i++)
        {
            Debug.Log(curvePoints[i]);
        }
        lr.positionCount = curvePoints.Length;
        lr.SetPositions(curvePoints);
        //Debug.Log(Curver.MakeSmoothCurve(points, 9f));*/
    }

    void RenderArc()//population lin renderer with setting for the arc
    {
        lr.positionCount = resolution + 1;
        lr.SetPositions(CalculateArcArray());
    }

    //creat an array of vertor 3 posion for arc
    Vector3[] CalculateArcArray()
    {
        Vector3[] arcArray = new Vector3[resolution + 1];
        arcArray[0] = GameObject.Find("Hand.R").transform.position;

        radianAngle = Mathf.Deg2Rad * angle;
        float MaxDist = (velocity * velocity * Mathf.Sin(2 * radianAngle)) / g;

        for (int i = 1; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;
            arcArray[i] = CalculateArcPoint(t, MaxDist, arcArray[i - 1].z);
            arcArray[i].x += arcArray[0].x;
        }

        return arcArray;
    }
    //calculate hieght and distance of each vertex
    Vector3 CalculateArcPoint(float t, float MaxDist, float z)
    {
        float x = t * MaxDist;
        float y = x * Mathf.Tan(radianAngle) - ((g * x * x) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));
        return new Vector3(x, y, z);
    }
}
public class VelocityFinder : MonoBehaviour
{
    public static Vector3 BallisticVel(Vector3 from, Vector3 to, float angle)
    {
        Vector3 dir = to - from;
        // get target direction 
        var h = dir.y;
        // get height difference 
        dir.y = 0;
        // retain only the horizontal direction 
        var dist = dir.magnitude;
        // get horizontal distance 
        var a = angle * Mathf.Deg2Rad;
        // convert angle to radians 
        dir.y = dist * Mathf.Tan(a);
        // set dir to the elevation angle 
        dist += h / Mathf.Tan(a);
        // correct for small height differences 
        // calculate the velocity magnitude 
        var vel = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        return vel * dir.normalized;
    }
}
