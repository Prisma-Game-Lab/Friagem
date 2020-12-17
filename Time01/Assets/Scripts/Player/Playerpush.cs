using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerpush : MonoBehaviour
{

    public string BotaoCaixa;
    public float distance;
    public LayerMask boxMask;
    public bool SegurandoCaixahorizontal = false;
    public bool SegurandoCaixavertical = false;

    //variaveis para verificar se as casas adjacentes estão ocupadas
    //[HideInInspector]
    public bool[] Stop;

    private GameObject box;
    public GameObject Player;

    private Move move;
    private int i;
    private int d;
    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<Move>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(SegurandoCaixahorizontal== false && SegurandoCaixavertical == false)
        {
            d = move.d;
            Debug.Log(d);
        }


        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, distance, boxMask);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, distance, boxMask);
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, distance, boxMask);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, distance, boxMask);

        RaycastHit2D[] hits = { hitRight, hitLeft, hitUp, hitDown };

        for (i = 0; i < 4; i++)
        {
            if (hits[i].collider != null && hits[i].collider.gameObject.tag == "Box" && Input.GetKeyDown(BotaoCaixa) && i==d)
            {
                box = hits[i].collider.gameObject;
                Debug.Log(hits[i].collider.name);
                Debug.Log(i);
                box.transform.SetParent(Player.transform);
                box.tag = "Untagged";
                /*if (hits[i] == hitRight || hits[i] == hitLeft)// comparar usando a lista nao estava funcionando, entao usei os indices
                {
                    SegurandoCaixahorizontal = true;
                    Debug.Log("horizontal");
                }
                else
                {
                    SegurandoCaixavertical = true;
                    Debug.Log("vertical");
                }*/

                if (i==0 || i==1)
                {
                    SegurandoCaixahorizontal = true;
                    Debug.Log("horizontal");
                }
                else
                {
                    SegurandoCaixavertical = true;
                    Debug.Log("vertical");
                }

                //if(box != null)
                //{
                //return;
                //}
            }
            else if (hits[i].collider != null && hits[i].collider.gameObject.tag == "Untagged" && Input.GetKeyUp(BotaoCaixa))
            {
                box.transform.SetParent(null);
                SegurandoCaixahorizontal = false;
                SegurandoCaixavertical = false;
                hits[i].collider.gameObject.tag = "Box";
            }
        }
    }



    //desenha as raios de colisao parao player
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(transform.position, ((Vector2)transform.position + Vector2.right * distance));
        Gizmos.DrawLine(transform.position, ((Vector2)transform.position + Vector2.left * distance));
        Gizmos.DrawLine(transform.position, ((Vector2)transform.position + Vector2.up * distance));
        Gizmos.DrawLine(transform.position, ((Vector2)transform.position + Vector2.down * distance));
    }


}