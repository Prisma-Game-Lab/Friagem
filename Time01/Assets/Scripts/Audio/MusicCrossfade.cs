using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicCrossfade : MonoBehaviour
  {
    [SerializeField] private float crossFadeTime;
    [SerializeField] private AudioClip music;
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

        aud[0].playOnAwake = false;
        aud[1].playOnAwake = false;
        
        activeAudioSource = aud[activeAudioSourceIndex ? 0:1];
        nextAudioSource = aud[activeAudioSourceIndex ? 1:0];
        activeAudioSource.clip = music;
        activeAudioSource.volume = AudioConfig.mainVol * AudioConfig.backgroundVol;
        activeAudioSource.Play();
    }
 
    void Update() {
        
        if(music.length - activeAudioSource.time <= crossFadeTime)
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
 
        musicTransition = transition(crossFadeTime); 
        StartCoroutine(musicTransition);
    }
 
        //  'transitionDuration' is how many tenths of a second it will take, eg, 10 would be equal to 1 second
    IEnumerator transition(float transitionDuration) {
 
        for (float i = 0.0f; i <= transitionDuration; i+=0.01f) {
            var vol = (transitionDuration - i) * (1f / transitionDuration) * AudioConfig.mainVol * AudioConfig.backgroundVol;
            activeAudioSource.volume = vol;
            nextAudioSource.volume = 1-vol;
 
 
            yield return new WaitForSecondsRealtime(0.01f);
            //use realtime otherwise if you pause the game you could pause the transition half way
        }
 
        //finish by stopping the audio clip on the now silent audio source
        activeAudioSource.Stop();
        activeAudioSource.volume = 0.0f;
        nextAudioSource.volume = 1.0f * AudioConfig.mainVol * AudioConfig.backgroundVol;
 
        activeAudioSourceIndex = !activeAudioSourceIndex;
        activeAudioSource = aud[activeAudioSourceIndex ? 0:1];
        nextAudioSource = aud[activeAudioSourceIndex ? 1:0];
        musicTransition = null;
    }
}