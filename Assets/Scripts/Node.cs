using UnityEngine;

public class Node
{
    public Vector3 worldPosition;

    public int gridX, gridY;

    public Node(Vector3 pos, int x, int y)
    {
        worldPosition = pos;
        gridX = x;
        gridY = y;
    }
}
