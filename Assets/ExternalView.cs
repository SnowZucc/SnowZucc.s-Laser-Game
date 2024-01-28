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
    { // Checks if player is in external view and teleports the player to the room view or to the external view (Gameobjects)
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
