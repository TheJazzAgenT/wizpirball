using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFixedPathing : MonoBehaviour {
    public Transform[] waypoints;
    public float rotationSpeed;
    public float secondsForOneLength = 10f;
    private int curWaypoint;
    private Transform from;
    private Transform to;
    private Transform future;
    private bool needsRotationStorage;
    private Quaternion rotationStore;
    private float timer = 0.0f;

	//health

	[SerializeField]
	private BarScript bar;

	static public int maxHealth = 100;
	[SerializeField]
	static public int curHealth = 100;

	//end of health

    // Use this for initialization
    void Start () {
		//health
		curHealth = maxHealth;
        //end of health
		curWaypoint = 0;
        from = waypoints[curWaypoint];
        to = waypoints[(curWaypoint + 1) % waypoints.Length];
        future = waypoints[(curWaypoint + 2) % waypoints.Length];
        transform.position = from.position;
        transform.LookAt(to);
        needsRotationStorage = true;
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i].LookAt(waypoints[(i + 1) % waypoints.Length]);
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (transform.position.x == to.position.x && transform.position.z == to.position.z)
        {
            Debug.Log("=========hit waypoint==========");
            curWaypoint = (curWaypoint + 1) % waypoints.Length;
            from = waypoints[curWaypoint];
            to = waypoints[(curWaypoint + 1) % waypoints.Length];
            future = waypoints[(curWaypoint + 2) % waypoints.Length];
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
	//health and damage
	public void TakeDamage(int amount)
	{
		curHealth -= amount;
		bar.fillAmount = curHealth;
	}

    public Vector3 getShipVelocity()
    {
        float dist = Vector3.Distance(to.position, from.position);
        float boatSpeed = dist / secondsForOneLength;
        Vector3 boatVel = (to.position - from.position).normalized * boatSpeed;
        return boatVel;
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
