using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrigger : MonoBehaviour
{
    public int damage = 20;
    public GameObject impact;
    private GameObject enemyShip;

    // Use this for initialization
    void Start()
    {
        enemyShip = GameObject.FindGameObjectWithTag("PlayerShip");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        //all projectile colliding game objects should be tagged "Enemy" or whatever in inspector but that tag must be reflected in the below if conditional
        if (col.gameObject.tag == "Enemy")
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
            //destroy the projectile that just caused the trigger collision
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "PlayerShip")
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
