using System;
using UnityEngine;
using UnityEngine.UI;

public class ThermostatManager : MonoBehaviour
{
    private GameObject thermostatPanel;
    private Button thermostatSetButton;
    private Text thermostatTitleText;
    private Text threshold;
    private Text currTemp;
    private Text boilerStatus;
    private Animation thermostatPanelAnimation;

    private static int currThreshold;
   
    private bool isActive;

    void Start()
    {
        thermostatPanel = GameObject.Find("ThermostatPanel");
        thermostatSetButton = GameObject.Find("ButtonSetThreshold").GetComponent<Button>();
        thermostatTitleText = GameObject.Find("ThermostatTitleText").GetComponent<Text>();
        threshold =  GameObject.Find("ThermostatSetTempText").GetComponent<Text>();
        currTemp = GameObject.Find("ThermostatCurrTempText").GetComponent<Text>();
        boilerStatus = GameObject.Find("BoilerStatusText").GetComponent<Text>();
        thermostatPanelAnimation = GameObject.Find("ThermostatPanel").GetComponent<Animation>();

        currThreshold = 18;
        threshold.text = currThreshold.ToString();

        HidePanel();
        SetBoilerStatus();        
    }

    private void HidePanel()
    {
        thermostatPanel.transform.localScale = new Vector3(0, 0, 0);
        isActive = false;
    }

    void Update ()
	{
	    
    }

    public void ShowHideThermostatPanel()
    {
        if (isActive)
        {
            CloseUi();
        }
        else
        {
            OpenUi();
        }

    }

    private void CloseUi()
    {
        thermostatPanelAnimation.Play("ThermostatPanel");
        isActive = false;
    }

    private void OpenUi()
    {
        thermostatTitleText.text = "SET THRESHOLD";
        thermostatPanelAnimation.Play("ThermostatPanel_On");
        threshold.text = currThreshold.ToString();
        isActive = true;
    }

    public void SetTemperature()
    {
        thermostatTitleText.text = "UPDATED";
        currThreshold = int.Parse(threshold.text);
    }

    public void IncreaseTemperature()
    {
        thermostatTitleText.text = "SET THRESHOLD";
        int value = int.Parse(threshold.text);
        value += 1;
        threshold.text = value.ToString();
        SetBoilerStatus();
    }

    public void DecreaseTemperature()
    {
        thermostatTitleText.text = "SET THRESHOLD";
        int value = int.Parse(threshold.text);
        value += 1;
        threshold.text = value.ToString();
        SetBoilerStatus();
    }

    private void SetBoilerStatus()
    {
        if (currTemp.text != "---")
        {
            int currTempValue = int.Parse(currTemp.text);
            int thresholdValue = int.Parse(threshold.text);

            if (thresholdValue > currTempValue)
                boilerStatus.text = "On";
            else
                boilerStatus.text = "Off";
        }
    }
}
