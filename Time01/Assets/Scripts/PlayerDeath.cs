using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour

{
    public bool dead = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("TriggerEnter");
        if(other.gameObject.CompareTag("Enemy"))
        {
            dead = true;

        }
    }

}
