using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FalsePlatform : MonoBehaviour, ITrap
{
    [SerializeField] private float fallDelay = 0.18f; 

    private Rigidbody rb;
    private bool triggered = false;

    public void Activate() {}

    public void OnTrapTriggered(GameObject player)
    {
        if (triggered) return;
        triggered = true;

        //invoco el método con un delay personalizado
        Invoke(nameof(Drop), fallDelay);
    }

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

    private void Drop()
    {
        rb.isKinematic = false;
    }
}
