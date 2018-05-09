using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrigger : MonoBehaviour
{
    public int damage = 20; // Damage dealt by this ball
    public GameObject impact; // Impact effect

    private GameObject enemyShip;
    private List<GameObject> lightningBalls = new List<GameObject>();
    private Rigidbody rb;
    private float magnetism = 500.0f; // How much is this ball effected by magnetic balls
    private string self; // Which player fired me
    private string other;

    void Awake()
    {
        // Find all lightning balls currently in the scene, add only the ones fired by current player to the list
        var meBalls = GameObject.FindGameObjectsWithTag("LightningBall");
        foreach (GameObject lBall in meBalls)
        {
            if (lBall.GetComponent<LightningBallTrigger>().self != self)
            {
                lightningBalls.Add(lBall);
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        self = GetComponent<PlayerSelector>().me;
        other = GetComponent<PlayerSelector>().notMe;
        enemyShip = GameObject.FindGameObjectWithTag("Ship_" + other);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // For each lightning ball stuck on enemy ship, find the magnetic force and add that to current ball.
        // Force is based on distance exponentially
        foreach (GameObject lightningBall in lightningBalls)
        {
            if (lightningBall)
            {
                Vector3 direction = (lightningBall.transform.position - transform.position).normalized;
                Vector3 force = direction * (magnetism / Mathf.Pow(Vector3.Distance(lightningBall.transform.position, transform.position), 2));
                rb.velocity += force;
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        //all projectile colliding game objects should be tagged "Enemy" or whatever in inspector but that tag must be reflected in the below if conditional
        if (col.gameObject.tag == "Ship_" + other)
        {
            //Destroy(col.gameObject);
            //add an explosion or something
            ShipController curhealth = enemyShip.GetComponent<ShipController>();
            //if exists
            if(curhealth != null)
            {
                curhealth.isHit = true;
                curhealth.TakeDamage(damage);
            }

            var explosion = (GameObject)Instantiate(impact,transform.position,transform.rotation);
            Vector3 boatVelocity = enemyShip.GetComponent<ShipFixedPathing>().getShipVelocity();
            explosion.GetComponent<Rigidbody>().velocity = boatVelocity;
            explosion.GetComponent<ParticleSystem>().Play();
            explosion.GetComponent<AudioSource>().Play();

            //destroy the projectile that just caused the trigger collision
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "WATER")
        {
            Destroy(gameObject);
        }
    }
}
