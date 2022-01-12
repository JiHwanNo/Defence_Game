using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    Vector2 gridSize;

    Node[,] grid;

    float nodeRadius;              //노드 반지름
    float nodeDiameter;            //노드 지름
    int gridXCount, gridYCount;   //Grid X축 Node 갯수, Grid Y축 Node 갯수
    private void Awake()
    {
        nodeRadius = 0.5f;
        nodeDiameter = nodeRadius * 2f;
        gridSize = new Vector2(30, 30);

        gridXCount = Mathf.RoundToInt(gridSize.x / nodeDiameter);
        gridYCount = Mathf.RoundToInt(gridSize.y / nodeDiameter);

    }
    private void Start()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        grid = new Node[gridXCount, gridYCount]; // [30,30]

        Vector3 worldBottomLeft = transform.position - new Vector3((gridSize.x * 0.5f), 0, (gridSize.y * 0.5f));

        for (int x = 0; x < gridXCount; x++)
        {
            for (int y = 0; y < gridYCount; y++)
            {
                Vector3 worldPosition = worldBottomLeft + new Vector3((x * nodeDiameter + nodeRadius), 0, (y * nodeDiameter + nodeRadius));

                grid[x, y] = new Node(worldPosition, x, y);
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, 1f, gridSize.y));

        if(grid != null)
        {
            foreach(var node in grid)
            {
                Gizmos.color = Color.white;

                Gizmos.DrawCube(node.worldPosition, Vector3.one * nodeDiameter);
            }
        }
    }
}
