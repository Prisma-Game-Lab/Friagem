﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{

    public string BotaoFlash;
    public bool ilumina = false;
    public float tempoIluminado;
    public Vector3 posFlash;

    public AudioSource flashSound;
    public AudioSource heartbeat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(BotaoFlash))
        {
            flashSound.Play();
            heartbeat.Play();
            StartCoroutine(Luz());
        }
    }

    private IEnumerator Luz()
    {
        Debug.Log("Flash");
        posFlash = transform.position;
        ilumina = true;
        yield return new WaitForSeconds(tempoIluminado);
        ilumina = false;
    }
}
