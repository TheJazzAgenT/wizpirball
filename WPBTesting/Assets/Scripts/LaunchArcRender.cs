using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class LaunchArcRender : MonoBehaviour {


    LineRenderer lr;
    public float velocity;
    public float angle;
    public int resolution = 10;
    float g;// force of gravity on y axis
    float radianAngle;


    private void Awake(){
        lr = GetComponent<LineRenderer>();
        g = Mathf.Abs(Physics2D.gravity.y);
    }

    private void OnValidate()
    {
        //check that lr is not null and game playing
        if(lr != null && Application.isPlaying)
        {
            RenderArc();
        }
    }
    // Use this for initialization
    void Start () {
        RenderArc();
	}

    void RenderArc()//population lin renderer with setting for the arc
    {
        lr.positionCount = resolution +1;
        lr.SetPositions(CalculateArcArray());
    }

    //creat an array of vertor 3 posion for arc
    Vector3[] CalculateArcArray()
    {
        Vector3[] arcArray = new Vector3[resolution + 1];

        radianAngle = Mathf.Deg2Rad * angle;
        float MaxDist = (velocity*velocity * Mathf.Sin(2* radianAngle))/g;

        for(int i = 0;i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;
            arcArray[i] = CalculateArcPoint(t, MaxDist);
        }

        return arcArray;
    }
    //calculate hieght and distance of each vertex
    Vector3 CalculateArcPoint(float t, float MaxDist)
    {
        float x = t * MaxDist;
        float y = x * Mathf.Tan(radianAngle) - ((g * x * x) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));
        return new Vector3(x, y);
    }

}
