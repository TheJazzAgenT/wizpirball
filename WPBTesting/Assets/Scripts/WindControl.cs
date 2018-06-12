using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindControl : MonoBehaviour {

    private Hv_wind_AudioLib script;
    Hv_waves_AudioLib waves;
    private ShipFixedPathing shipControl;
    private float volume;
    private float waveVolume;
    private float windSpeed;
    private float waveSpeed;
    private float boatSpeed;
    private float prevBoatSpeed = 0;
    private float timer = 0;
    private float timer2 = 0;
    private float lerpTime = 2.0f;

    // Use this for initialization
    void Start () {
        shipControl = GetComponent<ShipFixedPathing>();

        script = GetComponent<Hv_wind_AudioLib>();
        volume = script.GetFloatParameter(Hv_wind_AudioLib.Parameter.Volume);
        script.SetFloatParameter(Hv_wind_AudioLib.Parameter.Volume, 0.9f);
        windSpeed = script.GetFloatParameter(Hv_wind_AudioLib.Parameter.Windspeed);

        waves = GetComponent<Hv_waves_AudioLib>();
        waveVolume = waves.GetFloatParameter(Hv_waves_AudioLib.Parameter.Volume);
        waveSpeed = waves.GetFloatParameter(Hv_waves_AudioLib.Parameter.Volume);

        waves.SendEvent(Hv_waves_AudioLib.Event.Turnon);
    }
	
	// Update is called once per frame
	void Update () {
        boatSpeed = shipControl.getShipVelocity().magnitude;
        if (boatSpeed != prevBoatSpeed)
        {
            StartCoroutine(LerpSpeed(prevBoatSpeed, boatSpeed));
            StartCoroutine(LerpWaveSpeed(prevBoatSpeed, boatSpeed));
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

    private IEnumerator LerpWaveSpeed(float old, float current)
    {
        while (timer2 <= lerpTime)
        {
            float curBoatSpeed = Mathf.Lerp(old, current, timer2 / lerpTime);
            waves.SetFloatParameter(Hv_waves_AudioLib.Parameter.Wavespeed, curBoatSpeed * 5);
            waves.SetFloatParameter(Hv_waves_AudioLib.Parameter.Volume, curBoatSpeed);
            yield return null;
            timer2 += Time.deltaTime;
        }
        timer2 = 0;
    }
}
