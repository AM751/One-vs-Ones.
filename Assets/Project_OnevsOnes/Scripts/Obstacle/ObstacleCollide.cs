using System;
using UnityEngine;

public class ObstacleCollide : MonoBehaviour
{
    public AudioSource bashAudioSource;
    [SerializeField] public AudioClip bashAudio;

    void Awake()
    {
        bashAudioSource = GetComponent<AudioSource>();
    }
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bashAudioSource.PlayOneShot(bashAudio);
        }
    }
    
}
