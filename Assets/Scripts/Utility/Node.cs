using UnityEngine;

public class Node : IHeapItem<Node>
{
    public Node parent;

    public bool IsWalkable;
    public Vector3 worldPosition;

    public int gridX, gridY;

    // ����� ������� ������ ���.
    public int gCost;
    // ��尡 ���������� ����Ǵ� ���.
    public int hCost; 

    //����� ����
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
        /*      -1 : Fcost < other.Fcost(�켱������ ���� ����)
         *      0 : Fcost == other.Fcost(�켱������ ����) 
         *      -1 : Fcost > other.Fcost(�켱������ other�� ����)      
         */
        int compare = Fcost.CompareTo(other.Fcost);
        
        if (compare == 0) // ���ٸ� hCost�� ���� ������.
            compare = hCost.CompareTo(other.hCost);

        return compare;
    }
}
