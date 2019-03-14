using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpMessageManager : MonoBehaviour
{

    public GameObject popUpMessage;
    public bool isActive;

    void Start()
    {
        isActive = true;
    }

    void Update ()
    {
        if (isActive)
        {
            popUpMessage.SetActive(true);
        }
        else
        {
            popUpMessage.SetActive(false);
        }
    }

    public void Disable()
    {
        isActive = false;
    }
}
