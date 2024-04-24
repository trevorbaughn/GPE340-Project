using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void OnQuit()
    {
        Application.Quit();
    }

    public void OnSettings()
    {
        GameManager.instance.soundAudioSource.PlayOneShot(GameManager.instance.menuButton);
        SceneManager.LoadScene("Settings");
    }

    public void OnStartGame()
    {
        GameManager.instance.soundAudioSource.PlayOneShot(GameManager.instance.menuButton);
        SceneManager.LoadScene("TestScene");
    }
}
