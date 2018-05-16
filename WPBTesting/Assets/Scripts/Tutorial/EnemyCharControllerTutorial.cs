using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCharControllerTutorial : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float BulletSpeed = 1.0f;
    public Transform farEnd;
    public Transform nearEnd;
    public bool stunned = false;
    public Transform p1;
    public Transform p2;

    private float secondsForOneLength = 5f;
    private float batAngle = 10.0f;
    float timer = 0.0f;
    float shootDelay = 2.0f;

    void Start()
    {
        transform.position = nearEnd.position;
    }

    void Update()
    {
        // Fire every 2 seconds.
        timer += Time.deltaTime;
        if (timer >= shootDelay)
        {
            if (!stunned)
            {
                Fire();
                timer = 0;
            }

        }
        //BulletSpeed = Vector3.Distance(transform.position, playerShip.transform.position)/150f;
        batAngle = Vector3.Distance(transform.position, p1.transform.position) / 7.0f;
        // AI Paces between nearEnd and farEnd
        transform.position = Vector3.Lerp(nearEnd.position, farEnd.position, Mathf.SmoothStep(0f, 1f, Mathf.PingPong(Time.time / secondsForOneLength, 1f)));

    }

    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        Vector3 aimDirection = (Random.value < 0.5f ? p1 : p2).transform.position - transform.position;
        bullet.GetComponent<Rigidbody>().velocity = (new Vector3(aimDirection.x, aimDirection.y + Random.Range(batAngle, batAngle + 10.0f), aimDirection.z + Random.Range(-10.0f, 10.0f)) * BulletSpeed);
    }
}
