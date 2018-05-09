using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {

	[SerializeField]
	public float fillAmount = 100;

	[SerializeField]
	private Image content;
    private float maxHealth;
    private float prevfillAmount;
    private float lerpTime = 2.0f;
    private float timer = 0.0f;
    private IEnumerator coroutine;


    public Text Health;

	// Use this for initialization
	void Start () {
        maxHealth = fillAmount;
        prevfillAmount = fillAmount;
		//fillAmount = 100;//this is causing some problems
		Health.text = fillAmount.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		this.HandleBar();
	}

	private void HandleBar()
	{
		//content.fillAmount = Map(fillAmount, 0, maxHealth, 0, 1);
		//Health.text = fillAmount.ToString ();
        if (fillAmount != prevfillAmount)
        {
            if (prevfillAmount - fillAmount < 2)
            {
                content.fillAmount = Map(fillAmount, 0, maxHealth, 0, 1);
                Health.text = fillAmount.ToString ();
                prevfillAmount = fillAmount;
                return;
            }
            StartCoroutine(LerpBar(prevfillAmount, fillAmount));
            prevfillAmount = fillAmount;
        }
	}

    private IEnumerator LerpBar(float prev, float cur)
    {
        while (timer < lerpTime)
        {
            content.fillAmount = Map(Mathf.Lerp(prev, cur, timer), 0, maxHealth, 0, 1);
            timer += Time.deltaTime;
            Health.text = fillAmount.ToString();
            yield return null;
        }
        timer = 0;
    }

	private float Map(float value, float inMin, float inMax, float outMin, float outMax)
	{
		return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin; // for determining health (in the case that health isn't necessarily a range from 0 to 100)
		// example: (80 - 0) * (1 - 0) / (100 - 0) + 0 = 80/100 = 0.8
	}
}
