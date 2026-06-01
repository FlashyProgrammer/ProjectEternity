using UnityEngine;
using UnityEngine.UI;

public class MusicSliderUI : MonoBehaviour
{
    public AudioManager audioManager;
    public Slider musicSlider;

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);

        musicSlider.value = savedVolume;
        audioManager.musicSource.volume = savedVolume;

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
    }

    public void SetMusicVolume(float value)
    {
        audioManager.musicSource.volume = value;
        PlayerPrefs.SetFloat("MusicVolume", value);
    }
}