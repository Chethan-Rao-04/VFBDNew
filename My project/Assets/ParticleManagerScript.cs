using UnityEngine;
using System.Collections;

public class ParticleAndModelManager : MonoBehaviour
{
    private const int numberOfParticles = 200;
    private const float radius = 0.4f;
    private const float length = 3.7f;
    private const float horizontalMovementRange = 0.02f;  // Range for initial random horizontal movement
    private const float randomHorizontalFactor = 0.001f;  // Factor for continuous random horizontal movement

    public GameObject particlePrefab;
    public GameObject smokePrefab;
    public GameObject model;  // Reference to the vibrating model
    public float particleFrequency = 20f;  // Frequency for particles
    public float modelFrequency = 20f;     // Frequency for the model

    public float particleAmplitude = 0.2f; // Amplitude for particles
    public float modelAmplitude = 0.002f;  // Amplitude for the model

    private GameObject[] particles;
    private Vector3[] initialPositions;
    private Vector3[] velocities;
    private Vector3 modelOriginalPosition;
    private float particleOscillationOffset;
    private float modelOscillationOffset;

    void Start()
    {
        particles = new GameObject[numberOfParticles];
        initialPositions = new Vector3[numberOfParticles];
        velocities = new Vector3[numberOfParticles];

        for (int i = 0; i < numberOfParticles; i++)
        {
            Vector3 randomPosition = RandomPointInUpperHalfCylinder();
            particles[i] = Instantiate(particlePrefab, randomPosition, Quaternion.identity, transform);

            // Store the initial position
            initialPositions[i] = randomPosition;

            // Reduce the size of the particle to a quarter
            particles[i].transform.localScale *= 0.25f;

            // Disable shadows for the particle
            Renderer particleRenderer = particles[i].GetComponent<Renderer>();
            if (particleRenderer != null)
            {
                particleRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                particleRenderer.receiveShadows = false;
            }

            // Initialize with small random horizontal velocity
            velocities[i] = new Vector3(
                Random.Range(-horizontalMovementRange, horizontalMovementRange),
                0,
                Random.Range(-horizontalMovementRange, horizontalMovementRange)
            );

            StartCoroutine(ChangeColorAfterTime(particles[i], 14f));
        }

        // Store the original position of the model
        if (model != null)
        {
            modelOriginalPosition = model.transform.localPosition;
        }

    }

    void Update()
    {
        // Calculate the oscillation offsets for particles and the model separately
        particleOscillationOffset = Mathf.Sin(Time.time * particleFrequency) * particleAmplitude;
        modelOscillationOffset = Mathf.Sin(Time.time * modelFrequency) * modelAmplitude;

        // Update particle positions
        for (int i = 0; i < numberOfParticles; i++)
        {
            Vector3 position = initialPositions[i];

            // Apply the vertical oscillation to the y-axis
            float randomVerticalOffset = Random.Range(-0.05f, 0.05f); // Add a little randomness to vertical movement
            position.y += particleOscillationOffset + randomVerticalOffset;

            // Update position with velocity (for random horizontal movement)
            position += velocities[i] * Time.deltaTime;

            // Apply a small random factor to horizontal movement
            position.x += Random.Range(-randomHorizontalFactor, randomHorizontalFactor);
            position.z += Random.Range(-randomHorizontalFactor, randomHorizontalFactor);

            // Check for collisions with other particles
            for (int j = 0; j < numberOfParticles; j++)
            {
                if (i != j)
                {
                    float distance = Vector3.Distance(position, particles[j].transform.position);
                    if (distance < 0.04f) // Approximate collision radius (0.04 to make it more responsive)
                    {
                        // Resolve collision by reflecting the velocities
                        Vector3 direction = (position - particles[j].transform.position).normalized;
                        velocities[i] = Vector3.Reflect(velocities[i], direction);
                        velocities[j] = Vector3.Reflect(velocities[j], -direction);

                        // Separate the particles slightly to avoid overlap
                        position += direction * (0.04f - distance);
                    }
                }
            }

            // Clamp the position within the cylinder
            position.x = Mathf.Clamp(position.x, -length / 2, length / 2);
            position.z = Mathf.Clamp(position.z, -radius, radius);
            position.y = Mathf.Clamp(position.y, 0, radius);

            particles[i].transform.position = position;

            // Slightly modify horizontal velocities over time for continuous random movement
            velocities[i] += new Vector3(
                Random.Range(-0.001f, 0.001f),
                0,
                Random.Range(-0.001f, 0.001f)
            );
        }

        // Apply the model's vertical oscillation
        if (model != null)
        {
            model.transform.localPosition = modelOriginalPosition + new Vector3(0, modelOscillationOffset, 0);
        }
    }

    Vector3 RandomPointInUpperHalfCylinder()
    {
        float angle = Random.Range(0f, Mathf.PI * 2);
        float distance = Random.Range(0f, radius);
        float y = distance * Mathf.Cos(angle);
        float z = distance * Mathf.Sin(angle);
        float x = Random.Range(-length / 2, length / 2);
        return new Vector3(x, Mathf.Abs(y), z);
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
