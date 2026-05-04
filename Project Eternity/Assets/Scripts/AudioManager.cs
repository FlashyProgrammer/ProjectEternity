using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Abilities Source")]
    [SerializeField] public AudioSource freezeAbility;
    [SerializeField] public AudioSource sightAbility;
    [SerializeField] public AudioSource transition;
    [SerializeField] public AudioSource audioSource;

    [Header("Abilities Sound")]
    [SerializeField] public AudioClip timeStop;
    [SerializeField] public AudioClip sightSound;
    [SerializeField] public AudioClip transitionSound;

    [Header("Player Sounds")]
    [SerializeField] public AudioClip walkSound;

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





}
