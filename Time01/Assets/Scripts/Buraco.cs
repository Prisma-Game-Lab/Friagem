using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buraco : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Box"))
        {
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
        }
    }
}
