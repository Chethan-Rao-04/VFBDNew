using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    public Transform target;        // The model that the camera will focus on
    public Vector3 offset = new Vector3(0, 5, -10); // Offset to maintain distance from the model
    public float smoothSpeed = 0.125f; // Smooth transition speed
    public bool lookAtTarget = true; // Whether the camera should look at the target

    private void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Target not assigned for CameraFocus script.");
            return;
        }

        // Desired position of the camera
        Vector3 desiredPosition = target.position + offset;
        // Smooth transition to the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Optionally look at the target
        if (lookAtTarget)
        {
            transform.LookAt(target);
        }
    }
}
