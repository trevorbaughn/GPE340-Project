using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedValues : MonoBehaviour
{
    public static SavedValues instance;
    private void Awake()
    {
        if (instance == null)
        {
            //this is THE SavedValues
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else //this isn't THE SavedValues
        {
            Destroy(gameObject);
            
        }
    }

    [Header("Saves Here")]
    public float defaultMasterSliderValue;
    public float defaultMusicSliderValue;
    public float defaultSoundSliderValue;
    public bool isFullscreen;
    
    [Header("Sounds")]
    public AudioClip menuButton;
    public AudioSource soundAudioSource;
    public AudioSource musicAudioSource;
}
