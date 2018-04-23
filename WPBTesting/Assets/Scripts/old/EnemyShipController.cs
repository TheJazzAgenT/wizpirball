using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipController : MonoBehaviour {

    static public int maxhealth = 100;
    static public int curhealth = 100;
    public bool alive;

	[SerializeField]
	private BarScript Bar;
     
    void Start()
    {
        curhealth = maxhealth;
        alive = true;
    }
    void Update()
    {
        if (curhealth < 1)
        {
            CommitSudoku();
        }
    }

    public void TakeDamage( int amount)
    {
        curhealth -= amount;
		Bar.fillAmount = curhealth;
    }

    private void CommitSudoku()
    {
        alive = false;
        GetComponent<ShipFixedPathing>().enabled = false;
        GetComponent<Rigidbody>().useGravity = true;
    }
}
