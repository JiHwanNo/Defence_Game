using System.Collections.Generic;
using UnityEngine;

public class _Grid : MonoBehaviour
{
    enum Vertex
    {
        LEFT_DOWN,
        LEFT_UP,
        RIGHT_DOWN,
        RIGHT_UP,
        CENTER,
        MAX
    }

    public LayerMask unwalkableMask;

    Node[,] grid;

    Vector3[] vertices;
    Vector2 gridSize;
    float grid_Width;
    float grid_Height;

    float nodeRadius;              //��� ������
    float nodeDiameter;            //��� ����
    int gridXCount, gridYCount;   //Grid X�� Node ����, Grid Y�� Node ����
    // ���� �������� ��ġ�� �޾� Grid�� ������ Ȯ����Ų��.
    //��ü Node ũ�Ⱚ
    public int MaxSize { get => gridXCount * gridYCount; }


    private void Start()
    {
        nodeRadius = 1f;
        nodeDiameter = nodeRadius * 2f;

        Set_Vertex();

        // ���� ����
        grid_Width = Mathf.Abs(vertices[(int)Vertex.LEFT_DOWN].x - vertices[(int)Vertex.RIGHT_DOWN].x );
        grid_Height = Mathf.Abs(vertices[(int)Vertex.LEFT_DOWN].z - vertices[(int)Vertex.LEFT_UP].z );
        gridSize = new Vector2(grid_Width, grid_Height);

        gridXCount = Mathf.RoundToInt(gridSize.x / nodeDiameter);
        gridYCount = Mathf.RoundToInt(gridSize.y / nodeDiameter);

        CreateGrid();
    }
    /// <summary>
    /// set Ground Grid Vertex point
    /// </summary>
    void Set_Vertex()
    {
        vertices = new Vector3[(int)Vertex.MAX];

        vertices[(int)Vertex.LEFT_DOWN] = new Vector3(-50 - nodeRadius, 0, -100 - nodeRadius);
        vertices[(int)Vertex.LEFT_UP] = new Vector3(-50 - nodeRadius, 0, 100 + nodeRadius);
        vertices[(int)Vertex.RIGHT_UP] = new Vector3(50 + nodeRadius, 0, 100 + nodeRadius);
        vertices[(int)Vertex.RIGHT_DOWN] = new Vector3(50 + nodeRadius, 0, -100 - nodeRadius);
        vertices[(int)Vertex.CENTER] = new Vector3(0, 0, 0);
        transform.position = vertices[(int)Vertex.CENTER];
    }

    public List<Node> GetNearByNode(Node currentNode)
    {
        List<Node> NearByNodes = new List<Node>();

        // �ش�Ǵ� ����� �پ��ִ� ��带 üũ�Ͽ� List�� ��´�.
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                // ���� �ڽ��̸� �Ѿ��.
                if (x == 0 && y == 0)
                    continue;

                int check_X = currentNode.gridX + x;
                int check_Y = currentNode.gridY + y;
                // ������ �ֺ���尡 �´ٸ� List�� �����Ѵ�.
                if ((0 <= check_X && check_X < gridXCount) &&
                    (0 <= check_Y && check_Y < gridYCount))
                    NearByNodes.Add(grid[check_X, check_Y]);
            }
        }
        return NearByNodes;
    }

    /// <summary>
    /// Creating Node Function
    /// </summary>
    private void CreateGrid()
    {
        grid = new Node[gridXCount, gridYCount];
        // ������ �������� �߰� �����̾�� �Ѵ�.
        Vector3 worldBottomLeft =transform.position - new Vector3((gridSize.x * 0.5f), 0, (gridSize.y * 0.5f));
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

        Debug.Log($"percet_X : {percen_X} , percent_Y : {percen_Y}");

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
                Gizmos.color = Color.white;

                Gizmos.DrawCube(node.worldPosition, new Vector3(nodeDiameter, 1, nodeDiameter));
            }
        }
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, 1f, gridSize.y));
    }

}
