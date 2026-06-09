using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //maneja t0d0 lo relativo a audio
    public static SoundManager Instance { get; private set; }

    [Header("Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Clips")]
    [SerializeField] private AudioClip musicClip;
    [SerializeField] private AudioClip winClip;
    [SerializeField] private AudioClip loseClip;
    [SerializeField] private AudioClip trapHitClip;
    [SerializeField] private AudioClip jumpClip;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        //reproduzco y loopeo la música de fondo cuando empieza el nivel
        if (musicSource == null || musicClip == null) return;
        musicSource.clip = musicClip;
        musicSource.loop = true;
        musicSource.Play();
    }

    //estos métodos se llaman en el manager dependiendo la condición de victoria o derrota
    //o colisión o input
    public void PlayWin()      => PlaySFX(winClip);
    public void PlayLose()     => PlaySFX(loseClip);
    public void PlayTrapHit()  => PlaySFX(trapHitClip);
    public void PlayJump()     => PlaySFX(jumpClip);

    private void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
