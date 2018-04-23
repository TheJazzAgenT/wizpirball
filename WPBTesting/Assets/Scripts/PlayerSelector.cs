using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelector : MonoBehaviour {

    public string me;
    public string notMe;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetPlayer(string self, string other)
    {
        me = self;
        notMe = other;
    }
}
