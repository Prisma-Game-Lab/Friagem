using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Flash : MonoBehaviour
{

    public string BotaoFlash;
    public bool ilumina = false;
    public bool sistemaDeCargas = false;
    public float tempoIluminado;
    public float tempoDeRecarga;
    public Vector3 posFlash;
    public int totalFlares;

    public GameObject Flare;
    public GameObject Lanterna;

    public AudioSource flashSound;
    public AudioSource heartbeat;
    public TextMeshProUGUI flareText;

    private int numFlares;
    private bool carregado = true;

    // Start is called before the first frame update
    void Start()
    {
        numFlares = totalFlares;
        flareText.text = "Level Flares: " + numFlares.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(BotaoFlash))
        {
            flashSound.Play();
            heartbeat.Play();

            if(!sistemaDeCargas && carregado)
            {
                StartCoroutine(FlareCoolDown());
                StartCoroutine(Luz());
            }
            else if(sistemaDeCargas && numFlares > 0)
            {
                StartCoroutine(Luz());
                numFlares = numFlares - 1;
                flareText.text = "Level Flares: " + numFlares.ToString();
                Debug.Log(numFlares);
            }
        }
    }

    private IEnumerator Luz()
    {
        Debug.Log("Flash");
        posFlash = transform.position;
        ilumina = true;
        Flare.SetActive(true);
        Lanterna.SetActive(false);
        yield return new WaitForSeconds(tempoIluminado);
        ilumina = false;
        Flare.SetActive(false);
        Lanterna.SetActive(true);
    }
    private IEnumerator FlareCoolDown()
    {
        carregado = false;
        yield return new WaitForSeconds(tempoDeRecarga);
        carregado = true;
    }
}
