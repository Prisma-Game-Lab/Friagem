using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{

    public string BotaoFlash;
    public bool ilumina = false;
    public float tempoIluminado;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(BotaoFlash))
        {
            StartCoroutine(Luz());
        }
    }

    private IEnumerator Luz()
    {
        Debug.Log("Flash");
        ilumina = true;
        yield return new WaitForSeconds(tempoIluminado);
        ilumina = false;
    }
}
