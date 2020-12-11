using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public string BotaoRestart;
    public string NextLevel;
    public int NextLevelIndex;

    public float firstFlareTime;
    public Animator transition;
    public GameObject levelUI;
    public GameObject Flare;
    public GameObject Light;

    // Start is called before the first frame update
    void Start()
    {
        NextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        //levelUI.SetActive(true);
        StartCoroutine(BeginLevel());
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

            //SceneManager.LoadScene(NextLevel, LoadSceneMode.Single);
            StartCoroutine(LoadLevel(NextLevel));

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

    IEnumerator LoadLevel(string NextLevel)
    {
        transition.SetTrigger("Start");
        levelUI.SetActive(false);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(NextLevel, LoadSceneMode.Single);
    }

    IEnumerator BeginLevel()
    {
        yield return new WaitForSeconds(0.5f);
        if (!Light.activeSelf)
        {
            //restringir movimento do player
            Flare.SetActive(true);
            yield return new WaitForSeconds(firstFlareTime);
        }
        
        //permitir movimento do player
        levelUI.SetActive(true);
        Flare.SetActive(false);
    }
}
