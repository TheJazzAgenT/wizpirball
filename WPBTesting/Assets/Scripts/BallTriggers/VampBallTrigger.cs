using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampBallTrigger : MonoBehaviour {

    public int intialDamage = 20;
    public int healDamage = -15;
    public bool ignoreCaster = true;
    public float delayBeforeCasting = 0.0f;
    public float applyEveryNSeconds = 1.0f;
    public int applyDamageNTimes = 3;//means lasts 3 seconds
    public float oSpeed;// originally speed of the ship
    public GameObject impact;

    private GameObject enemyShip;
    private int appliedTimes = 0;
    private IEnumerator coroutine;
    private bool test = false;
    private string self;
    private string other;
    private GameObject me;
    private Collider mine;

    void Start()
    {
        self = GetComponent<PlayerSelector>().me;
        other = GetComponent<PlayerSelector>().notMe;
        me = GameObject.FindGameObjectWithTag("Ship_" + self);
        enemyShip = GameObject.FindGameObjectWithTag("Ship_" + other);
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        //all projectile colliding game objects should be tagged "Enemy" or whatever in inspector but that tag must be reflected in the below if conditional
        if (col.gameObject.tag == "Ship_" + other)
        {
            //add an explosion or something
            ShipController curhealth = enemyShip.GetComponent<ShipController>();
            //if exists
            if (curhealth != null)
            {
                curhealth.TakeDamage(intialDamage);
                me.GetComponent<ShipController>().TakeDamage(healDamage);
            }

            var explosion = (GameObject)Instantiate(impact, transform.position, transform.rotation);
            Vector3 boatVelocity = enemyShip.GetComponent<ShipFixedPathing>().getShipVelocity();
            explosion.GetComponent<Rigidbody>().velocity = boatVelocity;
            explosion.GetComponent<ParticleSystem>().Play();
            explosion.GetComponent<AudioSource>().Play();

            Destory(5.0f);//destroy after 5 seconds
            //destroy the projectile that just caused the trigger collision
            //Destroy(gameObject);
        }
        if (col.gameObject.tag == "WATER")
        {
            //Debug.Log("ice Collide water");
            coroutine = Destory(5.0f);
            StartCoroutine(coroutine);
        }
        GetComponent<VampBallTrigger>().enabled = false;
    }
    private IEnumerator Destory(float Delay)
    {
        bool alphaBool = true;
        while (alphaBool)
        {
            yield return new WaitForSeconds(Delay);
            Destroy(gameObject);
            alphaBool = false;
        }
    }

}
