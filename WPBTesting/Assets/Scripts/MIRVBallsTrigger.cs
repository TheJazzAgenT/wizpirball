using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIRVBallsTrigger : MonoBehaviour {

    public int intialDamage = 10;
    public bool ignoreCaster = true;
    public float delayBeforeCasting = 0.0f;
    public float applyEveryNSeconds = 1.0f;
    public int applyDamageNTimes = 3; // means lasts 3 seconds
    public GameObject impact;
    private int player;
    private GameObject enemyShip;
    private int appliedTimes = 0;
    private IEnumerator coroutine;
    private bool test = false;
    private string self;
    private string other;
    public float ConeRadius = 10f;
    public Vector3 move = new Vector3(10,0,0); // offset for the balls, hopefully

    void Start()
    {
        self = GetComponent<PlayerSelector>().me;
        other = GetComponent<PlayerSelector>().notMe;
        enemyShip = GameObject.FindGameObjectWithTag("Ship_" + other);
    }

    void Update()
    {
        //Need to add somethign here so it MIRVs
        IEnumerator Coroutine = CastDamage();
        StartCoroutine(Coroutine);
    }

    public void SetPlayer(int playerNum)
    {
        player = playerNum;
    }

    void OnTriggerEnter(Collider col)
    {
        //all projectile colliding game objects should be tagged "Enemy" or whatever in inspector but that tag must be reflected in the below if conditional
        if (col.gameObject.tag == "Ship_" + other)
        {
            //add an explosion or something
            ShipController curhealth = col.GetComponent<ShipController>();
            CharacterMovement target = col.GetComponentInChildren<CharacterMovement>();
            //if exists
            if (curhealth != null)
            {
                curhealth.TakeDamage(intialDamage);
                
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
            coroutine = Destory(5.0f);
            StartCoroutine(coroutine);
        }
        GetComponent<MIRVBallsTrigger>().enabled = false;
    }
    IEnumerator CastDamage()
    {
        while (true)
        {
            //create 3 balls then delete existing ball, hope this works
            yield return new WaitForSeconds(2);
            var bulletA = Instantiate(gameObject, gameObject.transform.position + move, gameObject.transform.rotation);
            var bulletB = Instantiate(gameObject, gameObject.transform.position, gameObject.transform.rotation);
            var bulletC = Instantiate(gameObject, gameObject.transform.position - move, gameObject.transform.rotation);
            Destroy(gameObject);
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