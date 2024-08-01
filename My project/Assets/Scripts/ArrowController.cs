using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public GameObject arrow; // Reference to the arrow GameObject
    public Transform blowerTransform;
    public Transform heaterTransform;
    public Transform chamberTransform;
    public Transform separatorTransform;
    public Transform fanTransform;
    public Transform filterTransform;

    void Start()
    {
        arrow.SetActive(false); // Ensure the arrow is inactive at the start
    }

    public void ShowArrowAtBlower()
    {
        ShowArrowAtComponent(blowerTransform);
    }

    public void ShowArrowAtHeater()
    {
        ShowArrowAtComponent(heaterTransform);
    }

    public void ShowArrowAtChamber()
    {
        ShowArrowAtComponent(chamberTransform);
    }

    public void ShowArrowAtSeparator()
    {
        ShowArrowAtComponent(separatorTransform);
    }

    public void ShowArrowAtFan()
    {
        ShowArrowAtComponent(fanTransform);
    }

    public void ShowArrowAtFilter()
    {
        ShowArrowAtComponent(filterTransform);
    }

    private void ShowArrowAtComponent(Transform componentTransform)
    {
        arrow.SetActive(true);
        arrow.transform.position = componentTransform.position;
        arrow.transform.rotation = componentTransform.rotation;
        // Adjust the arrow's local position/rotation if needed
    }

    public void HideArrow()
    {
        arrow.SetActive(false);
    }
}
