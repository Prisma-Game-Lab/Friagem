using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSound : MonoBehaviour
{
    public float soundRadius;
    public List<AudioClip> monsterClips;
    private AudioSource source;
    private void Awake() 
    {
        source=GetComponent<AudioSource>();
    }

    public void PlaySound(float distance)
    {
        if(distance < soundRadius && !source.isPlaying)
        {
            source.clip = monsterClips[Random.Range(0,monsterClips.Count)];
            source.volume = PlayerPrefs.GetFloat("MainPref") * PlayerPrefs.GetFloat("BackgorundPref");
            source.Play();
        }
    }

}
