using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class LightSwitch : MonoBehaviour
{
    public Light light; 
    Color blue = new Color(0, 0, 1, 1);

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        // if oculus touch controller B button is pressed
        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            light.color = blue;
        }

        // change the light every frame (test for fun)
         //light.color = new Color(Random.value, Random.value, Random.value, 1);
    }
}
