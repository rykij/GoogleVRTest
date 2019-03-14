using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets._1_Solution.Scripts.Helpers;
using UnityEngine;
using System;
using UnityEngine.UI;

public class LogWatcherManager : MonoBehaviour {

    private Text logPanel;

    readonly LogWatcherHelper reader = LogWatcherHelper.FromFile(@"C:\Users\portolan\Desktop\AR_VR\UnityPrj\GoogleVrTest2\Test.log");
    
    void Start ()
    {
        logPanel = GameObject.Find("LogText").GetComponent<Text>();

        InvokeRepeating("Read", 1.0f, 1.0f);
    }

    void OnDestroy()
    {
        reader.Dispose();
    }

    void Read()
    {
        logPanel.text = "Sto caricando...";
        reader.Read();
        logPanel.text = reader.Content;
    }

    void Update () {
		
	}
}
