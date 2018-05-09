using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletTrigger : MonoBehaviour {
    public int damage = 20;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ship_P1")
        {
            ShipController playerDamaged = col.GetComponent<ShipController>();
            //if exists
            if (playerDamaged != null)
            {
                playerDamaged.TakeDamage(damage);
            }
            //destroy the projectile that just caused the trigger collision
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "WATER")
        {
            Destroy(gameObject);
        }
    }
}
