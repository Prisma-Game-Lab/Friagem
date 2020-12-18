using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicCrossfade : MonoBehaviour
  {
    [SerializeField] private float crossFadeTime;
    [SerializeField] private List<AudioClip> music;
    private AudioClip currentMusic;
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
        activeAudioSource.clip = music[0];
        activeAudioSource.volume = PlayerPrefs.GetFloat("MainPref") * PlayerPrefs.GetFloat("BackgorundPref");
        currentMusic = music[0];
        activeAudioSource.Play();
    }
 
    void Update() {
        
        if(currentMusic.length - activeAudioSource.time <= crossFadeTime)
        {
            newSoundtrack(currentMusic);
        }
        if(musicTransition == null)
        {
            activeAudioSource.volume = PlayerPrefs.GetFloat("MainPref") * PlayerPrefs.GetFloat("BackgorundPref");;
        }
        switch(SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
            case 2:
            case 3:
                if(currentMusic != music[0]){ currentMusic = music[0]; newSoundtrack(currentMusic); }
                break;
            case 4:
            case 5:
            case 6:               
            case 7:
                if(currentMusic != music[1]){ currentMusic = music[1]; newSoundtrack(currentMusic); }
                break;
            case 8:
            case 9:
            case 10:
            case 11:
                if(currentMusic != music[2]){ currentMusic = music[2]; newSoundtrack(currentMusic); }
                break;
            case 12:
            case 13:
            case 14:
                if(currentMusic != music[3]){ currentMusic = music[3]; newSoundtrack(currentMusic); }
                break;
            case 15:          
            case 16:          
            case 17:
                if(currentMusic != music[4]){ currentMusic = music[4]; newSoundtrack(currentMusic); }
                break;
            case 18:
                if (musicTransition != null){
                    StopCoroutine(musicTransition);
                }
                activeAudioSource.Stop();
                nextAudioSource.Stop();
            break;
                    
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
 
    IEnumerator transition(float transitionDuration) {
 
        for (float i = 0.0f; i <= transitionDuration; i+=0.1f) {
            var vol = (transitionDuration - i) * (1f / transitionDuration) * PlayerPrefs.GetFloat("MainPref") * PlayerPrefs.GetFloat("BackgorundPref");;
            activeAudioSource.volume = vol;
            nextAudioSource.volume = 1-vol;
 
 
            yield return new WaitForSecondsRealtime(0.1f);
            //use realtime otherwise if you pause the game you could pause the transition half way
        }
 
        //finish by stopping the audio clip on the now silent audio source
        activeAudioSource.Stop();
        activeAudioSource.volume = 0.0f;
        nextAudioSource.volume = 1.0f * PlayerPrefs.GetFloat("MainPref") * PlayerPrefs.GetFloat("BackgorundPref");;
 
        activeAudioSourceIndex = !activeAudioSourceIndex;
        activeAudioSource = aud[activeAudioSourceIndex ? 0:1];
        nextAudioSource = aud[activeAudioSourceIndex ? 1:0];
        musicTransition = null;
    }
}