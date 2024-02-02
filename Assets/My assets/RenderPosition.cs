using UnityEngine;
public class FollowCameraRotation : MonoBehaviour
{
    public Transform camera1;
    public Transform camera2;

void Update()
{
    Quaternion targetRotation = Quaternion.Euler(camera1.rotation.eulerAngles.x, camera1.rotation.eulerAngles.y, 0);
    camera2.rotation = targetRotation;
}
}