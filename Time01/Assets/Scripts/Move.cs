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
    private int lastPasso = -1;
    private bool Moving = false;
    private Playerpush playerPush;
    private Animator anim;

    public float Speed;
    public float GridSize;
    public float cooldown;
    public List<AudioSource> passos;
    public AudioSource colisaoSound;
    public LayerMask pitMask;
    


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

        RaycastHit2D hitHc = Physics2D.Raycast(transform.position, h * MoveHor, playerPush.distance, playerPush.boxMask);
        RaycastHit2D hitVc = Physics2D.Raycast(transform.position, v * MoveVer, playerPush.distance, playerPush.boxMask);
        RaycastHit2D hitHb = Physics2D.Raycast(transform.position, h * MoveHor, playerPush.distance, pitMask);
        RaycastHit2D hitVb = Physics2D.Raycast(transform.position, v * MoveVer, playerPush.distance, pitMask);


        if (!Moving)
        {
            if (h != 0 && !(playerPush.SegurandoCaixavertical))
            {
                anim.SetFloat("Horizontal", h);
                anim.SetFloat("Vertical", 0f);
                if ((hitHc.collider != null && hitHc.collider.gameObject.tag == "Box") || (hitHb.collider != null && hitHb.collider.gameObject.tag == "Wall"))
                {
                    TargetPos = transform.position;
                }
                else
                {
                    TargetPos = transform.position + h * MoveHor;
                }

                if (CanMove(TargetPos)) //Quando implementar a arte no tilemap usar CanMove()
                {
                    PlayStepSound();
                    StartCoroutine(MoveCooldown());
                }
                else
                {
                    if(!colisaoSound.isPlaying)
                    {
                        colisaoSound.Play();
                    }
                }
            }
            else if (v != 0 && !(playerPush.SegurandoCaixahorizontal))
            {
                anim.SetFloat("Vertical", v);
                anim.SetFloat("Horizontal", 0f);
                if ((hitVc.collider != null && hitVc.collider.gameObject.tag == "Box") || (hitVb.collider != null && hitVb.collider.gameObject.tag == "Wall"))
                {
                    TargetPos = transform.position;
                }
                else
                {
                    TargetPos = transform.position + v * MoveVer;
                }

                if (CanMove(TargetPos)) //Quando implementar a arte no tilemap usar CanMove()
                {
                    PlayStepSound();
                    StartCoroutine(MoveCooldown());
                }
                else
                {
                    if(!colisaoSound.isPlaying)
                    {
                        colisaoSound.Play();
                    }
                }
            }
        }
    }

    private bool CanMove(Vector3 direction)
    {
        Vector3Int gridPos = ground.WorldToCell(direction);

        if (transform.childCount > 2)
        {
            Vector3 childDir =  transform.GetChild(2).position;
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

    private void PlayStepSound()
    {
        int qualPasso = Random.Range(0,passos.Count);
        if(qualPasso == lastPasso)
        {
            qualPasso += 1;
            qualPasso = qualPasso%passos.Count;
        }
        lastPasso = qualPasso;
        passos[qualPasso].Play();
    }
}
