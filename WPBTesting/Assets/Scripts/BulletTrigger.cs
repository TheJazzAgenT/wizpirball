using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrigger : MonoBehaviour
{
    public int damage = 20;
    public GameObject impact;

    private GameObject enemyShip;
    private GameObject[] lightningBalls;
    private Rigidbody rb;
    private float magnetism = 500.0f;
    private string self;
    private string other;

    void Awake()
    {
        lightningBalls = GameObject.FindGameObjectsWithTag("LightningBall");
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
        foreach (GameObject lightningBall in lightningBalls)
        {
            Vector3 direction = (lightningBall.transform.position - transform.position).normalized;
            Vector3 force = direction * (magnetism / Mathf.Pow(Vector3.Distance(lightningBall.transform.position, transform.position), 2));
            rb.velocity += force;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        //all projectile colliding game objects should be tagged "Enemy" or whatever in inspector but that tag must be reflected in the below if conditional
        if (col.gameObject.tag == "Ship_" + other)
        {
            //Destroy(col.gameObject);
            //add an explosion or something
            EnemyShipController curhealth = col.GetComponent<EnemyShipController>();
            //if exists
            if(curhealth != null)
            {
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
        if (col.gameObject.tag == "Ship_" + self)
        {

            //Destroy(col.gameObject);
            //add an explosion or something
            ShipController playerDamaged = col.GetComponent<ShipController>();
            //if exists
            if (playerDamaged != null)
            {
                playerDamaged.TakeDamage(damage);
            }

            //destroy the projectile that just caused the trigger collision
            //Destroy(gameObject);
        }
        if (col.gameObject.tag == "WATER")
        {
            Destroy(gameObject);
        }
    }
}
