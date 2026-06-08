using UnityEngine;

public class SizeTrap : MonoBehaviour, ITrap
{
    [SerializeField] private float targetScale    = 0.4f;

    [SerializeField] private float tweenDuration  = 0.35f;

    [SerializeField] private float effectDuration = 4f;
    
    [SerializeField] private float cooldown = 6f;

    private bool onCooldown = false;

    public void Activate()
    {
    }

    public void OnTrapTriggered(GameObject player)
    {
        if (onCooldown) return;

        Player p = player.GetComponent<Player>();
        if (p == null) return;

        //player.transform.DOScale(Vector3.one * targetScale, tweenDuration)
              //.SetEase(tweenEase);

        p.speedMultiplier = targetScale;

        /*DOVirtual.DelayedCall(effectDuration, () =>
        {
            if (player == null) return;
            player.transform.DOScale(Vector3.one, tweenDuration).SetEase(tweenEase);
            p.speedMultiplier = 1f;
        });*/

        StartCooldown();
    }

    private void Start() => Activate();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            OnTrapTriggered(other.gameObject);
    }

    private void StartCooldown()
    {
        onCooldown = true;
        //DOVirtual.DelayedCall(cooldown, () => onCooldown = false);
    }
}
