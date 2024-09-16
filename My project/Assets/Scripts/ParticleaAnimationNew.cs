using UnityEngine;

public class FluidizedBedParticleAnimato : MonoBehaviour
{
    public ParticleSystem particleSystemComponent;
    public float emissionRate = 10f;  // Particles per second
    public float speed = 5f;  // Base speed of particle movement
    public float vibrationAmplitude = 0.1f; // Amplitude of vertical vibration
    public float vibrationFrequency = 10f; // Frequency of vertical vibration (doubled)
    public float horizontalForce = 2f; // Horizontal force due to hot air

    // Define the bounds of the cylinder
    public Vector2 boundsMin = new Vector2(-0.5f, -1f);
    public Vector2 boundsMax = new Vector2(0.5f, 1f);

    // Colors for the first half and second half of the particle's lifetime
    public Color firstHalfColor = Color.blue; // Color for first half of lifetime
    public Color secondHalfColor = Color.red; // Color for second half of lifetime
    public Color thirdHalfColor = Color.yellow; // Color for second half of lifetime

    private ParticleSystem.EmissionModule emissionModule;
    private ParticleSystem.MainModule mainModule;
    private ParticleSystem.Particle[] particles;

    void Start()
    {
        if (particleSystemComponent == null)
        {
            particleSystemComponent = GetComponent<ParticleSystem>();
        }

        // Get the modules from the particle system instance
        emissionModule = particleSystemComponent.emission;
        mainModule = particleSystemComponent.main;

        int maxParticles = mainModule.maxParticles;
        particles = new ParticleSystem.Particle[maxParticles];
    }

    void Update()
    {
        int numParticlesAlive = particleSystemComponent.GetParticles(particles);

        for (int i = 0; i < numParticlesAlive; i++)
        {
            // Apply vibration effect (vertical oscillation)
            Vector3 currentPosition = particles[i].position;
            currentPosition.y += Mathf.Sin(Time.time * vibrationFrequency + i) * vibrationAmplitude;

            // Apply horizontal force (simulate hot air flow)
            currentPosition.x += horizontalForce * Time.deltaTime;

            // Constrain particles within the bounds of the cylinder
            if (currentPosition.x < boundsMin.x) currentPosition.x = boundsMin.x;
            if (currentPosition.x > boundsMax.x) currentPosition.x = boundsMax.x;
            if (currentPosition.y < boundsMin.y) currentPosition.y = boundsMin.y;
            if (currentPosition.y > boundsMax.y) currentPosition.y = boundsMax.y;

            particles[i].position = currentPosition;

            // Check the particle's lifetime to determine when to change color
            float lifeProgress = (particles[i].startLifetime - particles[i].remainingLifetime) / particles[i].startLifetime;

            if (lifeProgress <= 0.33f)
            {
                particles[i].startColor = firstHalfColor; // Color for the second half of lifetime
            }
            else if (lifeProgress >= 0.33f && lifeProgress <= 0.66f)
            {
                particles[i].startColor = secondHalfColor; // Color for the first half of lifetime
            }
            else
            {
                particles[i].startColor = thirdHalfColor;
            }
        }

        particleSystemComponent.SetParticles(particles, numParticlesAlive);
    }
}