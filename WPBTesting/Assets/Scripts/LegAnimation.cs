using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegAnimation : MonoBehaviour {

	public Animator LegAnimator;
	// Use this for initialization
	void Start () {
		LegAnimator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		//check if player is moving or the key bindings for movement are being pressed
		if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
		{
			//then play feet animation *********************** update later if you decide to move the feet a certain direction based on key input
			LegAnimator.Play ("Armature|FeetMove");
		}
		else
		{
			//else play the empty animation ***** in other words no animation since it is empty and all
			LegAnimator.Play ("Armature|Empty");
		}


	}
}
