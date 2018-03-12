using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCharController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float BulletSpeed = 1.0f;
    public Transform farEnd;
    public Transform nearEnd;
    public GameObject playerShip;
    [SerializeField]
    private GameObject myShip;
    private float secondsForOneLength = 5f;
    float timer = 0.0f;
    float shootDelay = 2.0f;
    public bool stunned = false;

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
        BulletSpeed = Vector3.Distance(transform.position, playerShip.transform.position)/100f;
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
        Vector3 boatVelocity = myShip.GetComponent<ShipFixedPathing>().getShipVelocity();
        Vector3 aimDirection = playerShip.transform.position - transform.position;
        //bullet.GetComponent<Rigidbody>().velocity = (new Vector3(aimDirection.x, aimDirection.y + Random.Range(10.0f, 20.0f), aimDirection.z + Random.Range(1.0f, 10.0f)) * BulletSpeed) + boatVelocity;
        bullet.GetComponent<Rigidbody>().velocity = (new Vector3(aimDirection.x, aimDirection.y + 15, aimDirection.z * BulletSpeed)) + boatVelocity;
        //bullet.GetComponent<Rigidbody>().velocity = playerShip.transform.position * BulletSpeed;

        // not destroying bullet yet, letting it go free
        // Destroy the bullet after 2 seconds
        // Destroy(bullet, 2.0f);
    }
}
