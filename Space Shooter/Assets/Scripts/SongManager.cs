using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;

public class SongManager : MonoBehaviour
{
    public static SongManager Instance;

    [Header("Clips")]
    public AudioClip musicClip;    
    public AudioClip menuClip;     

    [Header("Config")]
    public string[] playScenes = new string[] { "Main" }; 
    [Range(0f, 1f)] public float defaultVolume = 1f;
    public bool loop = true;

    AudioSource audioSource;
    Coroutine fadeCoroutine;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.spatialBlend = 0f;
        audioSource.playOnAwake = false;
        audioSource.loop = loop;
        audioSource.volume = defaultVolume;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        ApplySceneMusic(SceneManager.GetActiveScene().name);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ApplySceneMusic(scene.name);
    }

    void ApplySceneMusic(string sceneName)
    {
        if (fadeCoroutine != null) { StopCoroutine(fadeCoroutine); fadeCoroutine = null; }

        if (menuClip != null && sceneName == "MainMenu")
        {
            PlayClip(menuClip, true);
            return;
        }

        if (playScenes != null && playScenes.Contains(sceneName))
        {
            PlayClip(musicClip, loop);
            return;
        }

        StopImmediate();
    }

    void PlayClip(AudioClip clip, bool shouldLoop)
    {
        if (clip == null)
        {
            StopImmediate();
            return;
        }

        audioSource.loop = shouldLoop;
        if (audioSource.clip != clip) audioSource.clip = clip;
        audioSource.volume = defaultVolume;

        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    public void Play() => PlayClip(musicClip, loop);

    public void RestoreAndPlay()
    {
        string current = SceneManager.GetActiveScene().name;
        if (menuClip != null && current == "MainMenu")
        {
            PlayClip(menuClip, true);
            return;
        }

        if (playScenes != null && playScenes.Contains(current))
        {
            PlayClip(musicClip, loop);
            return;
        }

        PlayClip(musicClip, loop);
    }

    public void FadeOutAndStop(float duration)
    {
        if (audioSource == null) return;
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeOutCoroutine(duration));
    }

    IEnumerator FadeOutCoroutine(float duration)
    {
        if (audioSource == null || audioSource.clip == null) yield break;
        float startVolume = audioSource.volume;
        float t = 0f;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / duration);
            yield return null;
        }
        audioSource.volume = 0f;
        audioSource.Stop();
        audioSource.volume = defaultVolume;
        fadeCoroutine = null;
    }

    public void StopImmediate()
    {
        if (fadeCoroutine != null) { StopCoroutine(fadeCoroutine); fadeCoroutine = null; }
        if (audioSource != null) audioSource.Stop();
    }
}
