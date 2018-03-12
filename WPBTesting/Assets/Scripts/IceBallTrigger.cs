using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBallTrigger : MonoBehaviour {

    public int intialDamage = 5;
    public float slow = 30.0f;
    public bool ignoreCaster = true;
    public float delayBeforeCasting = 0.0f;
    public float applyEveryNSeconds = 1.0f;
    public int applyDamageNTimes = 10;
    public float oSpeed;

    private bool delied = false;

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
            Debug.Log("Ice Collide test");
            //Destroy(col.gameObject);
            //add an explosion or something
            EnemyShipController curhealth = col.GetComponent<EnemyShipController>();
            ShipFixedPathing target = col.GetComponent<ShipFixedPathing>();
            //if exists
            if (curhealth != null)
            {
                curhealth.TakeDamage(intialDamage);
                oSpeed = target.secondsForOneLength;
                StartCoroutine(CastDamage(target));
                target.secondsForOneLength = oSpeed;
            }


            Destory(5.0f);
            //destroy the projectile that just caused the trigger collision
            //Destroy(gameObject);
        }
        if (col.gameObject.tag == "WATER")
        {
            Debug.Log("Ice Collide water");
            Destory(5.0f);
            //Destroy(gameObject);
        }
    }

    IEnumerator CastDamage(ShipFixedPathing damageable)
    {
        if (!test && appliedTimes <= applyDamageNTimes || !test && applyEveryNSeconds == 0)
        {
            test = true;
            if (!delied)
            {
                yield return new WaitForSeconds(delayBeforeCasting);
                delied = true;
            }
            else
            {
                yield return new WaitForSeconds(applyEveryNSeconds);
                damageable.secondsForOneLength = slow;
            }
            appliedTimes++;
            test = false;
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

