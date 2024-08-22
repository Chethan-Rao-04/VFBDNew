using UnityEngine;
using UnityEngine.UI;

public class ParticleControllerWithUI : MonoBehaviour
{
    public ParticleSystem particleSystemController;
    public Slider velocitySlider;
    public Slider sizeSlider;
    public Text velocityValueText;
    public Text sizeValueText;

    private ParticleSystem.MainModule mainModule;
    private ParticleSystem.VelocityOverLifetimeModule velocityOverLifetime;

    void Start()
    {
        Debug.Log("Start method called");

        if (particleSystemController == null)
        {
            particleSystemController = GetComponent<ParticleSystem>();
        }

        mainModule = particleSystemController.main;
        velocityOverLifetime = particleSystemController.velocityOverLifetime;

        ConfigureVelocityOverLifetime();

        velocitySlider.onValueChanged.AddListener(OnVelocityChanged);
        sizeSlider.onValueChanged.AddListener(OnSizeChanged);

        velocitySlider.value = 2.0f;
        sizeSlider.value = 0.5f;
        velocityValueText.text = "Velocity: " + velocitySlider.value.ToString("0.00");
        sizeValueText.text = "Size: " + sizeSlider.value.ToString("0.00");
    }

    void ConfigureVelocityOverLifetime()
    {
        Debug.Log("Configuring velocity over lifetime");

        velocityOverLifetime.enabled = true;

        AnimationCurve xCurve = new AnimationCurve();
        xCurve.AddKey(0.0f, 0.0f);
        xCurve.AddKey(1.0f, 2.0f);
        velocityOverLifetime.x = new ParticleSystem.MinMaxCurve(1.0f, xCurve);

        AnimationCurve yCurve = new AnimationCurve();
        yCurve.AddKey(0.0f, 0.0f);
        yCurve.AddKey(0.25f, 1.0f);
        yCurve.AddKey(0.5f, 0.0f);
        yCurve.AddKey(0.75f, -1.0f);
        yCurve.AddKey(1.0f, 0.0f);
        velocityOverLifetime.y = new ParticleSystem.MinMaxCurve(1.0f, yCurve);

        velocityOverLifetime.z = new ParticleSystem.MinMaxCurve(2.0f);
    }

    void OnVelocityChanged(float value)
    {
        Debug.Log("Velocity changed to: " + value);

        velocityOverLifetime.z = new ParticleSystem.MinMaxCurve(value);
        velocityValueText.text = "Velocity: " + value.ToString("0.00");
    }

    void OnSizeChanged(float value)
    {
        Debug.Log("Size changed to: " + value);

        mainModule.startSize = value;
        sizeValueText.text = "Size: " + value.ToString("0.00");
    }
}
