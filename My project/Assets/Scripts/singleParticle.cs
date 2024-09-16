using UnityEngine;

public class SingleParticleAnimation : MonoBehaviour
{
    public float amplitude = 0.5f;       // Amplitude of oscillation
    public float frequency = 1f;         // Frequency of oscillation
    public float horizontalSpeed = 2f;   // Speed of movement to the right
    public float entrySpeed = 3f;        // Speed of particle entering the chamber
    public Vector3 inletPosition;        // Starting position at the inlet
    public Vector3 chamberEntryPosition; // Position where the particle starts oscillating
    public Color startColor = Color.white;
    public Color endColor = Color.red;
    public float duration = 5f;          // Duration for the particle to move across the chamber

    private float elapsedTime = 0f;      // Tracks time passed for color and movement
    private bool enteredChamber = false; // Tracks if particle has entered the chamber

    void Start()
    {
        // Set the particle at the inlet position
        transform.position = inletPosition;

        // Set the initial color of the particle
        GetComponent<Renderer>().material.color = startColor;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (!enteredChamber)
        {
            // Move the particle from the inlet to the chamber entry position
            transform.position = Vector3.MoveTowards(transform.position, chamberEntryPosition, entrySpeed * Time.deltaTime);

            // Check if the particle has reached the chamber entry point
            if (Vector3.Distance(transform.position, chamberEntryPosition) < 0.1f)
            {
                enteredChamber = true;  // Start oscillating once the particle has entered the chamber
            }
        }
        else
        {
            // Once inside the chamber, apply oscillating and horizontal movement
            float verticalOffset = Mathf.Sin(Time.time * frequency) * amplitude;

            // Move the particle to the right while oscillating vertically
            transform.position += new Vector3(horizontalSpeed * Time.deltaTime, verticalOffset, 0);

            // Linearly interpolate the color over time
            Color currentColor = Color.Lerp(startColor, endColor, elapsedTime / duration);
            GetComponent<Renderer>().material.color = currentColor;

            // If the particle reaches the outlet, destroy it or handle exit
            if (transform.position.x >= 10f)  // Assuming outlet is at x = 10
            {
                Destroy(gameObject);  // Or fade out, stop movement, etc.
            }
        }
    }
}
