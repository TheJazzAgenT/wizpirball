using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampBallTrigger : MonoBehaviour {

    public int initialDamage = 20;
    public int healDamage = -15;
    public bool ignoreCaster = true;
    public float delayBeforeCasting = 0.0f;
    public float applyEveryNSeconds = 1.0f;
    public int applyDamageNTimes = 3;//means lasts 3 seconds
    public float oSpeed;// originally speed of the ship
    public GameObject impact;

    private GameObject enemyShip;
    private GameObject me;
    private int appliedTimes = 0;
    private IEnumerator coroutine;
    private bool test = false;
    private string self;
    private string other;
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
                curhealth.TakeDamage(initialDamage);
                me.GetComponent<ShipController>().TakeDamage(healDamage);
            }

            var explosion = (GameObject)Instantiate(impact, transform.position, transform.rotation);
            explosion.transform.SetParent(enemyShip.transform);
            explosion.GetComponent<ParticleSystem>().Play();
            explosion.GetComponent<AudioSource>().Play();

            StartCoroutine(DestoryAfterDelay(5.0f, gameObject)); // Destroy after 5 seconds
            StartCoroutine(DestoryAfterDelay(3.0f, explosion));
            //destroy the projectile that just caused the trigger collision
            //Destroy(gameObject);
        }
        else if (col.gameObject.tag == "Ship_" + self)
        {
            //Destroy(col.gameObject);
            //add an explosion or something
            ShipController curhealth = me.GetComponent<ShipController>();
            //if exists
            if (curhealth != null)
            {
                curhealth.TakeDamage(initialDamage);
                curhealth.TakeDamage(healDamage);
            }

            var explosion = (GameObject)Instantiate(impact, transform.position, transform.rotation);
            explosion.transform.SetParent(me.transform);
            explosion.GetComponent<ParticleSystem>().Play();
            explosion.GetComponent<AudioSource>().Play();

            //destroy the projectile that just caused the trigger collision
            StartCoroutine(DestoryAfterDelay(2.8f, explosion));
        }
        if (col.gameObject.tag == "WATER")
        {
            StartCoroutine(DestoryAfterDelay(5.0f, gameObject));
        }
        GetComponent<VampBallTrigger>().enabled = false;
    }
    private IEnumerator DestoryAfterDelay(float Delay, GameObject destroyable)
    {
        Debug.Log("entered Destory");
        bool alphaBool = true;
        while (alphaBool)
        {
            yield return new WaitForSeconds(Delay);
            Debug.Log("Waited for " + Delay + " seconds");
            Destroy(destroyable);
            alphaBool = false;
        }
    }

}
