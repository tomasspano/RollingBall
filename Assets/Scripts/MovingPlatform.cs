using UnityEngine;

/// <summary>
/// Moving platform that bounces back and forth along a direction.
/// Implements ITrap — Activate() kicks off the movement.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class MovingPlatform : MonoBehaviour, ITrap
{
    [SerializeField] private float   speed           = 3f;
    [SerializeField] private Vector3 movementDirection = Vector3.right;
    [SerializeField] private float   timeToMove      = 2f;

    private Rigidbody rb;
    private float     timer;

    // ------------------------------------------------------------------ ITrap
    public void Activate()
    {
        rb.isKinematic  = true; // kinematic so physics doesn't mess with the path
        rb.linearVelocity = movementDirection.normalized * speed;
    }

    public void OnTrapTriggered(GameObject player)
    {
        // Moving platform doesn't harm the player directly.
        // The danger is falling off — no extra logic needed.
    }

    // ------------------------------------------------------------------ unity
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Activate();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToMove)
        {
            movementDirection  = -movementDirection.normalized;
            rb.linearVelocity  = movementDirection * speed;
            timer = 0f;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
            OnTrapTriggered(other.gameObject);
    }
}
