using UnityEngine;

public class AudioSequence : MonoBehaviour
{
    public AudioSource audioSource;   // Single Audio Source for both clips
    public AudioClip audioClip1;      // First Audio Clip
    public AudioClip audioClip2;      // Second Audio Clip
    public float delay = 10f;         // Delay in seconds before playing the second audio clip

    private float originalVolume;     // To store the original volume of the audio source

    void Start()
    {
        // Check if the audio source and clips are assigned
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned in the inspector.");
            return;
        }
        
        if (audioClip1 == null || audioClip2 == null)
        {
            Debug.LogError("AudioClip1 or AudioClip2 is not assigned in the inspector.");
            return;
        }

        // Store the original volume
        originalVolume = audioSource.volume;

        // Play the first audio clip
        audioSource.clip = audioClip1;
        audioSource.Play();
        Debug.Log("First audio clip started playing.");

        // Schedule the second audio clip to play after the delay
        Invoke(nameof(PlaySecondAudio), delay);
    }

    void PlaySecondAudio()
    {
        // Pause or lower volume of the first audio clip
        audioSource.Pause(); // or audioSource.volume = 0f;
        Debug.Log("First audio clip paused.");

        // Play the second audio clip
        audioSource.clip = audioClip2;
        audioSource.Play();
        Debug.Log("Second audio clip started playing.");

        // Schedule to resume the first audio clip after the second audio clip finishes
        Invoke(nameof(ResumeFirstAudio), audioClip2.length);
    }

    void ResumeFirstAudio()
    {
        // Resume the first audio clip
        audioSource.clip = audioClip1;
        audioSource.Play();
        audioSource.volume = originalVolume; // Restore volume if changed
        Debug.Log("First audio clip resumed.");
    }
}
