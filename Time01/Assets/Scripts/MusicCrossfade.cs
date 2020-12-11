using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicCrossfade : MonoBehaviour
  {
    public AudioClip music;
    private AudioSource[] aud = new AudioSource[2];
    private bool activeAudioSourceIndex;
    private AudioSource activeAudioSource;
    private AudioSource nextAudioSource;
    IEnumerator musicTransition = null;
 
    void Awake () {
        aud[0] = gameObject.AddComponent<AudioSource>();
        aud[1] = gameObject.AddComponent<AudioSource>();
        aud[0].loop = false;
        aud[1].loop = false;
        activeAudioSource = aud[activeAudioSourceIndex ? 0:1];
        nextAudioSource = aud[activeAudioSourceIndex ? 1:0];
    }
 
    void Update() {
        
        if(music.length - activeAudioSource.time <= 2.0f)
        {
            newSoundtrack(music);
        }
    }
    public void newSoundtrack (AudioClip clip) {
 
        //If a transition is already happening, we stop it here to prevent our new Coroutine from competing
        if (musicTransition != null)
            StopCoroutine(musicTransition);
 
        nextAudioSource.clip = clip;
        nextAudioSource.Play();
 
        musicTransition = transition(20); //20 is the equivalent to 2 seconds (More than 3 seconds begins to overlap for a bit too long)
        StartCoroutine(musicTransition);
    }
 
        //  'transitionDuration' is how many tenths of a second it will take, eg, 10 would be equal to 1 second
    IEnumerator transition(int transitionDuration) {
 
        for (int i = 0; i < transitionDuration+1; i++) {
            var vol = (transitionDuration - i) * (1f / transitionDuration);
            activeAudioSource.volume = vol;
            nextAudioSource.volume = 1-vol;
 
 
            yield return new WaitForSecondsRealtime(0.1f);
            //use realtime otherwise if you pause the game you could pause the transition half way
        }
 
        //finish by stopping the audio clip on the now silent audio source
        activeAudioSource.Stop();
 
        activeAudioSourceIndex = !activeAudioSourceIndex;
        activeAudioSource = aud[activeAudioSourceIndex ? 0:1];
        nextAudioSource = aud[activeAudioSourceIndex ? 1:0];
        musicTransition = null;
    }
}