using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buraco : MonoBehaviour
{
    public GameObject player;

    private Playerpush playerPush;

    private void Start()
    {
        playerPush = player.GetComponent<Playerpush>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Untagged"))
        {
            SoundManager.instance.boxFalling.Play();
            Destroy(gameObject);
            Destroy(collision.gameObject);
            playerPush.SegurandoCaixahorizontal = false;
            playerPush.SegurandoCaixavertical = false;
           
        }
    }
}
