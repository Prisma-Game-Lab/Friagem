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
       }
       else{
           Destroy(this.gameObject);
       }
   }

   private void Awake() {
       boxFalling = transform.GetChild(0).GetComponent<AudioSource>();
       DontDestroyOnLoad(transform.gameObject);
   }
}
