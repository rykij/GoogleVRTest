using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ArduinoVideoManager : MonoBehaviour
{
    public RawImage rawImage;

    public float GetRate = 0.15f;

    public bool isPlaying;
    public Renderer cameraPanel;
    public Material start;
    public Material stop;

    private float nextGet;

    private string url;
    private Text title;
    private Text description;
    private Text infoCamera;

    private bool isLoading;
    private bool isLoaded;
    private bool isError;

    void Start()
    {
        url = "http://hal9000.local:8080/?action=snapshot";
        title = GameObject.Find("TitleCameraText").GetComponent<Text>();
        description = GameObject.Find("DescriptionCameraText").GetComponent<Text>();
        infoCamera = GameObject.Find("InfoCameraText").GetComponent<Text>();

        nextGet = 0;
        rawImage.enabled = false;
        infoCamera.text = "";
        isLoading = true;
        isError = false;

        Application.runInBackground = true;
    }

   
    void Update()
    {
        if (isPlaying)
        {
            if (!isError)
            {
                UpdateVideoInfo();
                StartCoroutine(SetPlayStatus());

                if (Time.time > nextGet)
                {
                    StartCoroutine(GetVideoTexture());
                    nextGet = Time.time + GetRate;
                }
            }
            else
            {
                StartCoroutine(SetConnectionErrorStatus());
            }
        }
        else
        {           
            SetStopStatus();
        }
    }

    IEnumerator SetPlayStatus()
    {
        if (isLoading)
        {
            title.text = "Connecting...";
            description.text = "please wait";
            infoCamera.text = "";
            yield return new WaitForSeconds(10f);

            isLoading = false;
            isLoaded = true;
        }
        else if (isLoaded)
        {
            title.text = "";
            description.text = "";
            cameraPanel.material = start;
            rawImage.enabled = true;
        }
    }

    IEnumerator GetVideoTexture()
    {       
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log("Error");
                Debug.Log(uwr.error);
                isError = true;
            }
            else
            {
                Debug.Log("Done");
               
                var texture = DownloadHandlerTexture.GetContent(uwr);                 
                rawImage.texture = texture;                
            }
        }
    }

    IEnumerator SetConnectionErrorStatus()
    {
        title.text = "NO CAMERA FOUND";
        description.text = "check your connection and try again";
        infoCamera.text = "";
        cameraPanel.material = stop;
        rawImage.enabled = false;

        yield return new WaitForSeconds(5f);

        isPlaying = false;
    }

    private void  UpdateVideoInfo()
    {
        infoCamera.text = "connected to cam01 " + DateTime.Now.ToLongDateString().ToLower() + " " + DateTime.Now.ToLongTimeString().ToLower();
    }

    private void SetStopStatus()
    {
        title.text = "VIDEO";
        description.text = "press camera button on control panel to connect";
        infoCamera.text = "";
        cameraPanel.material = stop;
        rawImage.enabled = false;
    }

    public void ConnectToCum()
    {
        if (isPlaying)
            isPlaying = false;
        else
            isPlaying = true;
    }
}
