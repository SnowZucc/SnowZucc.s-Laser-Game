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

    public bool doubleRotation = false;
    private bool gravityEnabled = false;

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
        if (!grabbedObject)
            grabbedObject = nearObjects.Count > 0 ? nearObjects[0] : otherHand.grabbedObject;

        if (grabbedObject)
        {
            Vector3 deltaPosition = transform.position - previousPosition;
            Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(previousRotation);

            grabbedObject.position += deltaPosition;

            Quaternion newRotation = deltaRotation * grabbedObject.rotation;
            grabbedObject.rotation = newRotation;

            // Double rotation
            if (doubleRotation)
            {
                Vector3 rotationAxis;
                float rotationAngle;
                deltaRotation.ToAngleAxis(out rotationAngle, out rotationAxis);
                grabbedObject.rotation = Quaternion.AngleAxis(rotationAngle * 2f, rotationAxis) * grabbedObject.rotation;
            }

            previousPosition = transform.position;
            previousRotation = transform.rotation;

            // Get the rigidbody component of the grabbed object (to later disable its gravity when grabbed)
            Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
            // Disable object's rigidbody gravity and stop its movement (otherwise it goes away)
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
    else if (grabbedObject)
    {
        if (gravityEnabled)
        {
            // Get the rigidbody component of the grabbed object
            Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
            // Re-enable object's gravity
            rb.useGravity = true;
        }

        grabbedObject = null;
    }

    previousPosition = transform.position;
    previousRotation = transform.rotation;

    // The buttons to toggle things
    if (Input.GetKeyDown(KeyCode.JoystickButton1))
    {
        doubleRotation = !doubleRotation;
    }

        if (Input.GetKeyDown(KeyCode.JoystickButton2))
    {
        gravityEnabled = !gravityEnabled;
    }
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