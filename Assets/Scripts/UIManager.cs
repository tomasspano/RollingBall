using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("HUD")]
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private TMP_Text   timerText;

    [Header("Panels")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    [Header("Buttons")]
    [SerializeField] private Button winRestartButton;
    [SerializeField] private Button loseRestartButton;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void Start()
    {
        if (winRestartButton  != null) winRestartButton.onClick.AddListener(GameManager.Instance.RestartLevel);
        if (loseRestartButton != null) loseRestartButton.onClick.AddListener(GameManager.Instance.RestartLevel);

        ShowHUD();
    }

    public void UpdateTimer(float timeRemaining)
    {
        if (timerText == null) return;
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        timerText.text = $"{minutes}:{seconds}";
    }

    public void ShowHUD()  => SwitchTo(hudPanel);
    public void ShowWin()  => SwitchTo(winPanel);
    public void ShowLose() => SwitchTo(losePanel);

    private void SwitchTo(GameObject target)
    {
        SetActive(hudPanel,  false);
        SetActive(winPanel,  false);
        SetActive(losePanel, false);
        SetActive(target,    true);
    }

    private static void SetActive(GameObject go, bool state)
    {
        if (go != null) go.SetActive(state);
    }
}
