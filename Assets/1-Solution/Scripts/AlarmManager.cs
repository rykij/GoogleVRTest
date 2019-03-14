using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AlarmManager : MonoBehaviour {

    public float blinkingTimeInSeconds;   

    public bool isActive;
    public Light alarmLightSX;
    public Light alarmLightDX;

    private AudioSource alertSound;
    public AudioClip alert;
    private bool isPlayingSound;

    private AudioSource alertVoice;
    public AudioClip voice;

    private RawImage imgWarning;
    private Button btnAlarmStop;

    void Start () {
        alarmLightSX = GameObject.Find("AlarmLightSX").GetComponent<Light>();
        alarmLightDX = GameObject.Find("AlarmLightDX").GetComponent<Light>();

        alertSound = GetComponent<AudioSource>();
        alertVoice = GetComponent<AudioSource>();

        imgWarning = GameObject.Find("ImgWarning").GetComponent<RawImage>();
        btnAlarmStop = GameObject.Find("ButtonStop").GetComponent<Button>();

        isActive = false;
        isPlayingSound = false;
        imgWarning.enabled = false;
        btnAlarmStop.interactable = false;
    }
	
	void Update ()
	{ 
        if (isActive)
	    {
	        StartCoroutine(StartAlarm());
	    }
	    else
	    {
	        StartCoroutine(StopAlarm());
	    }
	}

    private IEnumerator StartAlarm()
    {
        btnAlarmStop.interactable = true;
        btnAlarmStop.image.color = btnAlarmStop.colors.normalColor;

        StartCoroutine(FlashNow());

        if (!isPlayingSound)
        {
            isPlayingSound = true;
            StartCoroutine(PlayAlarmSound());
            StartCoroutine(PlayAlarmVoice());
        }

        yield return null;
    }

    private IEnumerator StopAlarm()
    {
        btnAlarmStop.interactable = false;
        btnAlarmStop.image.color = btnAlarmStop.colors.disabledColor;

        alarmLightSX.enabled = false;
        alarmLightDX.enabled = false;
        isPlayingSound = false;

        yield return null;
    }

    private IEnumerator FlashNow()
    {

        while (isActive)
        {
            if (alarmLightSX.enabled == false && alarmLightDX.enabled == false)
            {
                yield return new WaitForSeconds(blinkingTimeInSeconds);  
                alarmLightSX.enabled = true;
                alarmLightDX.enabled = true;
                imgWarning.enabled = true;
                
            }
            else
            {
                yield return new WaitForSeconds(blinkingTimeInSeconds);
                alarmLightSX.enabled = false;
                alarmLightDX.enabled = false;
                imgWarning.enabled = false;

            }
        }
    }

    private IEnumerator PlayAlarmSound()
    {
        while (isActive)
        {            
            alertSound.PlayOneShot(alert, 0.4F);
            yield return new WaitForSeconds(2);
        }
        
    }

    private IEnumerator PlayAlarmVoice()
    {
        while (isActive)
        {           
            alertSound.PlayOneShot(voice, 0.9F);
            yield return new WaitForSeconds(8);
        }

    }

    public void Active(string statusValuer)
    {
        if (statusValuer == "On")
            this.isActive = true;
    }

    public void Disable()
    {
        this.isActive = false;
    }
}
