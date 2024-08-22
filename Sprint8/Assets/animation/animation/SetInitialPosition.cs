using System.Numerics;
using UnityEngine;

public class SetInitialPosition : MonoBehaviour
{
    public UnityEngine.Vector3 initialPosition; // The position where you want the animation to start

    void Start()
    {
        // Set the initial position of the model
        transform.position = initialPosition;
    }
}
