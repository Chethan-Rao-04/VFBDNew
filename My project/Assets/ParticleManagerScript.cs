using UnityEngine;
using System.Collections;

public class ParticleManager : MonoBehaviour
{
    // Hardcoded values
    private const int numberOfParticles = 40; // Number of particles
    private const float radius = 1f; // Radius of the cylinder
    private const float length = 6f; // Length of the cylinder

    public GameObject particlePrefab;
    private GameObject[] particles;
    private Vector3[] velocities;

    void Start()
    {
        particles = new GameObject[numberOfParticles];
        velocities = new Vector3[numberOfParticles];

        for (int i = 0; i < numberOfParticles; i++)
        {
            Vector3 randomPosition = RandomPointInCylinder();
            particles[i] = Instantiate(particlePrefab, randomPosition, Quaternion.identity, transform);
            velocities[i] = Random.insideUnitSphere * 0.1f;
            StartCoroutine(ChangeColorAfterTime(particles[i], 5f));
        }
    }

    void Update()
    {
        for (int i = 0; i < numberOfParticles; i++)
        {
            Vector3 position = particles[i].transform.position;
            Vector3 velocity = velocities[i];

            position += velocity;

            // Check for collisions with the cylindrical boundary in 2D
            float distanceFromCenter = Mathf.Sqrt(position.y * position.y + position.z * position.z);
            if (distanceFromCenter > radius)
            {
                // Reflect the velocity in the yz-plane
                Vector3 normal = new Vector3(0, position.y, position.z).normalized;
                velocity = Vector3.Reflect(velocity, normal);
                position = particles[i].transform.position + velocity;
            }

            // Check for collisions with the cylinder's length boundaries
            if (position.x < -length / 2 || position.x > length / 2)
            {
                velocity.x = -velocity.x;
                position = particles[i].transform.position + velocity;
            }

            // Clamp the position to stay within the bounds
            position.x = Mathf.Clamp(position.x, -length / 2, length / 2);
            float newDistanceFromCenter = Mathf.Sqrt(position.y * position.y + position.z * position.z);
            if (newDistanceFromCenter > radius)
            {
                float scale = radius / newDistanceFromCenter;
                position.y *= scale;
                position.z *= scale;
            }

            particles[i].transform.position = position;
            velocities[i] = velocity;
        }
    }

    Vector3 RandomPointInCylinder()
    {
        float angle = Random.Range(0f, Mathf.PI * 2);
        float distance = Random.Range(0f, radius);
        float y = distance * Mathf.Cos(angle);
        float z = distance * Mathf.Sin(angle);
        float x = Random.Range(-length / 2, length / 2);
        return new Vector3(x, y, z);
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
}