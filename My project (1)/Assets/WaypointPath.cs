using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    public Transform[] waypoints;

    void Awake()
    {
        // Automatically populate the waypoints array with the child transforms
        waypoints = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }
    }
}
