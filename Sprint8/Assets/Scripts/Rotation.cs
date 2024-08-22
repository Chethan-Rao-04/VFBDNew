using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RotatableNamespace // Use a meaningful namespace name
{
    public class Rotatable : MonoBehaviour
    {
        [SerializeField] private InputAction pressed, axis;

        private Transform cam;
        [SerializeField] private float speed = 1;
        [SerializeField] private bool inverted;
        private Vector2 rotation;
        private bool rotateAllowed;

        private float currentYRotation = 0f; // Current y-axis rotation
        private float targetYRotation = 50f; // Target y-axis rotation for max rotation

        private void Awake()
        {
            cam = Camera.main?.transform;
            if (cam == null)
            {
                Debug.LogError("Main Camera not found!");
                return;
            }

            pressed.Enable();
            axis.Enable();
            pressed.performed += _ => { StartCoroutine(Rotate()); };
            pressed.canceled += _ => { rotateAllowed = false; };
            axis.performed += context => { rotation = context.ReadValue<Vector2>(); };
        }

        private IEnumerator Rotate()
        {
            rotateAllowed = true;
            while (rotateAllowed)
            {
                Vector2 appliedRotation = rotation * speed;

                // Calculate new y-axis rotation
                currentYRotation += appliedRotation.x;
                currentYRotation = Mathf.Clamp(currentYRotation, 0, targetYRotation);

                // Apply y-axis rotation
                transform.localEulerAngles = new Vector3(
                    transform.localEulerAngles.x,
                    currentYRotation,
                    transform.localEulerAngles.z
                );

                // Apply x-axis rotation around the camera's right vector
                float newXRotation = transform.localEulerAngles.x + (appliedRotation.y * (inverted ? -1 : 1));
                transform.localEulerAngles = new Vector3(
                    newXRotation,
                    transform.localEulerAngles.y,
                    transform.localEulerAngles.z
                );

                // Reset rotation for the next frame
                rotation = Vector2.zero;

                yield return null;
            }
        }

        private void OnDisable()
        {
            pressed.Disable();
            axis.Disable();
        }
    }
}
