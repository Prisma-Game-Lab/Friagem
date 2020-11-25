using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    
    public string pauseButton;
    public GameObject PauseMenuUI;
    public GameObject SettingsMenuUI;

    private bool paused = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pauseButton))
        {
            if (paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }


    public void ResumeGame()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    public void PauseGame()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    public void OpenSettings()
    {
        SettingsMenuUI.SetActive(true);
        PauseMenuUI.SetActive(false);
    }

    public void QuitSettingsMenu()
    {
        SettingsMenuUI.SetActive(false);
        PauseMenuUI.SetActive(true);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        paused = false;
        SceneManager.LoadScene("Main Menu");
    }
}
