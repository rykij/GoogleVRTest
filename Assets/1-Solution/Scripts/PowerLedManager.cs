using UnityEngine;

public class PowerLedManager : MonoBehaviour
{
    public Material[] material;
    private Renderer renderer;

	void Start ()
	{
	    renderer = GetComponent<Renderer>();
	    renderer.sharedMaterial = material[0];
    }

    public void Set(string sensorStatus)
    {
        if (sensorStatus == "On")
            renderer.sharedMaterial = material[1];
        else if(sensorStatus == "Off")
            renderer.sharedMaterial = material[0];
        else
            renderer.sharedMaterial = material[0];
    }
}
