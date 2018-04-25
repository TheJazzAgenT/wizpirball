using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFixedPathing : MonoBehaviour {
    public List<Transform> waypoints;
    public float rotationSpeed;
    public float secondsForOneLength = 10f;

    private bool slowed = false;
    private int curWaypoint;
    private Transform from;
    private Transform to;
    //private Transform future;//console thinks this is not used, it is.
    private bool needsRotationStorage;
    private Quaternion rotationStore;
    private float timer = 0.0f;

    // Use this for initialization
    void Start () {
		curWaypoint = 0;
        from = waypoints[curWaypoint];
        to = waypoints[(curWaypoint + 1) % waypoints.Count];
        //future = waypoints[(curWaypoint + 2) % waypoints.Length];
        transform.position = from.position;
        transform.LookAt(to);
        needsRotationStorage = true;
        for (int i = 0; i < waypoints.Count; i++)
        {
            waypoints[i].LookAt(waypoints[(i + 1) % waypoints.Count]);
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (transform.position.x == to.position.x && transform.position.z == to.position.z)
        {
            if (slowed)
            {
                waypoints.RemoveAt(curWaypoint);
                curWaypoint -= 1;
                slowed = false;
            }
            //Debug.Log("=========hit waypoint==========");
            curWaypoint = (curWaypoint + 1) % waypoints.Count;
            from = waypoints[curWaypoint];
            to = waypoints[(curWaypoint + 1) % waypoints.Count];
            //future = waypoints[(curWaypoint + 2) % waypoints.Length];
            transform.LookAt(to);
            needsRotationStorage = true;
        }
        timer += Time.deltaTime;

        transform.position = Vector3.Lerp(from.position, to.position, timer / secondsForOneLength);

        if (timer > secondsForOneLength * rotationSpeed)
        {
            if (needsRotationStorage == true)
            {
                rotationStore = transform.rotation;
                needsRotationStorage = false;
            }
            transform.rotation = Quaternion.RotateTowards(transform.rotation, to.rotation, Quaternion.Angle(rotationStore, to.rotation)/((secondsForOneLength * (1 - rotationSpeed)) / Time.deltaTime));
        }

        if (timer >= secondsForOneLength)
        {
            timer = 0.0f;
        }
    }

    public Vector3 getShipVelocity()
    {
        float dist = Vector3.Distance(to.position, from.position);
        float boatSpeed = dist / secondsForOneLength;
        Vector3 boatVel = (to.position - from.position).normalized * boatSpeed;
        return boatVel;
    }

    public void Slow()
    {
        if (!slowed)
        {
            // create an empty game object to make a new transform
            GameObject slowWaypoint = new GameObject();
            slowWaypoint.transform.position = transform.position;
            waypoints.Insert(curWaypoint + 1, slowWaypoint.transform);
            curWaypoint = (curWaypoint + 1) % waypoints.Count;
            from = waypoints[curWaypoint];
            timer = 0;
            slowed = true;
        }
    }

	/*private void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.tag != "WATER")
		{
			audioSource.PlayOneShot(crashSound);
		}
	}*/
	//health and damage end
}
