using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.Tilemaps;

public class Move : MonoBehaviour
{

    [SerializeField]
    private Tilemap ground;
    [SerializeField]
    private Tilemap obstaculos;
    private Vector3 MoveHor;
    private Vector3 MoveVer;
    private Vector3 TargetPos;
    private bool Moving = false;
    private Playerpush playerPush;

    public float Speed;
    public float GridSize;
    public float cooldown;

    public AudioSource passo1;
    public AudioSource passo2;
    private int qualPasso = 1;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        //Distância fixa do movimento.
        MoveHor = new Vector3(GridSize, 0f, 0f);
        MoveVer = new Vector3(0f, GridSize, 0f);

        playerPush = GetComponent<Playerpush>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Variáveis para guardar a direção do movimento. -A
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if(!Moving)
        {
            if (h != 0 && !(playerPush.SegurandoCaixavertical))
            {
                if(playerPush.Stop[0] && h > 0)
                {
                    TargetPos = transform.position;
                }
                else if(playerPush.Stop[1] && h < 0)
                {
                    TargetPos = transform.position;
                }
                else
                {
                    anim.SetFloat("Horizontal", h);
                    anim.SetFloat("Vertical", 0f);
                    TargetPos = transform.position + h * MoveHor;
                }
                
                if (CanMove(TargetPos)) //Quando implementar a arte no tilemap usar CanMove()
                {
                    if(qualPasso == 1)
                    {
                        qualPasso=2;
                        passo1.Play();
                    }
                    else
                    {
                        qualPasso = 1;
                        passo2.Play();
                    }
                    StartCoroutine(MoveCooldown());
                }
            }
            else if (v != 0 && !(playerPush.SegurandoCaixahorizontal))
            {
                if (playerPush.Stop[2] && v > 0)
                {
                    TargetPos = transform.position;
                }
                else if (playerPush.Stop[3] && v < 0)
                {
                    TargetPos = transform.position;
                }
                else
                {
                    anim.SetFloat("Vertical", v);
                    anim.SetFloat("Horizontal", 0f);
                    TargetPos = transform.position + v * MoveVer;
                }
                if (CanMove(TargetPos)) //Quando implementar a arte no tilemap usar CanMove()
                {
                    if (qualPasso == 1)
                    {
                        qualPasso = 2;
                        passo1.Play();
                    }
                    else
                    {
                        qualPasso = 1;
                        passo2.Play();
                    }
                    StartCoroutine(MoveCooldown());
                }
            }
        }       
    }

    private bool CanMove(Vector3 direction)
    {
        Vector3Int gridPos = ground.WorldToCell(direction);
        if (transform.childCount > 1)
        {
            Vector3 childDir =  transform.GetChild(1).position;
            direction -= transform.position;
            direction += childDir;
            Vector3Int childGridPos = ground.WorldToCell(direction);
            if ((obstaculos.HasTile(childGridPos) || obstaculos.HasTile(gridPos)) || (!ground.HasTile(childGridPos) || !ground.HasTile(gridPos)))
            {
                return false;
            }
        }
        if (obstaculos.HasTile(gridPos) || !ground.HasTile(gridPos))
        {
            return false;
        }
        return true;
    }

    //Corrotina para restringir movimento. -A
    private IEnumerator MoveCooldown()
    {
        Moving = true;
        anim.SetBool("andando", true);
        while(transform.position != TargetPos)
        {
            transform.position = Vector3.Lerp(transform.position, TargetPos, Speed * Time.deltaTime); //Move para a direção alvo. -A
            yield return null;
        }

        transform.position = TargetPos; //Tira o erro do movimento. -A
        yield return new WaitForSeconds(cooldown);
        Moving = false;
        anim.SetBool("andando", false);
    }
}
