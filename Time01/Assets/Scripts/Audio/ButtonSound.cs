using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ButtonSound : MonoBehaviour,  ISelectHandler
{
    private AudioSource buttonSounds;
    private Button button;
    public AudioClip buttonConfirm;
    public AudioClip buttonSelect;

    private void Start() {
        buttonSounds = new AudioSource();
        buttonSounds.playOnAwake = false;
        button = GetComponent<Button>();
        button.onClick.AddListener(Confirm);
    }
    public void Select(){
        buttonSounds.volume = PlayerPrefs.GetFloat("SfxPref") * PlayerPrefs.GetFloat("MainPref");
        buttonSounds.PlayOneShot(buttonSelect);
    }

    public void Confirm(){
        buttonSounds.volume = PlayerPrefs.GetFloat("SfxPref") * PlayerPrefs.GetFloat("MainPref");
        buttonSounds.PlayOneShot(buttonConfirm);
    }

    public void OnSelect(BaseEventData e){
        Select();
    }
}
