using UnityEngine;
using System.Collections;

public class MatSwitch : MonoBehaviour
{

    private Renderer rend;
    private bool altMatEnabled;
    public Material mainMat;
    public Material altMat;

    public void Start()
    {
        rend = GetComponent<Renderer>();
        altMatEnabled = false;
    }

    public void enableAltMat()
    {
        if (rend != null && altMat != null)
        {
            rend.material = altMat;
            altMatEnabled = true;
        }
    }

    public void disableAltMat()
    {
        if (rend != null && mainMat != null) {
            rend.material = mainMat;
            altMatEnabled = false;
        }
    }

    public void toggleMat()
    {
        if(rend != null)
        {
            Material nextMat = (altMatEnabled) ? mainMat : altMat;
            if (nextMat != null) rend.material = nextMat;
        }
    }
}
