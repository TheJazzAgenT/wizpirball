using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScriptEnemy : MonoBehaviour {

	[SerializeField]
	public float fillAmountE;

	[SerializeField]
	private Image contentTwo;


	public Text EnemyHealth;
	// Use this for initialization
	void Start () {
		fillAmountE = 100;
		EnemyHealth.text = fillAmountE.ToString ();
	}

	// Update is called once per frame
	void Update () {
		HandleBarEnemy ();
	}

	private void HandleBarEnemy()
	{
		contentTwo.fillAmount =Map(fillAmountE, 0, 100, 0, 1);
		EnemyHealth.text = fillAmountE.ToString ();	
	}

	private float Map(float value, float inMin, float inMax, float outMin, float outMax)
	{
		return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin; // for determining health (in the case that health isn't necessarily a range from 0 to 100)
		// example: (80 - 0) * (1 - 0) / (100 - 0) + 0 = 80/100 = 0.8
	}
}
