﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


//https://en.wikipedia.org/wiki/A*_search_algorithm
public class Pathfinding2D 
{
    private Tilemap walkableTiles;

    public Pathfinding2D(Tilemap walkable)
    {
        this.walkableTiles=walkable;
    } 

    public List<Vector3> A_Star(Vector3 start, Vector3 finish)
    {
        List<Vector3> shortestPath;

        Vector3Int gridStart = walkableTiles.WorldToCell(new Vector3(start.x,start.y,0));
        Vector3Int gridFinish = walkableTiles.WorldToCell(new Vector3(finish.x,finish.y,0));

        PriorityQueue<GridNode> queue = new PriorityQueue<GridNode>();
        GridNode startNode = new GridNode(walkableTiles,gridStart);
        startNode.gScore=0;
        startNode.cameFrom=null;
        queue.Enqueue(startNode);
        while(queue.Count()>0)
        {
            GridNode currentNode = queue.Dequeue();
            if(currentNode.pos == gridFinish)
            {
                //Achou o destino
                shortestPath = new List<Vector3>();
                do
                {
                    shortestPath.Insert(0,(walkableTiles.CellToWorld(currentNode.pos)) + new Vector3(0.5f,0.5f,0));
                    currentNode=currentNode.cameFrom;
                } while (currentNode.cameFrom != null);
                return shortestPath;
            }
            List<GridNode> neighbors = currentNode.GetNeighbors();
            foreach (GridNode n in neighbors)
            {
                int novoGScore = currentNode.gScore + 1;
                if(n.gScore > novoGScore)
                {
                    n.gScore=novoGScore;
                    n.cameFrom=currentNode;

                    if(!queue.Contains(n))
                    {
                        queue.Enqueue(n);
                    }
                }
            }
        }
        return null;
    }
}

/////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////
public class GridNode : IComparable<GridNode>
{
    
    public bool visited;
    public int gScore, fScore;
    public Vector3Int pos;
    public GridNode cameFrom;
    private Tilemap walkableMap;
    private List<GridNode> neighbors;

    static Dictionary<Vector3Int, GridNode> allTiles = new Dictionary<Vector3Int, GridNode>();

    public GridNode(Tilemap tileMap, Vector3Int pos)
    {
        this.walkableMap = tileMap;
        this.pos=pos;
        this.visited=false;
        this.neighbors=null;
        this.gScore=2147483647;
        this.fScore=2147483647;
        this.cameFrom = null;
    }

    int IComparable<GridNode>.CompareTo(GridNode other)
    {
        return this.gScore - other.gScore;
    }

    public List<GridNode> GetNeighbors()
    {
        if(this.neighbors == null)
        {
            this.neighbors = new List<GridNode>();
            Vector3Int[] candidatos= new Vector3Int[4];
            candidatos[0]= new Vector3Int(pos.x+1,pos.y,pos.z);
            candidatos[1]= new Vector3Int(pos.x-1,pos.y,pos.z);
            candidatos[2]= new Vector3Int(pos.x,pos.y+1,pos.z);
            candidatos[3]= new Vector3Int(pos.x,pos.y-1,pos.z);

            for(int i=0; i<4;i++)
            {
                if(walkableMap.HasTile(candidatos[i])) //verificar se tem caixa/monstros tambem
                {
                    GridNode instance;
                    if(!allTiles.TryGetValue(candidatos[i],out instance))
                    {
                        instance=new GridNode(walkableMap,candidatos[i]);
                        allTiles.Add(candidatos[i],instance);
                    }
                    this.neighbors.Add(instance);
                }

            }
        }
        return this.neighbors;

    }

}
