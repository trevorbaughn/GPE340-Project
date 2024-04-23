using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryUI : MonoBehaviour
{
    public void OnQuit()
    {
        Application.Quit();
    }

    public void OnReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
