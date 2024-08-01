using UnityEngine;

public class ParticleMovement : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Particle Movement script started.");
    }

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime);
    }
}
