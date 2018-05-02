using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoStore : MonoBehaviour {

    public int[] ChoicesP1;
    public int[] ChoicesP2;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int[] GetLoadout(int player)
    {
        if (player == 1)
        {
            return ChoicesP1;
        }
        if (player == 2)
        {
            return ChoicesP2;
        }
        else
        {
            Debug.Log("Invalid player number");
            return null;
        }
    }
}
