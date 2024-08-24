using UnityEngine;
using System.Collections;

public class ParticleManager : MonoBehaviour
{
    // Hardcoded values
    private const int numberOfParticles = 100; // Increased number of particles
    private const float radius = 0.4f; // Radius of the cylinder
    private const float length = 4f; // Length of the cylinder

    public GameObject particlePrefab;
    public GameObject smokePrefab; // Reference to the smoke prefab
    private GameObject[] particles;
    private Vector3[] velocities;

    void Start()
    {
        particles = new GameObject[numberOfParticles];
        velocities = new Vector3[numberOfParticles];

        for (int i = 0; i < numberOfParticles; i++)
        {
            Vector3 randomPosition = RandomPointInUpperHalfCylinder();
            particles[i] = Instantiate(particlePrefab, randomPosition, Quaternion.identity, transform);

            // Reduce the size of the particle by half
            particles[i].transform.localScale *= 0.5f;

            // Increase vertical motion to a higher value for faster oscillation
            velocities[i] = new Vector3(
                Random.Range(-0.1f, 0.1f),
                Random.Range(2.0f, 4.0f),
                Random.Range(-0.1f, 0.1f)
            );

            // Update delay to double the previous value
            StartCoroutine(ChangeColorAfterTime(particles[i], 14f)); // Delay of 14 seconds
        }

        // Add the smoke effect
        AddSmokeEffect();
    }

    void Update()
    {
        for (int i = 0; i < numberOfParticles; i++)
        {
            Vector3 position = particles[i].transform.position;
            Vector3 velocity = velocities[i];

            position += velocity * Time.deltaTime;

            // Check for collisions with the cylindrical boundary in 2D (y and z axis)
            float distanceFromCenter = Mathf.Sqrt(position.y * position.y + position.z * position.z);
            if (distanceFromCenter > radius)
            {
                // Reflect the velocity in the yz-plane
                Vector3 normal = new Vector3(0, position.y, position.z).normalized;
                velocity = Vector3.Reflect(velocity, normal);
                position += velocity * Time.deltaTime;
            }

            // Check for collisions with the cylinder's length boundaries (x axis)
            if (position.x < -length / 2 || position.x > length / 2)
            {
                velocity.x = -velocity.x;
                position.x = Mathf.Clamp(position.x, -length / 2, length / 2);
            }

            // Ensure the particle stays within the upper half of the cylinder's height (y > 0)
            if (position.y < 0)
            {
                velocity.y = -velocity.y;
                position.y = Mathf.Clamp(position.y, 0, radius);
            }

            // Clamp the position to stay within the bounds of the cylinder
            position.x = Mathf.Clamp(position.x, -length / 2, length / 2);
            position.z = Mathf.Clamp(position.z, -radius, radius);
            position.y = Mathf.Clamp(position.y, 0, Mathf.Sqrt(radius * radius - position.z * position.z));

            particles[i].transform.position = position;
            velocities[i] = velocity;
        }
    }

    // Generate a random point within the upper half of the cylinder
    Vector3 RandomPointInUpperHalfCylinder()
    {
        float angle = Random.Range(0f, Mathf.PI * 2);
        float distance = Random.Range(0f, radius);
        float y = distance * Mathf.Cos(angle);
        float z = distance * Mathf.Sin(angle);
        float x = Random.Range(-length / 2, length / 2);
        return new Vector3(x, Mathf.Abs(y), z); // Ensure y is positive to stay in the upper half
    }

    IEnumerator ChangeColorAfterTime(GameObject particle, float delay)
    {
        yield return new WaitForSeconds(delay);
        Renderer renderer = particle.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.red;
        }
    }

    void AddSmokeEffect()
    {
        // Instantiate the smoke effect at the bottom of the cylinder
        GameObject smoke = Instantiate(smokePrefab, new Vector3(0, -length / 2, 0), Quaternion.identity, transform);

        // Adjust the scale of the smoke effect to fit the radius of the cylinder
        float cylinderDiameter = radius * 2;
        smoke.transform.localScale = new Vector3(cylinderDiameter, 1, cylinderDiameter); // Adjust scale
    }
}
