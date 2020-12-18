using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSound : MonoBehaviour
{
    private AudioSource source;

    private void OnEnable() {
        source = GetComponent<AudioSource>();
        source.volume = PlayerPrefs.GetFloat("MainPref") * PlayerPrefs.GetFloat("BackgorundPref");
        source.Play();
    }
}
