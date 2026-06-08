using UnityEngine;
using DG.Tweening;

/// <summary>
/// SizeTrap — shrinks or grows the player ball using DOTween.
/// The scale change is cosmetic AND mechanical: it adjusts Player.speedMultiplier
/// so a tiny ball is slower and a giant ball is faster.
///
/// Tag this object as "Trap".
/// Set isTrigger = true on its Collider so the ball rolls over it without bouncing.
/// </summary>
public class SizeTrap : MonoBehaviour, ITrap
{
    [Header("Size settings")]
    [Tooltip("Target scale applied to the player. < 1 = shrink, > 1 = grow.")]
    [SerializeField] private float targetScale    = 0.4f;

    [Tooltip("How long the DOTween scale animation takes.")]
    [SerializeField] private float tweenDuration  = 0.35f;

    [Tooltip("How long the player stays at the modified scale before reverting.")]
    [SerializeField] private float effectDuration = 4f;

    [Tooltip("Ease curve for the shrink/grow punch.")]
    [SerializeField] private Ease  tweenEase      = Ease.OutBack;

    [Header("Cooldown")]
    [SerializeField] private float cooldown = 6f;

    private bool onCooldown = false;

    // ------------------------------------------------------------------ ITrap
    public void Activate()
    {
        // Always-on trap — nothing to initialise.
    }

    public void OnTrapTriggered(GameObject player)
    {
        if (onCooldown) return;

        Player p = player.GetComponent<Player>();
        if (p == null) return;

        player.transform.DOScale(Vector3.one * targetScale, tweenDuration)
              .SetEase(tweenEase);

        p.speedMultiplier = targetScale;

        DOVirtual.DelayedCall(effectDuration, () =>
        {
            if (player == null) return;
            player.transform.DOScale(Vector3.one, tweenDuration).SetEase(tweenEase);
            p.speedMultiplier = 1f;
        });

        StartCooldown();
    }

    // ------------------------------------------------------------------ unity
    private void Start() => Activate();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            OnTrapTriggered(other.gameObject);
    }

    // ------------------------------------------------------------------ helpers
    private void StartCooldown()
    {
        onCooldown = true;
        DOVirtual.DelayedCall(cooldown, () => onCooldown = false);
    }
}
