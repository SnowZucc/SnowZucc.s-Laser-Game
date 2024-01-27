using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform roompoint;
    public Transform externalpoint;

    private bool isExternalView = false;

    // Update is called once per frame
    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            if (isExternalView)
            {
                transform.position = roompoint.position;
                isExternalView = false;
            }
            else
            {
                transform.position = externalpoint.position;
                isExternalView = true;
            }
            
        }
    }
}
