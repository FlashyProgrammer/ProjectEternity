using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Abilities Source")]
    [SerializeField] public AudioSource freezeAbility;
    [SerializeField] public AudioSource sightAbility;
    [SerializeField] public AudioSource transition;
    [SerializeField] public AudioSource audioSource;

    [Header("Music")]
    [SerializeField] public AudioSource musicSource;
    [SerializeField] public AudioClip menuMusic;
    [SerializeField] public AudioClip gameMusic;

    [Header("Abilities Sound")]
    [SerializeField] public AudioClip timeStop;
    [SerializeField] public AudioClip sightSound;
    [SerializeField] public AudioClip transitionSound;

    [Header("Player Sounds")]
    [SerializeField] public AudioClip walkSound;

    public AudioManager audioManager;
    public Slider musicSlider;

    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        audioManager.musicSource.volume = musicSlider.value;

        musicSlider.onValueChanged.AddListener(SetVolume);
    }

    void SetVolume(float value)
    {
        audioManager.musicSource.volume = value;
        PlayerPrefs.SetFloat("MusicVolume", value);
    }
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        AudioManager[] managers = FindObjectsOfType<AudioManager>();
        if (managers.Length > 1)
        {
            Destroy(gameObject);
            return;
        }
    }
    public void SetClip(AudioClip sound)
    {
       audioSource.clip = sound;
       return;
    }
    public void SetClipAbilityFreeze(AudioClip sound)
    {
        freezeAbility.clip = sound;
        return;
    }

    public void SetClipAbilitySight(AudioClip sound)
    {
        sightAbility.clip = sound;
        return;
    }

    public void SetTransition(AudioClip sound)
    {
        transition.clip = sound;
        return;
    }

    public void PlayMenuMusic()
    {
        musicSource.clip = menuMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayGameMusic()
    {
        musicSource.clip = gameMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }



}
