using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public Light light; 
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }
    if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
    {
        light.color = blue
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
