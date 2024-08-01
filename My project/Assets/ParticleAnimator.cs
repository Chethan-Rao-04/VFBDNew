using UnityEngine;
using UnityEngine.UI;

public class FluidizedBedParticleAnimator : MonoBehaviour
{
    public ParticleSystem particleSystemComponent;
    public float emissionRate = 10f;  // Particles per second
    public float speed = 5f;  // Base speed of particle movement
    public float vibrationAmplitude = 0.1f; // Amplitude of vertical vibration
    public float vibrationFrequency = 5f; // Frequency of vertical vibration
    public float horizontalForce = 2f; // Horizontal force due to hot air

    // Define the bounds of the cylinder
    public Vector2 boundsMin = new Vector2(-0.5f, -1f);
    public Vector2 boundsMax = new Vector2(0.5f, 1f);

    // UI Sliders
    public Slider emissionRateSlider;
    public Slider speedSlider;
    public Slider vibrationAmplitudeSlider;
    public Slider vibrationFrequencySlider;
    public Slider horizontalForceSlider;

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

        // Set initial values of sliders
        emissionRateSlider.value = emissionRate;
        speedSlider.value = speed;
        vibrationAmplitudeSlider.value = vibrationAmplitude;
        vibrationFrequencySlider.value = vibrationFrequency;
        horizontalForceSlider.value = horizontalForce;

        // Add listeners to sliders
        emissionRateSlider.onValueChanged.AddListener(OnEmissionRateChanged);
        speedSlider.onValueChanged.AddListener(OnSpeedChanged);
        vibrationAmplitudeSlider.onValueChanged.AddListener(OnVibrationAmplitudeChanged);
        vibrationFrequencySlider.onValueChanged.AddListener(OnVibrationFrequencyChanged);
        horizontalForceSlider.onValueChanged.AddListener(OnHorizontalForceChanged);
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
        }

        particleSystemComponent.SetParticles(particles, numParticlesAlive);
    }

    void OnEmissionRateChanged(float value)
    {
        emissionRate = value;
        emissionModule.rateOverTime = emissionRate;
    }

    void OnSpeedChanged(float value)
    {
        speed = value;
    }

    void OnVibrationAmplitudeChanged(float value)
    {
        vibrationAmplitude = value;
    }

    void OnVibrationFrequencyChanged(float value)
    {
        vibrationFrequency = value;
    }

    void OnHorizontalForceChanged(float value)
    {
        horizontalForce = value;
    }
}
