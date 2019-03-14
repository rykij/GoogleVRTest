using UnityEngine;

public class SurvellianceManager : MonoBehaviour
{
    public GameObject cameraPanel;
    public GameObject logPanel;
    public GameObject mapPanel;
    
    private Animation cameraPanelAnimation;
    private Animation logPanelAnimation;
    private Animation mapPanelAnimation;

    private bool isActive = false;


    void Start()
    {
        cameraPanelAnimation = cameraPanel.GetComponent<Animation>();
        logPanelAnimation = logPanel.GetComponent<Animation>();
        mapPanelAnimation = mapPanel.GetComponent<Animation>();

        HidePanels();
        isActive = false;      
    }

    private void HidePanels()
    {
        cameraPanel.transform.localScale = new Vector3(0, 0, 0);
        logPanel.transform.localScale = new Vector3(0, 0, 0);
        mapPanel.transform.localScale = new Vector3(0, 0, 0);
    }

    void Update()
    {
      
    }

    public void ShowHideConsolePanels()
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
        cameraPanelAnimation.Play("CameraPanel");
        logPanelAnimation.Play("LogPanel");
        mapPanelAnimation.Play("MapPanel");

        isActive = false;
    }

    private void OpenUi()
    {
        cameraPanelAnimation.Play("CameraPanel_On");
        logPanelAnimation.Play("LogPanel_On");       
        mapPanelAnimation.Play("MapPanel_On");

        isActive = true;
    }

   


}

