using UnityEngine;

public class ParticlePath : MonoBehaviour
{
    public ParticleSystem particleSystemComponent;
    public WaypointPath waypointPath;
    public float speed = 5f;

    private ParticleSystem.Particle[] particles;
    private Vector3[] pathPositions;

    void Start()
    {
        // Get the ParticleSystem component if not assigned
        if (particleSystemComponent == null)
        {
            particleSystemComponent = GetComponent<ParticleSystem>();
        }

        // Check if WaypointPath reference is missing
        if (waypointPath == null || waypointPath.waypoints.Length == 0)
        {
            Debug.LogError("WaypointPath reference is missing or waypoints are not defined.");
            return;
        }

        // Initialize arrays
        int maxParticles = particleSystemComponent.main.maxParticles;
        particles = new ParticleSystem.Particle[maxParticles];
        pathPositions = new Vector3[waypointPath.waypoints.Length];

        // Populate pathPositions with waypoint positions
        for (int i = 0; i < waypointPath.waypoints.Length; i++)
        {
            pathPositions[i] = waypointPath.waypoints[i].position;
        }
    }

    void LateUpdate()
    {
        if (particles == null || particles.Length == 0)
        {
            return;
        }

        int numParticlesAlive = particleSystemComponent.GetParticles(particles);

        for (int i = 0; i < numParticlesAlive; i++)
        {
            particles[i].position = MoveAlongPath(particles[i].position, i);
        }

        particleSystemComponent.SetParticles(particles, numParticlesAlive);
    }

    Vector3 MoveAlongPath(Vector3 currentPosition, int particleIndex)
    {
        float distance = speed * Time.deltaTime;

        // Ensure pathPositions has at least 2 waypoints
        if (pathPositions.Length < 2)
        {
            Debug.LogError("Not enough waypoints defined in WaypointPath.");
            return currentPosition;
        }

        for (int i = 0; i < pathPositions.Length - 1; i++)
        {
            Vector3 segmentStart = pathPositions[i];
            Vector3 segmentEnd = pathPositions[i + 1];
            float segmentLength = Vector3.Distance(segmentStart, segmentEnd);

            // Check if currentPosition is between segmentStart and segmentEnd
            if (Vector3.Distance(currentPosition, segmentStart) < segmentLength)
            {
                return Vector3.MoveTowards(currentPosition, segmentEnd, distance);
            }
        }

        // If currentPosition is at the last waypoint, return currentPosition
        return currentPosition;
    }
}
