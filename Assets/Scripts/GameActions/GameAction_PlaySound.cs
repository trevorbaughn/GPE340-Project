using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameAction_PlaySound : GameAction
{
    public AudioClip audioClip;
    public float volume = 1.0f;
    private AudioSource audioSource;

    

    public override void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        audioSource.PlayOneShot(audioClip, volume);        
    }
}
