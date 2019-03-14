using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class ArduinoSensorManager : MonoBehaviour
{
    private const string ADDRESS = "http://hal9000.local/arduino/";
    private const string POWER_PIN_READER = "digital/5";
    private const string LIGHT_PIN_READER = "digital/4";
    private const string TEMPERATURE_PIN_READ = "analog/0";
    private const string MOTION_PIN_READ = "digital/8";

    private const string LIGHT_PIN_WRITER_ON = "digital/4/1";
    private const string LIGHT_PIN_WRITER_OFF = "digital/4/0";


    private Text tempText;
    private Text thermostatText;
    private Text lightText;

    private PowerLedManager powerLed;
    private AlarmManager alarm;
    private LampLightManager light;

   
    void Awake()
    {
        tempText= GameObject.Find("TempText").GetComponent<Text>();
        lightText = GameObject.Find("LightText").GetComponent<Text>();
        thermostatText = GameObject.Find("ThermostatCurrTempText").GetComponent<Text>();

        powerLed = GameObject.Find("StatusLed").GetComponent<PowerLedManager>();
        light = GameObject.Find("Lamps").GetComponent<LampLightManager>();
        alarm = GameObject.Find("Alarm").GetComponent<AlarmManager>();

        light.LightIsTurnedOn += SetLightStatusOn;
        light.LightIsTurnedOff += SetLightStatusOff;

        StartCoroutine(GetPowerStatus());
        StartCoroutine(GetLightStatus());
        StartCoroutine(GetTemperaturetatus());
        StartCoroutine(GetMotionStatus());
    }

    void Start()
    {
        
    }

 
    IEnumerator GetPowerStatus()
    {
        string url = ADDRESS + POWER_PIN_READER;
        while (true)
        {
            yield return new WaitForSeconds(2f);
            StartCoroutine(GetSensorValueFor(SensorType.POWER, url));
        }
    }

    IEnumerator GetLightStatus()
    {
        string url = ADDRESS + LIGHT_PIN_READER;
        while (true)
        { 
            yield return new WaitForSeconds(2f);
            StartCoroutine(GetSensorValueFor(SensorType.LIGHT, url));
        }
    }

    IEnumerator GetTemperaturetatus()
    {
        string url = ADDRESS + TEMPERATURE_PIN_READ;
        while (true)
        {
            yield return new WaitForSeconds(5f);
            StartCoroutine(GetSensorValueFor(SensorType.TEMPERATURE, url));
        }
    }

    IEnumerator GetMotionStatus()
    {
        string url = ADDRESS + MOTION_PIN_READ;
        while (true)
        {
            yield return new WaitForSeconds(1f);
            StartCoroutine(GetSensorValueFor(SensorType.MOTION, url));
        }
    }

  
    IEnumerator GetSensorValueFor(SensorType _sensorType, string _url)
    {
        UnityWebRequest request = UnityWebRequest.Get(_url);
        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            Debug.Log("Error While Sending: " + request.error);
        }
        else
        {
            string sensorValue = FormatValue(request.downloadHandler.text);

            Debug.Log("<color=green>END " + _sensorType + " CALL</color> VALUE IS " + sensorValue);

            switch (_sensorType)
            {
                case SensorType.POWER:                    
                    powerLed.Set(sensorValue);
                    break;

                case SensorType.LIGHT:
                    light.Set(sensorValue);
                    lightText.text = sensorValue;
                    break;

                case SensorType.TEMPERATURE:
                    tempText.text = sensorValue;
                    thermostatText.text = tempText.text;
                    break;

                case SensorType.MOTION:
                    alarm.Active(sensorValue);
                    break;
            }           
        }
    }

    private static string FormatValue(string value)
    {
        string formattedValue = "- - -";

        if (value != null)
        {
            if (value.Trim() == "0")
                formattedValue = "Off";
            else if (value.Trim() == "1")
                formattedValue = "On";
            else
                formattedValue = value.Trim();
        }

        return formattedValue;
    }

    public void SetLightStatusOn(object sender, EventArgs e)
    {
        string url = ADDRESS + LIGHT_PIN_WRITER_ON;

        StartCoroutine(GetSensorValueFor(SensorType.LIGHT_ON, url));
    }

    public void SetLightStatusOff(object sender, EventArgs e)
    {
        string url = ADDRESS + LIGHT_PIN_WRITER_OFF;

        StartCoroutine(GetSensorValueFor(SensorType.LIGHT_OFF, url));
    }
}
