﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lightningball : MonoBehaviour 
{
	[SerializeField]
	private Image lightningb;

	public bool On;

	// Use this for initialization
	void Start () 
	{
		On = false;

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1)) 
		{
			On = false;
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			On = false;
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			On = false;
		}
		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			On = true;
		}

		if (On == false) 
		{
			lightningb.GetComponent<Image> ().color = new Color32 (100, 100, 100, 100);
			//baseb.color = vector3(100, 100, 100);
		} else if (On == true) 
		{
			lightningb.GetComponent<Image> ().color = new Color32 (255, 255, 255, 255);

		}
	}
}
