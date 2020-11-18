using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{

    public string BotaoFlash;
    public bool ilumina = false;
    public bool sistemaDeCargas = false;
    public float tempoIluminado;
    public float tempoDeRecarga;
    public Vector3 posFlash;
    public int totalFlares;
    

    public AudioSource flashSound;
    public AudioSource heartbeat;

    private int numFlares;
    private bool carregado = true;

    // Start is called before the first frame update
    void Start()
    {
        numFlares = totalFlares;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(BotaoFlash) && !sistemaDeCargas && carregado)//numero de flares ilimitados com tempo para recarga
        {
            flashSound.Play();
            heartbeat.Play();
            StartCoroutine(Luz());
            StartCoroutine(FlareCoolDown());
        }
        else if(Input.GetKeyDown(BotaoFlash) && sistemaDeCargas && numFlares > 0)//numero de flares limitados
        {
               flashSound.Play();
               heartbeat.Play();
               StartCoroutine(Luz());
               numFlares = numFlares - 1;
               Debug.Log(numFlares); 
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
    private IEnumerator FlareCoolDown()
    {
        carregado = false;
        yield return new WaitForSeconds(tempoDeRecarga);
        carregado = true;
    }
}
