using System;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public static PlayerAudio Instance;
    
    [Header("Audio Zone.")]
    [SerializeField] public AudioClip sprintAudio;
    [SerializeField] public AudioClip jumpAudio;
    [SerializeField] public AudioClip bashAudio;
    public AudioSource sprintAudioSource;
    public AudioSource jumpAudioSource;
    public AudioSource bashAudioSource;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    public void playSoundOnJump()
    {
        if (jumpAudio != null && jumpAudioSource != null)
        {
            jumpAudioSource.PlayOneShot(jumpAudio);
        }
    }

    public void playSoundOnSprint()
    {
        if (sprintAudio != null && sprintAudioSource != null)
        {
            sprintAudioSource.PlayOneShot(sprintAudio);
        }
    }

    public void playSoundOnObjectCollide()
    {
        if (bashAudio != null && bashAudioSource != null)
        {
            bashAudioSource.PlayOneShot(bashAudio);
        }
    }
}
