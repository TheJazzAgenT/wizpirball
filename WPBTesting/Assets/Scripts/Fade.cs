using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {
    public bool doSceneStartFade = false;

    private Image mask;
    private Color clear;
    private Color opaque;
    private float duration = 3.0f;
    private float timer = 0.0f;

	// Use this for initialization
	void Start () {
        mask = GetComponent<Image>();
        clear = mask.color;
        opaque = new Color(clear.r, clear.g, clear.b, 1);

        if (doSceneStartFade)
        {
            DoFade(2.0f, true);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DoFade(float speed, bool isFadeIn)
    {
        StartCoroutine(StartFade(speed, isFadeIn));
    }

    private IEnumerator StartFade(float speed, bool direction)
    {
        //Debug.Log("fading");
        while (timer <= speed)
        {
            mask.color = Color.Lerp(direction ? opaque : clear, direction ? clear : opaque, timer / speed);
            timer += Time.deltaTime;
            yield return null;
        }
        timer = 0;
    }
}
