﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Baseball : MonoBehaviour {
	[SerializeField]
	private Image baseb;
	public bool On;

	// Use this for initialization
	void Start () {
		On = true;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1) || Input.GetButtonDown("A")) 
		{
			On = true;
		}
		if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetButtonDown("B"))
		{
			On = false;
		}
		if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetButtonDown("X"))
		{
			On = false;
		}
		if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetButtonDown("Y"))
		{
			On = false;
		}

		if (On == false) 
		{
			baseb.GetComponent<Image> ().color = new Color32 (100, 100, 100, 100);
			//baseb.color = vector3(100, 100, 100);
		} else if (On == true) 
		{
			baseb.GetComponent<Image> ().color = new Color32 (255, 255, 255, 255);
		
		}
	}
}