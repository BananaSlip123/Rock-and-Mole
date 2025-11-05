using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    #region STATIC INSTANCE
    public static AudioManager Instance { get; private set; }
    #endregion

    #region SERIALIZABLE VARIABLES

    [Header("Music Settings")]
    [SerializeField] private List<MusicEntry> musicEntries = new List<MusicEntry>();

    [Header("Audio Settings")]
    [SerializeField] private List<AudioEntry> audioEntries = new List<AudioEntry>();

    [System.Serializable]
    public enum MusicType
    {
        MenuMusic,
        VillageMusic,
        EnemyFightMusic,
        StoreMusic,
        StoreMusic2
        //las que sean
    }

    [System.Serializable]
    public enum AudioType
    {
        WalkSound,
        DeathEnemySound,
        DeathPlayerSound,
        AttackToEnemySound,
        AttackToPlayerSound,
        ClickerSound,
        MineSound,
        CollectObjectsSound
        //las que sean
    }

    [System.Serializable]
    public struct MusicEntry
    {
        public MusicType type;
        public AudioClip clip;
    }

    [System.Serializable]
    public struct AudioEntry
    {
        public AudioType type;
        public AudioClip clip;
    }
    #endregion

    #region PRIVATE VARIABLES
    private AudioSource musicSource;
    private AudioSource sfxSource;
    private Dictionary<MusicType, AudioClip> musicDictionary;
    private Dictionary<AudioType, AudioClip> audioDictionary;
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
            BuildAudioDictionary();
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
        sfxSource.playOnAwake = false;
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

    private void BuildAudioDictionary()
    {
        audioDictionary = new Dictionary<AudioType, AudioClip>();

        foreach (AudioEntry entry in audioEntries)
        {
            if (!audioDictionary.ContainsKey(entry.type))
            {
                audioDictionary.Add(entry.type, entry.clip);
            }
            else
            {
                Debug.LogWarning($"Audio type {entry.type} is duplicated in the list");
            }
        }
    }

    private void AssignClickSoundToAllButtons()
    {
        Button[] allButtons = FindObjectsByType<Button>(FindObjectsSortMode.None);

        foreach (Button button in allButtons)
        {
            button.onClick.AddListener(OnButtonClick);
        }

        Debug.Log($"Se asignó sonido de click a {allButtons.Length} botones");
    }

    private void OnButtonClick()
    {
        if (audioDictionary != null && audioDictionary.TryGetValue(AudioType.ClickerSound, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip);
        }
    }
    #endregion

    #region PUBLIC FUNCS
    public void PlayMusic(MusicType musicType)
    {
        if (musicDictionary != null && musicDictionary.TryGetValue(musicType, out AudioClip clip))
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning($"Music clip for {musicType} not found!");
        }
    }

    public void PlayAudio(AudioType audioType)
    {
        if (audioDictionary != null && audioDictionary.TryGetValue(audioType, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"Audio clip for {audioType} not found!");
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    //Reproduce en bucle
    public void PlayLoopedAudio(AudioType audioType)
    {
        if (audioDictionary.TryGetValue(audioType, out AudioClip clip))
        {
            if (sfxSource.clip != clip || !sfxSource.isPlaying)
            {
                sfxSource.clip = clip;
                sfxSource.loop = true;
                sfxSource.Play();
            }
        }
        else
        {
            Debug.LogWarning($"Audio clip for {audioType} not found!");
        }
    }


    //Detiene el audio
    public void StopAudio(AudioType audioType)
    {
        if (audioDictionary.TryGetValue(audioType, out AudioClip clip))
        {
            if (sfxSource.clip == clip)
            {
                sfxSource.Stop();
                sfxSource.loop = false;
                sfxSource.clip = null;
            }
        }
    }
    #endregion
}
