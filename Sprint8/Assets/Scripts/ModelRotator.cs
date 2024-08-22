using UnityEngine;
using System.Collections;

public class ModelRotator : MonoBehaviour
{
    public Transform model; // The model to be rotated
    public Vector3 sideViewRotationAngles = new Vector3(0, 50, 0); // The rotation angles for the side view
    public float rotationSpeed = 2f; // Speed of the rotation

    private Quaternion originalRotation; // Original rotation
    private Quaternion sideViewRotation; // Side view rotation

    private void Start()
    {
        // Initialize the original and side view rotations
        if (model != null)
        {
            originalRotation = model.rotation;
            sideViewRotation = originalRotation * Quaternion.Euler(sideViewRotationAngles);
        }
    }

    public void ShowSideView()
    {
        if (model != null)
        {
            StartCoroutine(RotateToTarget(sideViewRotation));
        }
    }

    public void ShowFrontView()
    {
        if (model != null)
        {
            StartCoroutine(RotateToTarget(originalRotation));
        }
    }

    private IEnumerator RotateToTarget(Quaternion target)
    {
        while (Quaternion.Angle(model.rotation, target) > 0.1f)
        {
            model.rotation = Quaternion.Lerp(model.rotation, target, Time.deltaTime * rotationSpeed);
            yield return null;
        }
        model.rotation = target;
    }
}
