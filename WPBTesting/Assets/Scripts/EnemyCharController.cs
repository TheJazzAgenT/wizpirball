using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCharController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float BulletSpeed = 1.0f;
    public GameObject spawnPoint;
    public Transform farEnd;
    public Transform nearEnd;
    public GameObject playerShip;
    private float secondsForOneLength = 10f;
    bool canRespawn = true;
    float timer = 0.0f;
    float shootDelay = 1.0f;

    void Start()
    {
    }

    void Update()
    {
        // Fire every 2 seconds.
        timer += Time.deltaTime;
        if (timer >= shootDelay)
        {
            Fire();
            timer = 0;
        }
        // AI Paces between nearEnd and farEnd
        transform.position = Vector3.Lerp(nearEnd.position, farEnd.position,
          Mathf.SmoothStep(0f, 1f,
            Mathf.PingPong(Time.time / secondsForOneLength, 1f)
         ));
        
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "WATER" && canRespawn)
        {
            canRespawn = false;
            Invoke("respawnPlayer", 5);
        }
    }
    void respawnPlayer()
    {
        this.transform.position = spawnPoint.transform.position;
        this.transform.rotation = spawnPoint.transform.rotation;
        canRespawn = true;
    }

    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        Vector3 aimDirection = -(transform.position - playerShip.transform.position);
        bullet.GetComponent<Rigidbody>().velocity = new Vector3(aimDirection.x, aimDirection.y + Random.Range(2.0f, 10.0f), aimDirection.z + Random.Range(1.0f, 10.0f)) * BulletSpeed;
        //bullet.GetComponent<Rigidbody>().velocity = playerShip.transform.position * BulletSpeed;

        // not destroying bullet yet, letting it go free
        // Destroy the bullet after 2 seconds
        // Destroy(bullet, 2.0f);
    }
}
