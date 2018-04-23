using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour {
    public Camera ShipCamera;
    public Camera PlayerCamera;
    public Camera TestCamera;

    // Use this for initialization
    void Start () {
        ShowShipView();
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
        if (Input.GetKeyDown(KeyCode.T))
        {
            ShowTestView();
        }
    }

    public void ShowPlayerView()
    {
        ShipCamera.enabled = false;
        TestCamera.enabled = false;
        PlayerCamera.enabled = true;
    }

    public void ShowShipView()
    {
        ShipCamera.enabled = true;
        TestCamera.enabled = false;
        PlayerCamera.enabled = false;
    }
    public void ShowTestView()
    {
        ShipCamera.enabled = false;
        PlayerCamera.enabled = false;
        TestCamera.enabled = true;
    }
}
