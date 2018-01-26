using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour {
    public Camera ShipCamera;
    public Camera PlayerCamera;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (ShipCamera.enabled == true)
            {
                ShowPlayerView();
            }
            else
            {
                ShowShipView();
            }
        }
    }

    public void ShowPlayerView()
    {
        ShipCamera.enabled = false;
        PlayerCamera.enabled = true;
    }

    public void ShowShipView()
    {
        ShipCamera.enabled = true;
        PlayerCamera.enabled = false;
    }


}
