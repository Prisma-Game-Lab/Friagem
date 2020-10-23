using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{

    public float Speed;

    public GameObject player;

    public float GridSize;

    private bool Moving = false;

    private Vector3 NextPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!Moving && player.GetComponent<Flash>().ilumina && Vector3.Distance(transform.position, player.transform.position) > 1)
        {
            if (transform.position.x < player.transform.position.x)
            {
                NextPos = transform.position + new Vector3(GridSize, 0f, 0f);
                StartCoroutine(MoveCooldown());
            }
            else if (transform.position.x > player.transform.position.x)
            {
                NextPos = transform.position - new Vector3(GridSize, 0f, 0f);
                StartCoroutine(MoveCooldown());
            }
            else if (transform.position.y < player.transform.position.y)
            {
                NextPos = transform.position + new Vector3(0f, GridSize, 0f);
                StartCoroutine(MoveCooldown());
            }
            else if (transform.position.y > player.transform.position.y)
            {
                NextPos = transform.position - new Vector3(0f, GridSize, 0f);
                StartCoroutine(MoveCooldown());
            }
        }
        
        /*if(Vector3.Distance(transform.position, player.transform.position) > 1 && player.GetComponent<Flash>().ilumina)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }*/
    }

    private IEnumerator MoveCooldown()
    {
        Moving = true;
        while (transform.position != NextPos)
        {
            transform.position = Vector3.Lerp(transform.position, NextPos, Speed * Time.deltaTime); //Move para a direção alvo. -A
            yield return null;
        }

        transform.position = NextPos; //Tira o erro do movimento. -A

        Moving = false;
    }
}
