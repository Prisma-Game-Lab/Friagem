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
    [HideInInspector]
    public bool[] Stop;

    private GameObject box;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, distance, boxMask);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, distance, boxMask);
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, distance, boxMask);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, distance, boxMask);

        RaycastHit2D[] hits = { hitRight, hitLeft, hitUp, hitDown };

        for (int i = 0; i < 4; i++)
        {
            if (hits[i].collider != null && hits[i].collider.gameObject.tag == "Box" && Input.GetKeyDown(BotaoCaixa))
            {
                box = hits[i].collider.gameObject;
                Debug.Log(hits[i].collider.name);
                box.transform.SetParent(Player.transform);
                if (hits[i] == hitRight || hits[i] == hitLeft)
                {
                    SegurandoCaixahorizontal = true;
                }
                else
                {
                    SegurandoCaixavertical = true;
                }
            }
            else if (hits[i].collider != null && hits[i].collider.gameObject.tag == "Box" && Input.GetKeyUp(BotaoCaixa))
            {
                box.transform.SetParent(null);
                SegurandoCaixahorizontal = false;
                SegurandoCaixavertical = false;
            }
            else if (hits[i].collider != null && hits[i].collider.gameObject.tag == "Box")
            {
                Stop[i] = true;
                if (Input.GetKey(BotaoCaixa))
                {
                    Stop[i] = false;
                }
            }
            else
            {
                Stop[i] = false;
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