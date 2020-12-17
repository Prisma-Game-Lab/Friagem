using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
   public static SoundManager instance = null;

   public AudioSource boxFalling;

   private void Start() {
       if(instance == null){
            instance = this;
            boxFalling = transform.GetChild(0).GetComponent<AudioSource>();
            DontDestroyOnLoad(transform.gameObject);
       }
       else{
           Destroy(this.gameObject);
       }
   }

   public void PlayBoxFalling(){
        boxFalling.volume = PlayerPrefs.GetFloat("SfxPref") * PlayerPrefs.GetFloat("MainPref");
        boxFalling.Play();
   }
}