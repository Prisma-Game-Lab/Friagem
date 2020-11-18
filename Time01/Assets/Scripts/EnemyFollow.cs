using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class EnemyFollow : MonoBehaviour
{
    public float GridSize;
    public float Speed;
    public GameObject player;

    public Tilemap ground;


    private bool Moving = false;
    private Flash flash;


    // Start is called before the first frame update
    void Start()
    {
        flash = player.GetComponent<Flash>();
    }

    // Update is called once per frame
    void Update()
    {        
        if (!Moving)
        {
            StartCoroutine(MoveCooldown(player.transform.position));
        }
    }

    private IEnumerator MoveCooldown(Vector3 target)
    {
        Moving = true;

        /*A forma mais eficiente é rodar isso a partir da posicao do flash*/
        Vector3 myPos=transform.position;
        List<Vector3> path = new Pathfinding2D(ground).A_Star(myPos,target);
        foreach (Vector3 NextPos in path)
        {
            if(player.transform.position != target)
            {
                path = new Pathfinding2D(ground).A_Star(myPos, target);
            }
            else
            {
                if (!flash.ilumina)
                {
                    transform.position = NextPos; //Move para a direção alvo. -A
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
       

        //transform.position = NextPos; //Tira o erro do movimento. -A

        Moving = false;
    }
}

/*
^^^^^^^^^^ 
caminho = []
while(iluminado)
    move para caminho[i]
    i+=1
    espera x segundos
*/
