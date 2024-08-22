using UnityEngine;
using UnityEngine.UI;

public class DrynessCalculator : MonoBehaviour
{
    public Slider airFlowVelocitySlider;
    public Slider airFlowTemperatureSlider;
    public Slider residenceTimeSlider;
    public Slider vibrationalIntensitySlider;
    public Slider feedQuantitySlider;

    public Text airFlowVelocityText;
    public Text airFlowTemperatureText;
    public Text residenceTimeText;
    public Text vibrationalIntensityText;
    public Text feedQuantityText;

    public Text resultText;

    void Start()
    {
        // Initialize slider values and add listeners
        UpdateTextFields();

        airFlowVelocitySlider.onValueChanged.AddListener(delegate { UpdateTextFields(); });
        airFlowTemperatureSlider.onValueChanged.AddListener(delegate { UpdateTextFields(); });
        residenceTimeSlider.onValueChanged.AddListener(delegate { UpdateTextFields(); });
        vibrationalIntensitySlider.onValueChanged.AddListener(delegate { UpdateTextFields(); });
        feedQuantitySlider.onValueChanged.AddListener(delegate { UpdateTextFields(); });
    }

    void UpdateTextFields()
    {
        airFlowVelocityText.text =airFlowVelocitySlider.value.ToString("0.00");
        airFlowTemperatureText.text = airFlowTemperatureSlider.value.ToString("0.00");
        residenceTimeText.text = residenceTimeSlider.value.ToString("0.00");
        vibrationalIntensityText.text =vibrationalIntensitySlider.value.ToString("0.00");
        feedQuantityText.text =feedQuantitySlider.value.ToString("0.00");
    }

    public void CalculateDrynessFraction()
    {
        float airFlowVelocity = airFlowVelocitySlider.value;
        float airFlowTemperature = airFlowTemperatureSlider.value;
        float residenceTime = residenceTimeSlider.value;
        float vibrationalIntensity = vibrationalIntensitySlider.value;
        float feedQuantity = feedQuantitySlider.value;

        // Example calculation for dryness fraction (replace with your actual formula)
        float drynessFraction = (airFlowVelocity + airFlowTemperature + residenceTime + vibrationalIntensity + feedQuantity) / 5.0f;

        resultText.text = drynessFraction.ToString("0.00");
    }
}
