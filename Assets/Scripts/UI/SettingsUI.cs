using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public AudioMixer mainAudioMixer;

    [Header("Volume Sliders")]
    public Slider mainVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider soundVolumeSlider;
    
    [Header("Resolution Options")]
    [SerializeField] private List<String> resolutionOptions;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle isFullscreenToggle;
    
    
    private void Awake()
    {
        SetResolutionOptions();
    }

    private void OnEnable()
    {
        mainVolumeSlider.value = SavedValues.instance.defaultMasterSliderValue;
        musicVolumeSlider.value = SavedValues.instance.defaultMusicSliderValue;
        soundVolumeSlider.value = SavedValues.instance.defaultSoundSliderValue;
        isFullscreenToggle.isOn = SavedValues.instance.isFullscreen;
    }

    private void OnDisable()
    {
        SavedValues.instance.defaultMasterSliderValue = mainVolumeSlider.value;
        SavedValues.instance.defaultMusicSliderValue = musicVolumeSlider.value;
        SavedValues.instance.defaultSoundSliderValue = soundVolumeSlider.value;
        SavedValues.instance.isFullscreen = isFullscreenToggle.isOn;
    }

    public void OnReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void SetResolutionOptions()
    {
        // make new list of strings with indexes lining up to Screen.resolutions
        resolutionOptions = new List<string>();
        for (int i = 0; i<Screen.resolutions.Length; i++)
        {
            resolutionOptions.Add(Screen.resolutions[i].width + "x" + Screen.resolutions[i].height + " :" + Screen.resolutions[i].refreshRateRatio);            
        }
        // clear dropdown
        resolutionDropdown.ClearOptions();

        // add options to the dropdown
        resolutionDropdown.AddOptions(resolutionOptions);
    }
    
    public void SetResolution()
    {
        Screen.SetResolution(Screen.resolutions[resolutionDropdown.value].width, Screen.resolutions[resolutionDropdown.value].height, isFullscreenToggle.isOn);
    }
    
    public void OnMainVolumeChange()
    {
        SavedValues.instance.soundAudioSource.PlayOneShot(SavedValues.instance.menuButton);
        VolumeChange(mainAudioMixer, "MasterVolume", mainVolumeSlider);
    }

    public void OnMusicVolumeChange()
    {
        SavedValues.instance.soundAudioSource.PlayOneShot(SavedValues.instance.menuButton);
        VolumeChange(mainAudioMixer, "MusicVolume", musicVolumeSlider);
    }

    public void OnSoundVolumeChange()
    {
        SavedValues.instance.soundAudioSource.PlayOneShot(SavedValues.instance.menuButton);
        VolumeChange(mainAudioMixer, "SFXVolume", soundVolumeSlider);
    }

    public void VolumeChange(AudioMixer audioMixer, string nameOfMixer, Slider volumeSlider)
    {
        float newVolume = volumeSlider.value;
        if (newVolume <= 0)
        {
            newVolume = -80; //set to bottommost log value for db
        }
        else
        {
            //>0 so use log10 because decibals
            newVolume = Mathf.Log10(newVolume);

            //0-20db instead of 0-1
            newVolume = newVolume * 20;

        }

        audioMixer.SetFloat(nameOfMixer, newVolume);
    }
}
