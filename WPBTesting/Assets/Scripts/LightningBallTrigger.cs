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
    private int appliedTimes = 0;
    private bool test = false;
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
        //all projectile colliding game objects should be tagged "Enemy" or whatever in inspector but that tag must be reflected in the below if conditional
        if (col.gameObject.tag == "Enemy")
        {
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
                target.stunned = false;
            }

            Destory(5.0f);
            //destroy the projectile that just caused the trigger collision
            //Destroy(gameObject);
        }
        if (col.gameObject.tag == "WATER")
        {
            Debug.Log("Lightning Collide water");
            Destory(5.0f);
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
            if (appliedTimes >= applyDamageNTimes)
            {
                damageable.stunned = false;
                break;
            }
        }
    }
    IEnumerator Destory(float Delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(Delay);
            Destroy(gameObject);
        }
    }

}
