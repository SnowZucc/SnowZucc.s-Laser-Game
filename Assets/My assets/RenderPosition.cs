using System;
using UnityEngine;
public class FollowCameraRotation : MonoBehaviour
{
    public Transform camera1;
    public Transform camera2;

void Start()
{
    // Set the initial rotation of camera1 and camera2 so camera2 does not look sideways in method 2 (does not work)
    camera1.rotation = Quaternion.Euler(0, 0, 0);
    camera2.rotation = Quaternion.Euler(0, 0, 0);
}

void Update()
{

    // Method 1 : Instant syncronization but the z axis of the lens stays to 0

    //Quaternion targetRotation = Quaternion.Euler(camera1.rotation.eulerAngles.x, camera1.rotation.eulerAngles.y, 0);
    //camera2.rotation = targetRotation;

    // Method 2 : z axis of the lens stays the ground on the bottom but gets gimbal locked and loses z syncronization
    // I used Slerp to make the transition smooth to limit the gimbal lock effect

    Quaternion targetRotation = Quaternion.Euler(camera1.rotation.eulerAngles.x, camera1.rotation.eulerAngles.y, camera2.rotation.eulerAngles.z);
    camera2.rotation = Quaternion.Slerp(camera2.rotation, targetRotation, Time.deltaTime * 10);

    //Quaternion targetRotation = camera1.rotation;

}
}