using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {
    private Image mask;
    private Color white;
    private Color black;
    private float duration = 3.0f;
    private float timer = 0.0f;

	// Use this for initialization
	void Start () {
        mask = GetComponent<Image>();
        white = mask.color;
        black = new Color(white.r, white.g, white.b, 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DoFade()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        Debug.Log("fading");
        while (timer <= duration)
        {
            mask.color = Color.Lerp(white, black, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
