using UnityEngine;

/// <summary>
/// Trap that spins on its Y axis and kills the player on contact.
/// Implements ITrap — Activate() starts the spin.
/// </summary>
public class SpinningTrap : MonoBehaviour, ITrap
{
    [SerializeField] private float rotationSpeed = 120f;

    // ------------------------------------------------------------------ ITrap
    public void Activate()
    {
        // Spinning immediately — nothing extra needed.
    }

    public void OnTrapTriggered(GameObject player)
    {
        GameManager.Instance?.PlayTrapHitSFX();
        // Respawn is handled inside Player.OnCollisionEnter via the "Trap" tag.
        // This hook exists for any extra feedback you want (particles, screen shake, etc).
    }

    // ------------------------------------------------------------------ unity
    private void Start() => Activate();

    private void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
            OnTrapTriggered(other.gameObject);
    }
}
