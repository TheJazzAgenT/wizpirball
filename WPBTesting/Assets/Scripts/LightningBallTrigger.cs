using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBallTrigger : MonoBehaviour {
    public int intialDamage = 13;
    public float slow = 0.0f;
    public bool ignoreCaster = true;
    public float delayBeforeCasting = 0.0f;
    public float applyEveryNSeconds = 1.0f;
    public int applyDamageNTimes = 3;
    public float oSpeed;// originally speed of the ship
    public GameObject impact;
    private GameObject enemyShip;

    private bool delied = false;

    private int appliedTimes = 0;
    private IEnumerator coroutine;
    private bool test = false;
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
            Debug.Log("lightning Collide test");
            //Destroy(col.gameObject);
            //add an explosion or something
            EnemyShipController curhealth = col.GetComponent<EnemyShipController>();
            EnemyCharController target = col.GetComponentInChildren<EnemyCharController>();
            //if exists
            if (curhealth != null)
            {
                curhealth.TakeDamage(intialDamage);
                IEnumerator Coroutine = CastDamage(target);
                StartCoroutine(Coroutine);
            }

            var explosion = (GameObject)Instantiate(impact, transform.position, transform.rotation);
            Vector3 boatVelocity = enemyShip.GetComponent<ShipFixedPathing>().getShipVelocity();
            explosion.GetComponent<Rigidbody>().velocity = boatVelocity;
            explosion.GetComponent<ParticleSystem>().Play();

            Destory(5.0f);
            //destroy the projectile that just caused the trigger collision
            //Destroy(gameObject);
        }
        if (col.gameObject.tag == "WATER")
        {

            Debug.Log("Lightning Collide water");
            coroutine = Destory(5.0f);
            StartCoroutine(coroutine);
            //Destroy(gameObject);
        }
    GetComponent<LightningBallTrigger>().enabled = false;
    }

    IEnumerator CastDamage(EnemyCharController damageable)
    {
        while (true)
        {
            yield return new WaitForSeconds(applyEveryNSeconds);
            if (!test && appliedTimes <= applyDamageNTimes || !test && applyEveryNSeconds == 0)
            {
                test = true;
                damageable.stunned = true;
                appliedTimes++;
                test = false;
            }

        }
    }
    private IEnumerator Destory(float Delay)
    {
        Debug.Log("entered Destory");
        bool alphaBool = true;
        while (alphaBool)
        {
            yield return new WaitForSeconds(Delay);
            Debug.Log("Waited for " + Delay + " seconds");
            Destroy(gameObject);
            alphaBool = false;
        }
    }

}
