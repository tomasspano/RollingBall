using UnityEngine;

public class RotatingCylinder : MonoBehaviour
{
    [SerializeField] private float torqueForce;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.AddTorque(Vector3.up * torqueForce, ForceMode.VelocityChange);
    }
}
