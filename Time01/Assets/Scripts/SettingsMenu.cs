using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{

    public bool fromGame = false;

    public void QuitMenu()
    {
        if (!fromGame)
        {
            SceneManager.LoadScene("Main Menu");
        }
        /*else
        {
            SceneManager.LoadScene("Pause Menu");
        }
        */
    }
}
