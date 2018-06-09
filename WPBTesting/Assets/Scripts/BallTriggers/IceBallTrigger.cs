using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBallTrigger : MonoBehaviour
{
    public int initialDamage = 13;
    public float slow = 0.0f;
    public bool ignoreCaster = true;
    public float delayBeforeCasting = 0.0f;
    public float applyEveryNSeconds = 1.0f;
    public int applyDamageNTimes = 3;//means lasts 3 seconds
    public float oSpeed;// originally speed of the ship
    public GameObject impact;

    private GameObject enemyShip;
    private GameObject myShip;
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
                curhealth.TakeDamage(initialDamage);
                IEnumerator Coroutine = CastDamage(target);
                StartCoroutine(Coroutine);
            }

            var explosion = (GameObject)Instantiate(impact, transform.position, transform.rotation);
            explosion.transform.SetParent(enemyShip.transform);
            explosion.GetComponent<ParticleSystem>().Play();
            explosion.GetComponent<AudioSource>().Play();

            StartCoroutine(DestoryAfterDelay(10.0f, gameObject)); // Destroy after x seconds
            StartCoroutine(DestoryAfterDelay(3.0f, explosion)); // Destroy explosion too
        }

        else if (col.gameObject.tag == "Ship_" + self)
        {
            myShip = GameObject.FindGameObjectWithTag("Ship_" + self);
            //Destroy(col.gameObject);
            //add an explosion or something
            ShipController curhealth = myShip.GetComponent<ShipController>();
            CharacterMovement target = myShip.GetComponentInChildren<CharacterMovement>();
            //if exists
            if (curhealth != null)
            {
                curhealth.TakeDamage(initialDamage);
                IEnumerator Coroutine = CastDamage(target);
                StartCoroutine(Coroutine);
            }

            var explosion = (GameObject)Instantiate(impact, transform.position, transform.rotation);
            explosion.transform.SetParent(myShip.transform);
            explosion.GetComponent<ParticleSystem>().Play();
            explosion.GetComponent<AudioSource>().Play();

            //destroy the projectile that just caused the trigger collision
            StartCoroutine(DestoryAfterDelay(10.0f, gameObject)); // Destroy after x seconds
            StartCoroutine(DestoryAfterDelay(2.8f, explosion));
        }

        if (col.gameObject.tag == "TutorialTarget")
        {
            var explosion = (GameObject)Instantiate(impact, transform.position, transform.rotation);
            explosion.GetComponent<ParticleSystem>().Play();
            explosion.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }

        if (col.gameObject.tag == "WATER")
        {
            //Debug.Log("ice Collide water");
            StartCoroutine(DestoryAfterDelay(10.0f, gameObject));
        }
        else if (col.gameObject.tag == "Rock")
        {
            StartCoroutine(DestoryAfterDelay(1.0f, gameObject));
        }
        GetComponent<IceBallTrigger>().enabled = false;
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
            }

        }
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
