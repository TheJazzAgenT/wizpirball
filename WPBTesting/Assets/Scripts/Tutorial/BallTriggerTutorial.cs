using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTriggerTutorial : MonoBehaviour
{
    public int damage = 20; // Damage dealt by this ball
    public GameObject impact; // Impact effect

    private GameObject enemyShip;
    private CharacterMovementTutorial myChar;
    private List<GameObject> lightningBalls = new List<GameObject>();
    private Rigidbody rb;
    private float magnetism = 500.0f; // How much is this ball effected by magnetic balls
    private string self; // Which player fired me
    private string other;

    void Awake()
    {

    }

    // Use this for initialization
    void Start()
    {
        self = GetComponent<PlayerSelector>().me;
        other = GetComponent<PlayerSelector>().notMe;
        myChar = GameObject.Find("Player" + self[1]).GetComponent<CharacterMovementTutorial>();
        rb = GetComponent<Rigidbody>();
        // Find all lightning balls currently in the scene, add only the ones fired by current player to the list
        var meBalls = GameObject.FindGameObjectsWithTag("LightningBall");
        foreach (GameObject lBall in meBalls)
        {
            //Debug.Log("lball: " + lBall.GetComponent<LightningBallTrigger>().self + " me: " + self);
            if (lBall.GetComponent<LightningBallTrigger>().self == self)
            {
                lightningBalls.Add(lBall);
            }
        }
        //Debug.Log("fired bball");
        //Debug.Log(lightningBalls.Count);
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
        
        if (col.gameObject.tag == "WATER")
        {
            StartCoroutine(DestoryAfterDelay(3.0f, gameObject));
        }
        if (col.gameObject.tag == "TutorialTarget")
        {
            myChar.tHits += 1;
            var explosion = (GameObject)Instantiate(impact, transform.position, transform.rotation);
            explosion.GetComponent<ParticleSystem>().Play();
            explosion.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
    }

    private IEnumerator DestoryAfterDelay(float Delay, GameObject destroyable)
    {
        //Debug.Log("entered Destory");
        bool alphaBool = true;
        while (alphaBool)
        {
            yield return new WaitForSeconds(Delay);
            //Debug.Log("Waited for " + Delay + " seconds");
            Destroy(destroyable);
            alphaBool = false;
        }
    }
}
