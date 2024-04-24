using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    public void OnStartToMainMenu()
    {
        GameManager.instance.soundAudioSource.PlayOneShot(GameManager.instance.menuButton);
        SceneManager.LoadScene("MainMenu");
    }
}
