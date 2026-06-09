using UnityEngine;

public class MovingPlatform : MonoBehaviour, ITrap
{
    [SerializeField] private float   speed = 3f;
    [SerializeField] private Vector3 movementDirection;
    [SerializeField] private float   timeToMove = 2f;

    private Rigidbody rb;
    private float     timer;

    public void Activate()
    {
        rb.linearVelocity = movementDirection.normalized * speed;
    }

    public void OnTrapTriggered(GameObject player) {}

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

}
