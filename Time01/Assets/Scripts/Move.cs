using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Move : MonoBehaviour
{

    [SerializeField]
    private Tilemap ground;
    [SerializeField]
    private Tilemap collisions;
    private Vector3 MoveHor;
    private Vector3 MoveVer;
    private Vector3 TargetPos;
    private bool Moving = false;

    public float Speed;
    public float GridSize;
    public LayerMask StopmMovement;


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

        if(!Moving)
        {
            if (h != 0)
            {
                TargetPos = transform.position + h * MoveHor;
                if (!Physics2D.OverlapCircle(TargetPos, 0.2f, StopmMovement)) //Quando implementar a arte no tilemap usar CanMove()
                {
                    StartCoroutine(MoveCooldown());
                }
            }
            else if (v != 0)
            {
                TargetPos = transform.position + v * MoveVer;
                if (!Physics2D.OverlapCircle(TargetPos, 0.2f, StopmMovement)) //Quando implementar a arte no tilemap usar CanMove()
                {
                    StartCoroutine(MoveCooldown());
                }
            }
        }       
    }

    private bool CanMove(Vector3 direction)
    {
        Vector3Int gridPos = ground.WorldToCell(direction);
        if (!ground.HasTile(gridPos) || collisions.HasTile(gridPos))
        {
            return false;
        }
        return true;
    }

    //Corrotina para restringir movimento. -A
    private IEnumerator MoveCooldown()
    {
        Moving = true;
        while(transform.position != TargetPos)
        {
            transform.position = Vector3.Lerp(transform.position, TargetPos, Speed * Time.deltaTime); //Move para a direção alvo. -A
            yield return null;
        }

        transform.position = TargetPos; //Tira o erro do movimento. -A

        Moving = false;
    }
}
