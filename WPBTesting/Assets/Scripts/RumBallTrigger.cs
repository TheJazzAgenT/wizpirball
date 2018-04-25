using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RumBallTrigger : MonoBehaviour {
    public int intialDamage = 13;
    public float slow = 0.0f;
    public bool ignoreCaster = true;
    public float delayBeforeCasting = 0.0f;
    public float applyEveryNSeconds = 1.0f;
    public int applyDamageNTimes = 3;//means lasts 3 seconds
    public float oSpeed;// originally speed of the ship
    public GameObject impact;
    public int manaCost;

    private GameObject enemyShip;
    private int appliedTimes = 0;
    private IEnumerator coroutine;
    private bool test = false;
    private string self;
    private string other;

    void Start()
    {
        self = GetComponent<PlayerSelector>().me;
        other = GetComponent<PlayerSelector>().notMe;
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
            CharacterMovement target = enemyShip.GetComponentInChildren<CharacterMovement>();
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
            explosion.GetComponent<AudioSource>().Play();

            Destory(10.0f);//destroy after 5 seconds
        }
        if (col.gameObject.tag == "WATER")
        {
            //Debug.Log("ice Collide water");
            coroutine = Destory(10.0f);
            StartCoroutine(coroutine);
        }
        GetComponent<RumBallTrigger>().enabled = false;
    }

    IEnumerator CastDamage(CharacterMovement damageable)
    {
        while (true)
        {
            yield return new WaitForSeconds(applyEveryNSeconds);
            if (appliedTimes >= applyDamageNTimes)
            {
                damageable.stunned = false;
                break;
            }
            if (!test && appliedTimes <= applyDamageNTimes || !test && applyEveryNSeconds == 0)
            {
                test = true;
                damageable.stunned = true;
                appliedTimes++;
                test = false;
                //add something here to activate the blurry vision
            }

        }
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