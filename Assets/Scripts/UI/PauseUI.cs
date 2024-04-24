using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    public void ResumeGame()
    {
        GameManager.instance.soundAudioSource.PlayOneShot(GameManager.instance.menuButton);
        GameManager.instance.UnPause();
    }

    public void QuitToMenu()
    {
        GameManager.instance.soundAudioSource.PlayOneShot(GameManager.instance.menuButton);
        GameManager.instance.UnPause();
        SceneManager.LoadScene("MainMenu");
    }
}