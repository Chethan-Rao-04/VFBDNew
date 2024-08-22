using UnityEngine;

public class ApplyTransparentMaterial : MonoBehaviour
{
    public Color baseColor = new Color(0.7f, 0.7f, 0.7f, 0.1f); // Adjust RGB values as needed and decrease alpha for more transparency
    public Color emissionColor = Color.white; // Emission color for brighter appearance
    public float emissionIntensity = 1.0f; // Emission intensity if using emission
    public Texture2D emissionTexture; // Optional: Emission texture for varied emission pattern

    void Start()
    {
        // Ensure the renderer component is found
        Renderer renderer = GetComponent<Renderer>();
        if (renderer == null)
        {
            Debug.LogError("Renderer component not found.");
            return;
        }

        // Create a new material based on the existing material or a new one if none assigned
        Material material = renderer.material;
        if (material == null)
        {
            material = new Material(Shader.Find("Standard")); // Change shader as needed
        }

        // Configure the material properties
        material.color = baseColor;
        material.SetFloat("_Mode", 3); // Set to Transparent rendering mode
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 3000; // Transparent queue

        // Apply emission properties for visibility of particles
        material.EnableKeyword("_EMISSION");
        material.SetColor("_EmissionColor", emissionColor * emissionIntensity);

        // Assign the material to the renderer
        renderer.material = material;
    }
}
