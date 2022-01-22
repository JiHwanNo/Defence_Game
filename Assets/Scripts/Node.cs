using UnityEngine;

public class Node
{
    public bool IsWalkable;
    public Vector3 worldPosition;

    public int gridX, gridY;

    // ����� ������� ������ ���.
    public int gCost;
    // ��尡 ���������� ����Ǵ� ���.
    public int hCost; 

    //����� ����
    public int Fcost { get => gCost + hCost; }

    public Node(bool walkable, Vector3 pos, int x, int y)
    {
        this.IsWalkable = walkable;
        worldPosition = pos;
        gridX = x;
        gridY = y;
    }
}
