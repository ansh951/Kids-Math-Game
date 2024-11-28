using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource bgMusicSource;
    public AudioSource clickSource;

    public AudioClip menuMusic;
    public AudioClip gameMusic;
    public AudioClip buttonClickSound;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayBackgroundMusic();
        PlayButtonClickSound();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBackgroundMusic();
    }

    void PlayBackgroundMusic()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            bgMusicSource.clip = menuMusic;
        }
        else if (SceneManager.GetActiveScene().name == "Game")
        {
            bgMusicSource.clip = gameMusic;
        }
        bgMusicSource.Play();
    }

    public void PlayButtonClickSound()
    {
        //bgMusicSource.PlayOneShot(buttonClickSound);
        clickSource.clip = buttonClickSound;
    }
}
