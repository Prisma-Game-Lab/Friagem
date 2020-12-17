using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public GameObject Player;
    public GameObject DeathUI;

    private PlayerDeath playerDeath;
    // Start is called before the first frame update
    void Start()
    {
        playerDeath = Player.GetComponent<PlayerDeath>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDeath.dead)
        {
            DeathUI.SetActive(true);
            Time.timeScale = 0f;
        }

    }


    public void ResumeGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        DeathUI.SetActive(false);
        Time.timeScale = 1f;
        playerDeath.dead = false;
        
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        playerDeath.dead = false;
        SceneManager.LoadScene("Main Menu");
    }
}
