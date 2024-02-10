using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomGrab : MonoBehaviour
{
    // This script should be attached to both controller objects in the scene
    // Make sure to define the input in the editor (LeftHand/Grip and RightHand/Grip recommended respectively)
    CustomGrab otherHand = null;
    public List<Transform> nearObjects = new List<Transform>();
    public Transform grabbedObject = null;
    public InputActionReference action;
    bool grabbing = false;

    private Vector3 previousPosition;
    private Quaternion previousRotation;

    // Additional feature to double the rotation
    public bool doubleRotation = false;

    private void Start()
    {
        action.action.Enable();

        // Find the other hand
        foreach(CustomGrab c in transform.parent.GetComponentsInChildren<CustomGrab>())
        {
            if (c != this)
                otherHand = c;
        }
    }

void Update()
{
    grabbing = action.action.IsPressed();
    if (grabbing)
    {
        // Grab nearby object or the object in the other hand
        if (!grabbedObject)
            grabbedObject = nearObjects.Count > 0 ? nearObjects[0] : otherHand.grabbedObject;

        if (grabbedObject)
        {
            // Calculate delta position and rotation
            Vector3 deltaPosition = transform.position - previousPosition;
            Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(previousRotation);

            // Apply delta position and rotation to the grabbed object
            grabbedObject.position += deltaPosition;

            // Calculate new rotation for the grabbed object
            Quaternion newRotation = deltaRotation * grabbedObject.rotation;
            grabbedObject.rotation = newRotation;

            // Double the rotation if enabled
            if (doubleRotation)
            {
                Vector3 rotationAxis;
                float rotationAngle;
                deltaRotation.ToAngleAxis(out rotationAngle, out rotationAxis);
                grabbedObject.rotation = Quaternion.AngleAxis(rotationAngle * 2f, rotationAxis) * grabbedObject.rotation;
            }

            // Update previous position and rotation
            previousPosition = transform.position;
            previousRotation = transform.rotation;
        }
    }
    // If let go of button, release object
    else if (grabbedObject)
        grabbedObject = null;

    // Should save the current position and rotation here
    previousPosition = transform.position;
    previousRotation = transform.rotation;
}

    private void OnTriggerEnter(Collider other)
    {
        // Make sure to tag grabbable objects with the "grabbable" tag
        // You also need to make sure to have colliders for the grabbable objects and the controllers
        // Make sure to set the controller colliders as triggers or they will get misplaced
        // You also need to add Rigidbody to the controllers for these functions to be triggered
        // Make sure gravity is disabled though, or your controllers will (virtually) fall to the ground

        Transform t = other.transform;
        if(t && t.tag.ToLower()=="grabbable")
            nearObjects.Add(t);
    }

    private void OnTriggerExit(Collider other)
    {
        Transform t = other.transform;
        if( t && t.tag.ToLower()=="grabbable")
            nearObjects.Remove(t);
    }
}