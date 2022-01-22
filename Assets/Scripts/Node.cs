using UnityEngine;

public class Node
{
    public bool IsWalkable;
    public Vector3 worldPosition;

    public int gridX, gridY;

    // 노드의 현재까지 움직인 비용.
    public int gCost;
    // 노드가 도착점까지 예상되는 비용.
    public int hCost; 

    //비용의 총합
    public int Fcost { get => gCost + hCost; }

    public Node(bool walkable, Vector3 pos, int x, int y)
    {
        this.IsWalkable = walkable;
        worldPosition = pos;
        gridX = x;
        gridY = y;
    }
}
