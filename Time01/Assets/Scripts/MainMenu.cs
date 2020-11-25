using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject SettingsMenuUI;
    public GameObject MainMenuUI;

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

    public void QuitGame()
    {
        Debug.Log("SAIR");
        Application.Quit();
    }

    public void OpenSettings()
    {
        SettingsMenuUI.SetActive(true);
        MainMenuUI.SetActive(false);
    }

    public void QuitSettingsMenu()
    {
        SettingsMenuUI.SetActive(false);
        MainMenuUI.SetActive(true);
    }
}
