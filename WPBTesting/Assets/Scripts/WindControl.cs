using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindControl : MonoBehaviour {

    private Hv_wind_AudioLib script;
    private ShipFixedPathing shipControl;
    private float volume;
    private float windSpeed;
    private float boatSpeed;
    private float prevBoatSpeed = 0;
    private float timer = 0;
    private float lerpTime = 2.0f;

    // Use this for initialization
    void Start () {
        shipControl = GetComponent<ShipFixedPathing>();
        script = GetComponent<Hv_wind_AudioLib>();
        volume = script.GetFloatParameter(Hv_wind_AudioLib.Parameter.Volume);
        windSpeed = script.GetFloatParameter(Hv_wind_AudioLib.Parameter.Windspeed);
    }
	
	// Update is called once per frame
	void Update () {
        boatSpeed = shipControl.getShipVelocity().magnitude;
        if (boatSpeed != prevBoatSpeed)
        {
            StartCoroutine(LerpSpeed(prevBoatSpeed, boatSpeed));
            prevBoatSpeed = boatSpeed;
        }
        //Debug.Log("SPOOD: " + boatSpeed);
        //script.SetFloatParameter(Hv_wind_AudioLib.Parameter.Windspeed, boatSpeed * 20);
    }

    private IEnumerator LerpSpeed(float old, float current)
    {
        while(timer <= lerpTime)
        {
            float curWindSpeed = Mathf.Lerp(old, current, timer / lerpTime);
            script.SetFloatParameter(Hv_wind_AudioLib.Parameter.Windspeed, curWindSpeed * curWindSpeed);
            yield return null;
            timer += Time.deltaTime;
        }
        timer = 0;
    }
}
