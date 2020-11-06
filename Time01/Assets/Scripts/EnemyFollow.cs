using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public float GridSize;
    public float Speed;
    public GameObject player;

    private bool Moving = false;
    private Vector3 NextPos;


    private NavMeshAgent agent;
    private Flash flash;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        flash = player.GetComponent<Flash>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(flash.ilumina)
        {
            agent.SetDestination(flash.posFlash);
        }
    }

    private IEnumerator MoveCooldown()
    {
        Moving = true;
        while (transform.position != transform.position)
        {
            transform.position = Vector3.Lerp(transform.position, NextPos, Speed * Time.deltaTime); //Move para a direção alvo. -A
            yield return null;
        }

        transform.position = NextPos; //Tira o erro do movimento. -A

        Moving = false;
    }
}
