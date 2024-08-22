using UnityEngine;

public class SmokeEffectManager : MonoBehaviour
{
    private const float radius = 1f; // Radius of the cylinder
    private const float length = 10f; // Length of the cylinder

    void Start()
    {
        // Ensure the smoke effect starts within the bottom half of the cylinder
        transform.position = RandomPointInLowerHalfCylinder();

        // Optionally, adjust the scale or emission rate to fit the environment
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            var shape = particleSystem.shape;
            shape.radius = radius;
        }
    }

    void Update()
    {
        // Ensure the smoke effect stays within the bounds of the bottom half of the cylinder
        Vector3 position = transform.position;

        // Clamp the position to stay within the bottom half of the cylinder
        position.x = Mathf.Clamp(position.x, -length / 2, length / 2);
        position.z = Mathf.Clamp(position.z, -radius, radius);
        position.y = Mathf.Clamp(position.y, -radius, 0); // Restrict y to negative values (bottom half)

        transform.position = position;
    }

    // Generate a random point within the lower half of the cylinder
    Vector3 RandomPointInLowerHalfCylinder()
    {
        float angle = Random.Range(0f, Mathf.PI * 2);
        float distance = Random.Range(0f, radius);
        float y = -distance * Mathf.Cos(angle); // Negative y for lower half
        float z = distance * Mathf.Sin(angle);
        float x = Random.Range(-length / 2, length / 2);
        return new Vector3(x, y, z);
    }
}
