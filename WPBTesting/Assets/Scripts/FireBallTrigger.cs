using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallTrigger : MonoBehaviour {

    public int intialDamage = 10;
    public int tickDamage = 5;

    public bool ignoreCaster = true;
    public float delayBeforeCasting = 0.0f;
    public float applyEveryNSeconds = 1.0f;
    public int applyDamageNTimes = 1;

    private bool delied = false;

    private int appliedTimes = 0;

    private bool test = false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        //all projectile colliding game objects should be tagged "Enemy" or whatever in inspector but that tag must be reflected in the below if conditional
        if (col.gameObject.tag == "Enemy")
        {

            //Destroy(col.gameObject);
            //add an explosion or something
            EnemyShipController curhealth = col.GetComponent<EnemyShipController>();
            //if exists
            if (curhealth != null)
            {
                curhealth.TakeDamage(intialDamage);
                StartCoroutine(CastDamage(curhealth));
            }


            //destroy the projectile that just caused the trigger collision
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "WATER")
        {
            Destroy(gameObject);
        }
    }

    IEnumerator CastDamage(EnemyShipController damageable)
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
            }
            damageable.TakeDamage(tickDamage);
            appliedTimes++;
            test = false;
        }
    }

}
