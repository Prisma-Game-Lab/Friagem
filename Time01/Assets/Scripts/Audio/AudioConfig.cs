using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioConfig : MonoBehaviour
{
    private int firstPlayInt;
    public Slider backgound, sfx, main;
    private float backgroundVol, sfxVol, mainVol;

    //public AudioSource BGM;
    //public AudioSource[] SFX;

    void Awake()
    {
        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            KeepSettings();
        }
    }

    void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt("FirstPlay");

        if(firstPlayInt == 0)
        {
            backgroundVol = 0.5f;
            sfxVol = 0.5f;
            mainVol = 0.5f;

            backgound.value = backgroundVol;
            sfx.value = sfxVol;
            main.value = mainVol;

            PlayerPrefs.SetFloat("BackgorundPref", backgroundVol);
            PlayerPrefs.SetFloat("SfxPref",sfxVol);
            PlayerPrefs.SetFloat("MainPref",mainVol);
            PlayerPrefs.SetInt("FirstPlay", -1);
        }
        else
        {
            backgroundVol = PlayerPrefs.GetFloat("BackgorundPref");
            sfxVol = PlayerPrefs.GetFloat("SfxPref");
            mainVol = PlayerPrefs.GetFloat("MainPref");

            backgound.value = backgroundVol;
            sfx.value = sfxVol;
            main.value = mainVol;
        }
    }


    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("BackgorundPref", backgound.value);
        PlayerPrefs.SetFloat("SfxPref", sfx.value);
        PlayerPrefs.SetFloat("MainPref", main.value);
    }

    private void OnApplicationFocus(bool focus)
    {
        if(!focus)
        {
            SaveVolume();
        }
    }

    /*public void UpdateSound()
    {
        BGM.volume = backgound.value;

        for(int i = 0; i < SFX.Length; i++)
        {
            SFX[i].volume = sfx.value;
        }
    }*/

    private void KeepSettings()
    {
        backgroundVol = PlayerPrefs.GetFloat("BackgroundPref");
        sfxVol = PlayerPrefs.GetFloat("SfxPref");
        mainVol = PlayerPrefs.GetFloat("MainPref");

        backgound.value = backgroundVol;
        sfx.value = sfxVol;
        main.value = mainVol;

        /*BGM.volume = backgroundVol;

        for (int i = 0; i < SFX.Length; i++)
        {
            SFX[i].volume = sfxVol;
        }*/
    }
}
