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

    private MonsterSound ms;
    private bool Moving = false;
    private bool CanMove = false;
    private Flash flash;


    // Start is called before the first frame update
    void Start()
    {
        flash = player.GetComponent<Flash>();
        ms = GetComponent<MonsterSound>();
        StartCoroutine(EnableMove());
    }

    // Update is called once per frame
    void Update()
    {        
        if (!Moving && CanMove)
        {
            StartCoroutine(MoveCooldown(player.transform.position));
        }
    }

    private IEnumerator EnableMove()
    {
        yield return new WaitForSeconds(6.0f);
        CanMove = true;
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
                    ms.PlaySound(Vector3.Distance(transform.position, player.transform.position));
                    yield return new WaitForSeconds(Speed);
                }
            }
        }

        Moving = false;
    }
}
