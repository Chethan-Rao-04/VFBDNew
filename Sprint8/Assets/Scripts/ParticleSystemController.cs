using UnityEngine;
using UnityEngine.UI;

public class ParticleSystemController : MonoBehaviour
{
    public ParticleSystem particleSystemComponent;

    public Slider horizontalForceSlider;
    public Slider vibrationFrequencySlider;
    public Button playButton;

    // Define the bounds of the cylinder
    public Vector2 boundsMin = new Vector2(-0.3f, -0.7f);
    public Vector2 boundsMax = new Vector2(0.3f, 0.7f);

    private ParticleSystem.MainModule mainModule;
    private ParticleSystem.EmissionModule emissionModule;
    private ParticleSystem.Particle[] particles;

    private float startSpeed;
    private float vibrationFrequency;

    void Start()
    {
        if (particleSystemComponent == null)
        {
            particleSystemComponent = GetComponent<ParticleSystem>();
        }

        // Get the main and emission modules from the particle system instance
        mainModule = particleSystemComponent.main;
        emissionModule = particleSystemComponent.emission;

        int maxParticles = mainModule.maxParticles;
        particles = new ParticleSystem.Particle[maxParticles];

        // Set initial values of sliders and their ranges
        startSpeed = 4f;  // Initial start speed
        vibrationFrequency = 7f;  // Default value, adjust as needed

        horizontalForceSlider.minValue = 4f;
        horizontalForceSlider.maxValue = 10f;
        horizontalForceSlider.value = startSpeed;

        vibrationFrequencySlider.minValue = 1f;
        vibrationFrequencySlider.maxValue = 20f;
        vibrationFrequencySlider.value = vibrationFrequency;

        // Set initial start speed of particles
        mainModule.startSpeed = startSpeed;
        // Set initial start size to 0
        mainModule.startSize = 0f;

        // Add listeners to sliders
        horizontalForceSlider.onValueChanged.AddListener(ChangeStartSpeed);
        vibrationFrequencySlider.onValueChanged.AddListener(ChangeVibrationFrequency);

        // Add listener to play button
        playButton.onClick.AddListener(PlayParticles);

        Debug.Log("Initial setup completed");
    }

    void Update()
    {
        int numParticlesAlive = particleSystemComponent.GetParticles(particles);

        for (int i = 0; i < numParticlesAlive; i++)
        {
            // Calculate time offset based on particle index to stagger the wave
            float timeOffset = i * 0.1f; // Adjust multiplier for desired spacing
            Vector3 currentPosition = particles[i].position;

            // Apply sinusoidal (wave-like) motion with increased frequency
            float verticalMovement = Mathf.Sin((Time.time + timeOffset) * vibrationFrequency * 2f) * 0.2f; // Multiply frequency by 2 for increased frequency
            currentPosition.y += verticalMovement;

            // Calculate horizontal movement based on start speed
            float horizontalMovement = startSpeed * Time.deltaTime;
            currentPosition.x += horizontalMovement;

            // Constrain particles within the bounds of the cylinder
            if (currentPosition.x < boundsMin.x) currentPosition.x = boundsMin.x;
            if (currentPosition.x > boundsMax.x) currentPosition.x = boundsMax.x;
            if (currentPosition.y < boundsMin.y) currentPosition.y = boundsMin.y;
            if (currentPosition.y > boundsMax.y) currentPosition.y = boundsMax.y;

            particles[i].position = currentPosition;

            // Log each particle's position for debugging
            Debug.Log($"Particle {i} Position: {currentPosition}");
        }

        particleSystemComponent.SetParticles(particles, numParticlesAlive);
    }

    void ChangeStartSpeed(float value)
    {
        startSpeed = Mathf.Clamp(value, 4f, 10f);
        mainModule.startSpeed = startSpeed;
        Debug.Log("Start Speed changed to: " + startSpeed); // Debug statement to confirm the change
    }

    void ChangeVibrationFrequency(float value)
    {
        vibrationFrequency = value;
        Debug.Log("Vibration Frequency changed to: " + vibrationFrequency); // Debug statement to confirm the change
    }

    void PlayParticles()
    {
        particleSystemComponent.Play();
        emissionModule.enabled = true;
        mainModule.startSize = 2.5f;  // Set start size to 2.5 when playing
        Debug.Log("Particles are playing");
    }
}
