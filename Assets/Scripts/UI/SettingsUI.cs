using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsUI : MonoBehaviour
{
    public void OnReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
