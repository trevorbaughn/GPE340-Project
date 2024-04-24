using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public void OnQuit()
    {
        Application.Quit();
    }

    public void OnReturnToMainMenu()
    {
        GameManager.instance.soundAudioSource.PlayOneShot(GameManager.instance.menuButton);
        SceneManager.LoadScene("MainMenu");
    }
}
