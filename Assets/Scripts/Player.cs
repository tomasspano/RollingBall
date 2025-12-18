using System;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector3 = UnityEngine.Vector3;

public class Player : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private Transform spawn;
    [SerializeField] float force;
    [SerializeField] float jumpForce;
    private Rigidbody rb;
    private Collider col;
    private bool isGrounded;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    void Update()
    {
        Jump();
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0,2.5f,0) * jumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        float h  = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        
        rb.AddForce(new Vector3(h, 0, v).normalized * force, ForceMode.Impulse);

        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
        else if (other.gameObject.CompareTag("Trap"))
        {
            transform.position = spawn.position;
            rb.AddForce(0, 0, 0, ForceMode.Impulse);
        }
        else if (other.gameObject.CompareTag("Victory"))
        {
            SceneManager.LoadScene(2);
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }
    }
}
