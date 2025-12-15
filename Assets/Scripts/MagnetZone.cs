using System;
using UnityEngine;

public class MagnetZone : MonoBehaviour
{
    
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(-10, 0, 0), ForceMode.Force);
        }
    }
}
