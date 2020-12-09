using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public string BotaoRestart;
    public string NextLevel;

    public int NextLevelIndex;

    // Start is called before the first frame update
    void Start()
    {
        NextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(BotaoRestart)) 
        {
            Restart();
            Debug.Log("Restart");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(NextLevelIndex == 13)
            {
                Debug.Log("Fim do jogo");
            }

            SceneManager.LoadScene(NextLevel, LoadSceneMode.Single);

            if (NextLevelIndex > PlayerPrefs.GetInt("levelReached") && NextLevelIndex != 13)
            {
                PlayerPrefs.SetInt("levelReached", NextLevelIndex);
            }
        }
    }


    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
