using UnityEngine;

public class Grid : MonoBehaviour
{
    GameObject player;
    
    public LayerMask unwalkableMask;
    Vector2 gridSize;

    Node[,] grid;

    float nodeRadius;              //노드 반지름
    float nodeDiameter;            //노드 지름
    int gridXCount, gridYCount;   //Grid X축 Node 갯수, Grid Y축 Node 갯수
    private void Awake()
    {
        player = GameObject.Find("Player");

        nodeRadius = 0.5f;
        nodeDiameter = nodeRadius * 2f;
        gridSize = new Vector2(30, 30);

        gridXCount = Mathf.RoundToInt(gridSize.x / nodeDiameter);
        gridYCount = Mathf.RoundToInt(gridSize.y / nodeDiameter);

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

                bool walkable = !(Physics.CheckSphere(worldPosition, nodeRadius, unwalkableMask));

                grid[x, y] = new Node(walkable, worldPosition, x, y);
            }
        }

    }

    Node GetNodeFromWorldPosition(Vector3 WP)
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

        if (grid != null)
        {
            foreach (var node in grid)
            {
                if (node == GetNodeFromWorldPosition(player.transform.position))
                    Gizmos.color = Color.green;
                else
                    Gizmos.color = node.IsWalkable ? Color.white : Color.red;

                Gizmos.DrawCube(node.worldPosition, Vector3.one * nodeDiameter);
            }
        }
    }
}
