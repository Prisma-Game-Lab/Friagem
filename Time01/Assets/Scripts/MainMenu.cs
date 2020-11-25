using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{


    void Start()
    {
        //GetComponent<PauseMenu>().fromGame = false; Está dando erro
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        
    }

    public void QuitGame()
    {
        Debug.Log("SAIR");
        Application.Quit();
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("Settings Menu");
    }
}
