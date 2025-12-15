using UnityEngine;

public class SpinningTrap : MonoBehaviour
{
   
    [SerializeField] private float rotationSpeed;
    
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
