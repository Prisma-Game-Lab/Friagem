using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSounds : MonoBehaviour
{

    public float intervalMin;
    public float intervalMax;

    private float currentInterval;
    private float currentTime;

    public List<AudioClip> soundList;
    public AudioSource sfxSource;

    void Start() {
        currentTime=0;
        currentInterval=Random.Range(intervalMin, intervalMax);
    }
    
    void Update() {
        currentTime += Time.deltaTime;
        if(currentTime >= currentInterval)
        {
            sfxSource.volume = PlayerPrefs.GetFloat("MainPref") * PlayerPrefs.GetFloat("SfxPref");
            sfxSource.clip = soundList[Random.Range(0,soundList.Count)];
            sfxSource.Play();
            currentTime=0;
            currentInterval=Random.Range(intervalMin, intervalMax);
        }
    }

}
