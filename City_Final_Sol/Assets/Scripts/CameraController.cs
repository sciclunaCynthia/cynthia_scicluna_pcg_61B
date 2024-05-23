using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Reference to the car's transform
    public Vector3 offset = new Vector3(0, 10, -10); // Offset from the car
    public float smoothSpeed = 0.125f; // Smoothing speed

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}
