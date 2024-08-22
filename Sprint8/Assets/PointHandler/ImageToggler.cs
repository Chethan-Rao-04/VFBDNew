using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageToggle : MonoBehaviour
{
    public GameObject npcImage; // Reference to the NPC image you want to show/hide

    // Method to toggle the visibility of the NPC image
    public void ToggleNPCImageVisibility()
    {
        // Toggle the active state of the NPC image
        npcImage.SetActive(!npcImage.activeSelf);
    }
}
