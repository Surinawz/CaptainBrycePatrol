using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private bool ovrFlashLightThrow;
    private Light flashLight;

    // Start is called before the first frame update
    void Start()
    {
        flashLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        ToggleFlashLight();
    }

    private void ToggleFlashLight()
    {
        ovrFlashLightThrow = OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch);
        if (ovrFlashLightThrow)
        {
            if (flashLight.intensity > 0)
            {
                flashLight.intensity = 0;
            }
            else
            {
                flashLight.intensity = 5;
            }
            
        }
      


    }
}
