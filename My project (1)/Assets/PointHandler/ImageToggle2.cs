using UnityEngine;
using UnityEngine.UI;

public class ImageToggle2 : MonoBehaviour
{
    public GameObject image1; // Reference to the first image
    public GameObject image2; // Reference to the second image

    // Method to show the first image and hide the second image
    public void ShowImage1()
    {
        image1.SetActive(true);
        image2.SetActive(false);
    }

    // Method to show the second image and hide the first image
    public void ShowImage2()
    {
        image1.SetActive(false);
        image2.SetActive(true);
    }
}

