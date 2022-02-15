using UnityEngine;

public class Node : IHeapItem<Node>
{
    public Node parent;

    public bool IsWalkable;
    public Vector3 worldPosition;

    public int gridX, gridY;

    // 노드의 현재까지 움직인 비용.
    public int gCost;
    // 노드가 도착점까지 예상되는 비용.
    public int hCost; 

    //비용의 총합
    public int Fcost { get => gCost + hCost; }
    private int heapIndex;
    public int HeapIndex {get => heapIndex; set => heapIndex = value;}

    public Node(bool walkable, Vector3 pos, int x, int y)
    {
        this.IsWalkable = walkable;
        worldPosition = pos;
        gridX = x;
        gridY = y;
    }

    public int CompareTo(Node other)
    {
        /*      -1 : Fcost < other.Fcost(우선순위가 내가 높음)
         *      0 : Fcost == other.Fcost(우선순위가 같음) 
         *      -1 : Fcost > other.Fcost(우선순위가 other이 높음)      
         */
        int compare = Fcost.CompareTo(other.Fcost);
        
        if (compare == 0) // 같다면 hCost를 통해 비교하자.
            compare = hCost.CompareTo(other.hCost);

        return compare;
    }
}
