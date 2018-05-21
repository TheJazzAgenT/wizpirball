using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class PulseColor : MonoBehaviour {
    private RawImage img;
    private Color m_Color;
	// Use this for initialization
	void Start () {
        img = GetComponent<RawImage>();
	}
	
	// Update is called once per frame
	void Update () {
        m_Color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, 1));
        img.color = m_Color;
    }
}
