using System;
using UnityEngine;

public class LampLightManager : MonoBehaviour {

    public GameObject LampSx;
    public GameObject LampDx;
    public bool isActive;

    private Light lightSx;
    private Light lightDx;

    private ArduinoSensorManager arduino;

    public event EventHandler LightIsTurnedOn;
    public event EventHandler LightIsTurnedOff;

    void Start()
    {
        arduino = GameObject.Find("Arduino").GetComponent<ArduinoSensorManager>();
        

        lightSx = LampSx.GetComponent<Light>();
        lightDx = LampDx.GetComponent<Light>();

        //isActive = false;
    }
	void Update ()
    {
      
    }

    public void TurnOnOffLight()
    {
        if (isActive)
        {
            LightIsTurnedOff(this, null);
            isActive = false;
        }
        else
        {
            LightIsTurnedOn(this, null);
            isActive = true;
        }
    }

    public void Set(string sensorValue)
    {
        if (sensorValue == "On")
        {
            lightSx.enabled = true;
            lightDx.enabled = true;
            isActive = true;
        }
        else
        {
            lightSx.enabled = false;
            lightDx.enabled = false;
            isActive = false;
        }
    }
}
