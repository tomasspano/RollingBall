using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float maxSpeed   = 8f;
    [SerializeField] private float force      = 12f;
    [SerializeField] private float jumpForce  = 6f;

    [SerializeField] private float movementSmoothing = 0.15f;

    [Header("Respawn")]
    [SerializeField] private Transform spawn;

    private Rigidbody  rb;
    private Collider   col;
    private bool       isGrounded;

    //oculto en editor para que solo las trampas lo cambien
    [HideInInspector] public float speedMultiplier = 1f;

    private void Awake()
    {
        rb  = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    private void Update()
    {
        HandleJump();
    }
    
    //fixed update porque son físicas
    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            SoundManager.Instance?.PlayJump();
        }
    }

    private void HandleMovement()
    {
        //esta parte devuelve entre -1 y 1 para dirección
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 inputDir = new Vector3(h, 0f, v).normalized;

        //este es el modificador de las SizeTraps
        float scaledMax   = maxSpeed  * speedMultiplier;
        float scaledForce = force     * speedMultiplier;

        //ignoramos la y para que el salto no afecte
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (flatVel.magnitude < scaledMax) //dejo de acelerar si llego al máximo
            rb.AddForce(inputDir * scaledForce, ForceMode.Force); 
        
        Vector3 clampedFlat = Vector3.ClampMagnitude(flatVel, scaledMax);

        Vector3 smoothedFlat = Vector3.Lerp(flatVel, clampedFlat, 1f - movementSmoothing);
        rb.linearVelocity = new Vector3(smoothedFlat.x, rb.linearVelocity.y, smoothedFlat.z);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
        else if (other.gameObject.CompareTag("Trap"))
        {
            Respawn();
            SoundManager.Instance?.PlayTrapHit();
        }
        else if (other.gameObject.CompareTag("Victory"))
        {
            GameManager.Instance?.OnLevelComplete();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
            isGrounded = false;
    }

    private void Respawn()
    {
        //reseteo velocidad y escala
        rb.linearVelocity  = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = spawn.position;

        speedMultiplier    = 1f;
        transform.localScale = Vector3.one;
    }
}
