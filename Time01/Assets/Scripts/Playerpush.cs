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
    public bool StopRight = false;
    public bool StopLeft = false;
    public bool StopUp = false;
    public bool StopDown = false;

    GameObject box;
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

        //verifica se tem caixa a direita
        if (hitRight.collider!=null && hitRight.collider.gameObject.tag == "Box" && Input.GetKeyDown(BotaoCaixa))
        {
            box = hitRight.collider.gameObject;
            Debug.Log(hitRight.collider.name);
            box.transform.SetParent(Player.transform);
            SegurandoCaixahorizontal = true;
        }
        else if(hitRight.collider != null && hitRight.collider.gameObject.tag == "Box" && Input.GetKeyUp(BotaoCaixa))
        {
            box.transform.SetParent(null);
            SegurandoCaixahorizontal = false;
        }
        else if (hitRight.collider != null && hitRight.collider.gameObject.tag == "Box")
        {
            StopRight = true;
            if (Input.GetKey(BotaoCaixa))
            {
                StopRight = false;
            }
        }
        else
        {
            StopRight = false;
        }

        //verifica se tem caixa a esquerda
        if (hitLeft.collider != null && hitLeft.collider.gameObject.tag == "Box" && Input.GetKeyDown(BotaoCaixa))
        {
            box = hitLeft.collider.gameObject;
            Debug.Log(hitLeft.collider.name);
            box.transform.SetParent(Player.transform);
            SegurandoCaixahorizontal = true;
        }
        else if (hitLeft.collider != null && hitLeft.collider.gameObject.tag == "Box" && Input.GetKeyUp(BotaoCaixa))
        {
            box.transform.SetParent(null);
            SegurandoCaixahorizontal = false;
        }
        else if(hitLeft.collider != null && hitLeft.collider.gameObject.tag == "Box")
        {
            StopLeft = true;
            if (Input.GetKey(BotaoCaixa))
            {
                StopLeft = false;
            }
        }
        else
        {
            StopLeft = false;
        }

        //verifica se tem caixa acima do player
        if (hitUp.collider != null && hitUp.collider.gameObject.tag == "Box" && Input.GetKeyDown(BotaoCaixa))
        {
            box = hitUp.collider.gameObject;
            Debug.Log(hitUp.collider.name);
            box.transform.SetParent(Player.transform);
            SegurandoCaixavertical = true;
        }
        else if (hitUp.collider != null && hitUp.collider.gameObject.tag == "Box" && Input.GetKeyUp(BotaoCaixa))
        {
            box.transform.SetParent(null);
            SegurandoCaixavertical = false;
        }
        else if (hitUp.collider != null && hitUp.collider.gameObject.tag == "Box")
        {
            StopUp = true;
            if (Input.GetKey(BotaoCaixa))
            {
                StopUp = false;
            }
        }
        else
        {
            StopUp = false;
        }

        //verifica se tem caixa abaixo do player
        if (hitDown.collider != null && hitDown.collider.gameObject.tag == "Box" && Input.GetKeyDown(BotaoCaixa))
        {
            box = hitDown.collider.gameObject;
            Debug.Log(hitDown.collider.name);
            box.transform.SetParent(Player.transform);
            SegurandoCaixavertical = true;
        }
        else if (hitDown.collider != null && hitDown.collider.gameObject.tag == "Box" && Input.GetKeyUp(BotaoCaixa))
        {
            box.transform.SetParent(null);
            SegurandoCaixavertical = false;
        }
        else if (hitDown.collider != null && hitDown.collider.gameObject.tag == "Box")
        {
            StopDown = true;
            if (Input.GetKey(BotaoCaixa))
            {
                StopDown = false;
            }
        }
        else
        {
            StopDown = false;
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