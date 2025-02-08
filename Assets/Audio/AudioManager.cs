using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // Singleton (чтобы менеджер существовал один раз)
    private AudioSource audioSource;

    public AudioClip menuMusic;  // Музыка для Menu, Options, Tutorial
    public AudioClip panelsMusic; // Музыка для Panels

    private string currentScene; // Хранение текущей сцены

    void Awake()
    {
        // Если уже есть AudioManager, удалить дубликат
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Не удалять при смене сцен

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        ChangeMusic(SceneManager.GetActiveScene().name);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ChangeMusic(scene.name);
    }

    void ChangeMusic(string sceneName)
    {
        // Если музыка уже играет в этой сцене — ничего не менять
        if (currentScene == sceneName)
            return;

        currentScene = sceneName;

        AudioClip newClip = null;

        if (sceneName == "Menu" || sceneName == "Options" || sceneName == "Tutorial")
            newClip = menuMusic;
        else if (sceneName == "Panels")
            newClip = panelsMusic;

        if (newClip != null && audioSource.clip != newClip)
        {
            audioSource.clip = newClip;
            audioSource.Play();
        }
    }
}
