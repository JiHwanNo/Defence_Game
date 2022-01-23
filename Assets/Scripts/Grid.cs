using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public LayerMask unwalkableMask;

    Node[,] grid;

    Vector2 gridSize;
    float nodeRadius;              //노드 반지름
    float nodeDiameter;            //노드 지름
    int gridXCount, gridYCount;   //Grid X축 Node 갯수, Grid Y축 Node 갯수

    //전체 Node 크기값
    public int MaxSize { get => gridXCount * gridYCount; }

    private void Awake()
    {
        gridSize = new Vector2(40, 40);
        nodeRadius = 0.5f;
        nodeDiameter = nodeRadius * 2f;

        gridXCount = Mathf.RoundToInt(gridSize.x / nodeDiameter);
        gridYCount = Mathf.RoundToInt(gridSize.y / nodeDiameter);

        CreateGrid();

    }
   public List<Node> GetNearByNode(Node currentNode)
    {
        List<Node> NearByNodes = new List<Node>();

        // 해당되는 노드의 붙어있는 노드를 체크하여 List에 담는다.
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                // 본인 자신이면 넘어간다.
                if (x == 0 && y == 0)
                    continue;

                int check_X = currentNode.gridX + x;
                int check_Y = currentNode.gridY + y;
                // 범위내 주변노드가 맞다면 List에 저장한다.
                if ((0 <= check_X && check_X < gridXCount) &&
                    (0 <= check_Y && check_Y < gridYCount))
                    NearByNodes.Add(grid[check_X, check_Y]);
            }
        }
        return NearByNodes;
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
                bool walkable = !(Physics.CheckSphere(worldPosition, nodeRadius, unwalkableMask));

                grid[x, y] = new Node(walkable, worldPosition, x, y);
            }
        }

    }

    public Node GetNodeFromWorldPosition(Vector3 WP)
    {
        float percen_X = (WP.x + gridSize.x * 0.5f) / gridSize.x;
        float percen_Y = (WP.z + gridSize.y * 0.5f) / gridSize.y;

        percen_X = Mathf.Clamp01(percen_X);
        percen_Y = Mathf.Clamp01(percen_Y);

        int x = Mathf.RoundToInt((gridXCount - 1) * percen_X);
        int y = Mathf.RoundToInt((gridYCount - 1) * percen_Y);

        return grid[x, y];
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, 1f, gridSize.y));
    }
}
