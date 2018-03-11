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
		if (Input.GetKeyDown ("w")) {
			LegAnimator.Play ("Armature|FeetMove");
		}
		if (Input.GetKeyUp ("w")) {
			LegAnimator.Play ("Armature|Empty");
		}

	}
}
