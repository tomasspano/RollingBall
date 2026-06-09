using UnityEngine;

/// <summary>
/// False platform: stays kinematic until the player steps on it, then falls.
/// Implements ITrap — Activate() is a no-op here since the trap reacts on contact.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class FalsePlatform : MonoBehaviour, ITrap
{
    [SerializeField] private float fallDelay = 0.18f; // tiny delay before dropping, feels more intentional

    private Rigidbody rb;
    private bool triggered = false;

    // ------------------------------------------------------------------ ITrap
    public void Activate()
    {
        // Platform is always ready — nothing to do on start.
    }

    public void OnTrapTriggered(GameObject player)
    {
        if (triggered) return;
        triggered = true;

        // Small delay so the player has a sliver of time to jump off.
        Invoke(nameof(Drop), fallDelay);
    }

    // ------------------------------------------------------------------ unity
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        Activate();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
            OnTrapTriggered(other.gameObject);
    }

    // ------------------------------------------------------------------ helpers
    private void Drop()
    {
        rb.isKinematic = false;
    }
}
