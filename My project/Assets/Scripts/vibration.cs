using UnityEngine;

public class ModelVibration : MonoBehaviour
{
    // Amplitude of the vibration (how far it moves)
    [SerializeField] private float amplitude = 0.002f; // Changed the default value to 0.002f

    // Frequency of the vibration (how fast it moves)
    public float frequency = 20f;

    // Original position of the model
    private Vector3 originalPosition;

    void Start()
    {
        // Store the original position of the model
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        // Calculate the vibration offset
        float offsetX = Mathf.Sin(Time.time * frequency) * amplitude;
        float offsetY = Mathf.Cos(Time.time * frequency) * amplitude;
        float offsetZ = Mathf.Sin(Time.time * frequency * 1.5f) * amplitude;

        // Apply the vibration to the model
        transform.localPosition = originalPosition + new Vector3(offsetX, offsetY, offsetZ);
    }
}
