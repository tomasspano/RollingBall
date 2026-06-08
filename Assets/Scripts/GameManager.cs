using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Timer")]
    [SerializeField] private float levelTimeLimit = 120f; //2 minutos

    private float timeRemaining;
    private bool  gameActive;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void Start()
    {
        timeRemaining = levelTimeLimit;
        gameActive    = true;
    }

    private void Update()
    {
        if (!gameActive) return;

        timeRemaining -= Time.deltaTime;
        UIManager.Instance?.UpdateTimer(timeRemaining);

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            OnTimeout();
        }
    }
    public void OnLevelComplete()
    {
        if (!gameActive) return;
        EndGame();
        SoundManager.Instance?.PlayWin();
        UIManager.Instance?.ShowWin();
    }

    public void OnPlayerDied()
    {
        if (!gameActive) return;
        EndGame();
        SoundManager.Instance?.PlayLose();
        UIManager.Instance?.ShowLose();
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        int next = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(next < SceneManager.sceneCountInBuildSettings ? next : 0);
    }

    private void OnTimeout()
    {
        EndGame();
        SoundManager.Instance?.PlayLose();
        UIManager.Instance?.ShowLose();
    }

    private void EndGame()
    {
        gameActive     = false;
        Time.timeScale = 0f;
    }
}
