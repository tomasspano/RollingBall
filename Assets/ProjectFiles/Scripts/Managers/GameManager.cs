using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //condiciones de victoria y derrota y manejo del nivel
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
    
        //actualizo el tiempo constantemente y si se alcanza el límite se acaba la partida
        timeRemaining -= Time.deltaTime;
        UIManager.Instance?.UpdateTimer(timeRemaining);

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            OnPlayerLose();
        }
    }
    public void OnLevelComplete()
    {
        if (!gameActive) return;
        EndGame();
        SoundManager.Instance?.PlayWin();
        UIManager.Instance?.ShowWin();
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnPlayerLose()
    {
        EndGame();
        SoundManager.Instance?.PlayLose();
        UIManager.Instance?.ShowLose();
    }

    private void EndGame()
    {
        gameActive     = false;
        Time.timeScale = 0f;
        Cursor.visible = true;
    }

    public void OnPlayerRespawn()
    {
        timeRemaining += 10f;
        if (timeRemaining >= levelTimeLimit) timeRemaining = levelTimeLimit;
    }
}
