using UnityEngine;

public class Node
{
    public bool IsWalkable;
    public Vector3 worldPosition;

    public int gridX, gridY;

    public Node(bool walkable, Vector3 pos, int x, int y)
    {
        this.IsWalkable = walkable;
        worldPosition = pos;
        gridX = x;
        gridY = y;
    }
}
