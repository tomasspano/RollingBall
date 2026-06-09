using UnityEngine;

public interface ITrap
{
    //interfaz que implementan todas las trampas: se activan y se triggerean
    void Activate();
    void OnTrapTriggered(GameObject player);
}
