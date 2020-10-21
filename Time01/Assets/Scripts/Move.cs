using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class Move : MonoBehaviour
{
 
    private Vector3 MoveHor;
    private Vector3 MoveVer;
    private Vector3 TargetPos;
    private bool moving = false;

    public float speed;
    public float GridSize;


    // Start is called before the first frame update
    void Start()
    {
        //Distância fixa do movimento.
        MoveHor = new Vector3(GridSize, 0f, 0f);
        MoveVer = new Vector3(0f, GridSize, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        //Variáveis para guardar a direção do movimento. -A
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if(!moving)
        {
            if (h != 0)
            {
                TargetPos = transform.position + h * MoveHor;
                StartCoroutine(MoveCooldown());
            }
            else if (v != 0)
            {
                TargetPos = transform.position + v * MoveVer;
                StartCoroutine(MoveCooldown());
            }
        }       
    }


    //Corrotina para restringir movimento. -A
    private IEnumerator MoveCooldown()
    {
        moving = true;
        while(transform.position != TargetPos)
        {
            transform.position = Vector3.Lerp(transform.position, TargetPos, speed * Time.deltaTime); //Move para a direção alvo. -A
            yield return null;
        }

        transform.position = TargetPos; //Tira o erro do movimento. -A

        moving = false;
    }
}
