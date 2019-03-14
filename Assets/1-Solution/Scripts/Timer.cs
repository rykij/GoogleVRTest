using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private Text Display { get; set; }


    void Start () {
	    Display = GameObject.Find("ClockText").GetComponent<Text>();
    }
	
	void Update ()
	{
	    StartCoroutine(Wait());
	}

    IEnumerator Wait()
    {
        UpdateTimer();
        yield return new WaitForSeconds(1);
    }

   void  UpdateTimer()
    {      
        DateTime today = DateTime.Now;
        Display.text = today.Hour + ":" + today.Minute;
    }

}
