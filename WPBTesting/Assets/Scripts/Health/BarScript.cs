using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {

	[SerializeField]
	public float fillAmount;

	[SerializeField]
	private Image content;


	[SerializeField]
	private 
	// Use this for initialization
	void Start () {
		fillAmount = 100;
	}
	
	// Update is called once per frame
	void Update () {
		this.HandleBar();
	}

	private void HandleBar()
	{
		content.fillAmount =Map(fillAmount, 0, 100, 0, 1);
	}

	private float Map(float value, float inMin, float inMax, float outMin, float outMax)
	{
		return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin; // for determining health (in the case that health isn't necessarily a range from 0 to 100)
		// example: (80 - 0) * (1 - 0) / (100 - 0) + 0 = 80/100 = 0.8
	}
}
