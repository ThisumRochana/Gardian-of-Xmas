using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("SFX Clips")]
    public AudioClip goblinStealGiftClip;
    public AudioClip goblinHitClip;
    public AudioClip snowballThrowClip;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            bgmSource = gameObject.AddComponent<AudioSource>();
            sfxSource = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // BGM controls
    public void PlayBGM()
    {
        if (bgmSource != null && !bgmSource.isPlaying)
            bgmSource.Play();
    }

    public void StopBGM()
    {
        if (bgmSource != null)
            bgmSource.Stop();
    }

    // SFX functions
    public void PlayGoblinStealGift()
    {
        if (goblinStealGiftClip != null)
            sfxSource.PlayOneShot(goblinStealGiftClip);
    }

    public void PlayGoblinHit()
    {
        if (goblinHitClip != null)
            sfxSource.PlayOneShot(goblinHitClip);
    }

    public void PlaySnowballThrow()
    {
        if (snowballThrowClip != null)
            sfxSource.PlayOneShot(snowballThrowClip);
    }
}
