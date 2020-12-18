using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    private AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        source.volume = PlayerPrefs.GetFloat("MainPref") * PlayerPrefs.GetFloat("BackgorundPref");
    }
}
