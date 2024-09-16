using UnityEngine;

public class AudioSequence : MonoBehaviour
{
    public AudioSource audioSource;   // Single Audio Source for the clip
    public AudioClip audioClip2;      // The only Audio Clip that will play

    void Start()
    {
        // Check if the audio source and clip are assigned
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned in the inspector.");
            return;
        }

        if (audioClip2 == null)
        {
            Debug.LogError("AudioClip2 is not assigned in the inspector.");
            return;
        }

        // Play the second (and only) audio clip
        audioSource.clip = audioClip2;
        audioSource.Play();
        Debug.Log("Audio clip started playing.");
    }
}
