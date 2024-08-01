using UnityEngine;

public class ImageHandler : MonoBehaviour
{
    public GameObject image1; // Reference to Image1 GameObject
    public GameObject image2; // Reference to Image2 GameObject
    public GameObject image3; // Reference to Image3 GameObject
    public GameObject image4; // Reference to Image4 GameObject
    public GameObject image5; // Reference to Image5 GameObject
    public GameObject image6; // Reference to Image6 GameObject

    public AudioClip audio1; // Reference to AudioClip for Image1
    public AudioClip audio2; // Reference to AudioClip for Image2
    public AudioClip audio3; // Reference to AudioClip for Image3
    public AudioClip audio4; // Reference to AudioClip for Image4
    public AudioClip audio5; // Reference to AudioClip for Image5
    public AudioClip audio6; // Reference to AudioClip for Image6

    private AudioSource audioSource;

    void Start()
    {
        // Start with all images deactivated
        DeactivateAllImages();

        // Get or add an AudioSource component
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void DeactivateAllImages()
    {
        image1.SetActive(false);
        image2.SetActive(false);
        image3.SetActive(false);
        image4.SetActive(false);
        image5.SetActive(false);
        image6.SetActive(false);
    }

    public void ShowImage1()
    {
        DeactivateAllImages();
        image1.SetActive(true); // Show Image1
        PlayAudio(audio1); // Play audio1
    }

    public void ShowImage2()
    {
        DeactivateAllImages();
        image2.SetActive(true); // Show Image2
        PlayAudio(audio2); // Play audio2
    }

    public void ShowImage3()
    {
        DeactivateAllImages();
        image3.SetActive(true); // Show Image3
        PlayAudio(audio3); // Play audio3
    }

    public void ShowImage4()
    {
        DeactivateAllImages();
        image4.SetActive(true); // Show Image4
        PlayAudio(audio4); // Play audio4
    }

    public void ShowImage5()
    {
        DeactivateAllImages();
        image5.SetActive(true); // Show Image5
        PlayAudio(audio5); // Play audio5
    }

    public void ShowImage6()
    {
        DeactivateAllImages();
        image6.SetActive(true); // Show Image6
        PlayAudio(audio6); // Play audio6
    }

    private void PlayAudio(AudioClip clip)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        audioSource.clip = clip;
        audioSource.Play();
    }
}
