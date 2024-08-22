using UnityEngine;
using UnityEngine.EventSystems;

public class HoverScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject npcImage; // Reference to the NPC image you want to show/hide

    // Method called when the pointer enters the object's area
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Show the NPC image
        npcImage.SetActive(true);
    }

    // Method called when the pointer exits the object's area
    public void OnPointerExit(PointerEventData eventData)
    {
        // Hide the NPC image
        npcImage.SetActive(false);
    }
}
