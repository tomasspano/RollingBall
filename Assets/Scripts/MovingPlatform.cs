using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private Vector3 movementDirection;
    [SerializeField] private float timeToMove;
    private float timer;
    
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = movementDirection.normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToMove)
        {
            movementDirection = -movementDirection.normalized;
            rb.linearVelocity = movementDirection.normalized * speed;
            timer = 0;
        }
    }
}
