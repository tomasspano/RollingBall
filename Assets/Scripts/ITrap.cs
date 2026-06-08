public interface ITrap
{
    // Called once when the trap initialises — start moving, spinning, etc.
    void Activate();

    // Called whenever the player collides with / enters the trap.
    void OnTrapTriggered(UnityEngine.GameObject player);
}
