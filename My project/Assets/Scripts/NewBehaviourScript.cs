using UnityEngine;
using UnityEngine.UI;

public class SliderValueDisplay : MonoBehaviour
{
    public Slider slider; // Reference to the Slider
    public Text valueText; // Reference to the Text

    void Start()
    {
        // Initial update
        UpdateValueText();

        // Add listener to call UpdateValueText when the slider value changes
        slider.onValueChanged.AddListener(delegate { UpdateValueText(); });
    }

    void UpdateValueText()
    {
        // Update the Text with the Slider's current value
        valueText.text = slider.value.ToString("0.00"); // Format as needed
    }
}
