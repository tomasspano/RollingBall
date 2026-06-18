using DG.Tweening;
using UnityEngine;

public class SizeTrap : MonoBehaviour, ITrap
{
    [SerializeField] private float targetScale    = 0.4f;

    [SerializeField] private float tweenDuration  = 0.35f;

    [SerializeField] private float effectDuration = 3f;

    public void Activate()
    {
    }

    public void OnTrapTriggered(GameObject player)
    {
        Player p = player.GetComponent<Player>();
        if (p == null) return;

        player.transform.DOScale(Vector3.one * targetScale, tweenDuration)
              .SetEase(Ease.InBounce);

        p.speedMultiplier = targetScale;

        DOVirtual.DelayedCall(effectDuration, () =>
        {
            player.transform.DOScale(Vector3.one, tweenDuration).SetEase(Ease.OutBounce);
            p.speedMultiplier = 1f;
        });

    }

    private void Start() => Activate();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            OnTrapTriggered(other.gameObject);
    }

}
