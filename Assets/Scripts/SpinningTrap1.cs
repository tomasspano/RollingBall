using UnityEngine;

public class SpinningTrap : MonoBehaviour, ITrap
{
    [SerializeField] private float rotationSpeed = 120f;

    public void Activate()
    {
    }

    public void OnTrapTriggered(GameObject player)
    {
        SoundManager.Instance?.PlayTrapHit();
    }

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
