using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    #region STATIC INSTANCE
    public static AudioManager Instance { get; private set; }
    #endregion
    #region SERIALIZABLE VARIABLES
    [Header("UI Sounds")]
    [SerializeField] private AudioClip uiClickSound;

    [Header("Music Settings")]
    [SerializeField] private List<MusicEntry> musicEntries = new List<MusicEntry>();

    [System.Serializable]
    public enum MusicType
    {
        MenuMusic,
        VillageMusic,
        //las que sean
    }

    [System.Serializable]
    public struct MusicEntry
    {
        public MusicType type;
        public AudioClip clip;
    }
    #endregion

    #region PRIVATE VARIABLES
    private AudioSource musicSource;
    private AudioSource sfxSource;
    private Dictionary<MusicType, AudioClip> musicDictionary;
    #endregion


    #region PRIVATE FUNCS
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SetupAudioSources();
            BuildMusicDictionary();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        AssignClickSoundToAllButtons();
    }

    private void SetupAudioSources()
    {
        // Configurar AudioSources
        musicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        musicSource.loop = true;
        musicSource.playOnAwake = false;
    }

    private void BuildMusicDictionary()
    {
        musicDictionary = new Dictionary<MusicType, AudioClip>();

        foreach (MusicEntry entry in musicEntries)
        {
            if (!musicDictionary.ContainsKey(entry.type))
            {
                musicDictionary.Add(entry.type, entry.clip);
            }
            else
            {
                Debug.LogWarning($"Music type {entry.type} is duplicated in the list");
            }
        }
    }

    private void AssignClickSoundToAllButtons()
    {
        Button[] allButtons = FindObjectsByType<Button>(FindObjectsSortMode.None);

        foreach (Button button in allButtons)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(OnButtonClick);
        }

        Debug.Log($"Se asignó sonido de click a {allButtons.Length} botones");
    }

    private void OnButtonClick()
    {
        if (uiClickSound != null)
        {
            sfxSource.PlayOneShot(uiClickSound);
        }
    }
    #endregion

    #region PUBLIC FUNCS
    public void PlayMusic(MusicType musicType)
    {
        if (musicDictionary != null && musicDictionary.ContainsKey(musicType))
        {
            musicSource.clip = musicDictionary[musicType];
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning($"Music clip for {musicType} not found!");
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
    #endregion

}