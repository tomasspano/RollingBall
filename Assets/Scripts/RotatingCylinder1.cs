using UnityEngine;

/// <summary>
/// Cylinder that spins continuously via torque.
/// Implements ITrap — Activate() is where the spin starts.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class RotatingCylinder : MonoBehaviour, ITrap
{
    [SerializeField] private float torqueForce = 5f;

    private Rigidbody rb;

    // ------------------------------------------------------------------ ITrap
    public void Activate()
    {
        rb.isKinematic = false;
    }

    public void OnTrapTriggered(GameObject player)
    {
        // Contact sends the ball flying — no extra code needed,
        // physics handles the deflection naturally.
    }

    // ------------------------------------------------------------------ unity
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Activate();
    }

    private void FixedUpdate()
    {
        rb.AddTorque(Vector3.up * torqueForce, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
            OnTrapTriggered(other.gameObject);
    }
}
