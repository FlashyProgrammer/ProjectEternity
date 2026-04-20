using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] public AudioSource audioSourceAbilities;
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip timeStop;
    [SerializeField] public AudioClip walkSound;

    public void SetClip(AudioClip sound)
    {
       audioSource.clip = sound;
       return;
    }
    public void SetClipAbility(AudioClip sound)
    {
        audioSourceAbilities.clip = sound;
        return;
    }



}
