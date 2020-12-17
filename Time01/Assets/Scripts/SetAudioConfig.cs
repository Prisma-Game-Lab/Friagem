using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAudioConfig : MonoBehaviour
{
    private float bakcgroundVol, sfxVol, mainVol;
    public AudioSource BGM;
    public AudioSource[] SFX;

    void Awake()
    {
        KeepSettings();
    }

    private void KeepSettings()
    {
        bakcgroundVol = PlayerPrefs.GetFloat("BackgroundPref");
        sfxVol = PlayerPrefs.GetFloat("SfxPref");
        mainVol = PlayerPrefs.GetFloat("MainPref");

        BGM.volume = bakcgroundVol;

        for(int i = 0; i < SFX.Length; i++)
        {
            SFX[i].volume = sfxVol;
        }
    }
}
