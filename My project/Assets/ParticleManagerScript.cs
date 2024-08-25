using UnityEngine;
using System.Collections;

public class ParticleManager : MonoBehaviour
{

    private const int numberOfParticles = 200; 
    private const float radius = 0.4f;
    private const float length = 3.7f;

    public GameObject particlePrefab;
    public GameObject smokePrefab;
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

            // Reduce the size of the particle to a quarter
            particles[i].transform.localScale *= 0.25f;

            // Disable shadows for the particle
            Renderer particleRenderer = particles[i].GetComponent<Renderer>();
            if (particleRenderer != null)
            {
                particleRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                particleRenderer.receiveShadows = false;
            }

            // Increase vertical motion to a higher value for faster oscillation
            velocities[i] = new Vector3(
                Random.Range(-0.1f, 0.1f),
                Random.Range(2.0f, 4.0f),
                Random.Range(-0.1f, 0.1f)
            );

            StartCoroutine(ChangeColorAfterTime(particles[i], 14f));
        }

        AddSmokeEffect();
    }

    void Update()
    {
        for (int i = 0; i < numberOfParticles; i++)
        {
            Vector3 position = particles[i].transform.position;
            Vector3 velocity = velocities[i];

            position += velocity * Time.deltaTime;

            float distanceFromCenter = Mathf.Sqrt(position.y * position.y + position.z * position.z);
            if (distanceFromCenter > radius)
            {
                Vector3 normal = new Vector3(0, position.y, position.z).normalized;
                velocity = Vector3.Reflect(velocity, normal);
                position += velocity * Time.deltaTime;
            }

            if (position.x < -length / 2 || position.x > length / 2)
            {
                velocity.x = -velocity.x;
                position.x = Mathf.Clamp(position.x, -length / 2, length / 2);
            }

            if (position.y < 0)
            {
                velocity.y = -velocity.y;
                position.y = Mathf.Clamp(position.y, 0, radius);
            }

            position.x = Mathf.Clamp(position.x, -length / 2, length / 2);
            position.z = Mathf.Clamp(position.z, -radius, radius);
            position.y = Mathf.Clamp(position.y, 0, Mathf.Sqrt(radius * radius - position.z * position.z));

            particles[i].transform.position = position;
            velocities[i] = velocity;
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

    void AddSmokeEffect()
    {
        GameObject smoke = Instantiate(smokePrefab, new Vector3(0, -length / 2, 0), Quaternion.identity, transform);

        float cylinderDiameter = radius * 2;
        smoke.transform.localScale = new Vector3(cylinderDiameter, 1, cylinderDiameter);
    }
}
