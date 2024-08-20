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

    public Text moistureContentResultText;
    public Text dryingTimeResultText;

    // Assuming some necessary constants for calculations
    public float bedVolume = 1.0f; // Volume of the bed in cubic meters
    public float bulkDensity = 600.0f; // Bulk density in kg/m^3
    public float moistureEvaporationRate = 0.1f; // Evaporation rate (ΔMw) in kg/s

    // Other parameters like mass transfer coefficient (KC), particle radius, etc. can be added as needed

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
        airFlowVelocityText.text = airFlowVelocitySlider.value.ToString("0.00");
        airFlowTemperatureText.text = airFlowTemperatureSlider.value.ToString("0.00");
        residenceTimeText.text = residenceTimeSlider.value.ToString("0.00");
        vibrationalIntensityText.text = vibrationalIntensitySlider.value.ToString("0.00");
        feedQuantityText.text = feedQuantitySlider.value.ToString("0.00");
    }

    public void CalculateDryingParameters()
    {
        float airFlowVelocity = airFlowVelocitySlider.value;
        float airFlowTemperature = airFlowTemperatureSlider.value;
        float residenceTime = residenceTimeSlider.value;
        float vibrationalIntensity = vibrationalIntensitySlider.value;
        float feedQuantity = feedQuantitySlider.value;

        // Example calculation for moisture evaporation rate (ΔMw)
        float deltaMw = moistureEvaporationRate; // This could be a function of the above parameters

        // Example calculation for product moisture content (Xout)
        float mwOut = deltaMw; // Example value, adjust this based on your exact calculation
        float ms = 1.0f; // Example value, replace with actual solid mass
        float xOut = mwOut / ms;

        // Example calculation for drying time (t)
        float dryingTime = (bedVolume * bulkDensity) / feedQuantity;

        // Update the UI with the results
        moistureContentResultText.text = xOut.ToString("0.00");
        dryingTimeResultText.text = dryingTime.ToString("0.00");
    }
}
