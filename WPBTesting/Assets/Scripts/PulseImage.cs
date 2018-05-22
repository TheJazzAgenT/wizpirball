using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PulseImage : MonoBehaviour {
    public Color pulseColor;

    private Image img;
    private Color startColor;
    private bool pulsing = true;
	// Use this for initialization
	void Start () {
        img = GetComponent<Image>();
        startColor = img.color;
	}

    public void StartPulse()
    {
        StartCoroutine(Pulse());
    }
    public void StopPulse()
    {
        pulsing = false;
        img.color = startColor;
    }

    private IEnumerator Pulse()
    {
        while (pulsing)
        {
            img.color = Color.Lerp(startColor, pulseColor, Mathf.PingPong(Time.time * 2, 1));
            yield return null;
        }
    }
}
