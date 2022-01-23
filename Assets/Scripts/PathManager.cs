using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;
public class PathManager : MonoBehaviour
{
    public Transform seeker, target;
    [SerializeField] private Grid grid;
    List<Node> path;
    private void Awake()
    {
        grid = GetComponent<Grid>();
    }
    private void Start()
    {
        FindPath(seeker.position, target.position);
    }
    
    private void FindPath(Vector3 seeker, Vector3 target)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        // openSet�� ��������� ȿ������ Node�� ��� ���� List�̴�.
        Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
        // closeSet�� ������ �Դ� Node �� �Դ� ��,Ȥ�� ��������� ��ȿ������ Node�� ��� ���� HashSet�̴�.
        HashSet<Node> closeSet = new HashSet<Node>();

        Node startNode = grid.GetNodeFromWorldPosition(seeker); // ��� Node[x,y]
        Node targetNode = grid.GetNodeFromWorldPosition(target); // ���� Node[x,y]

        openSet.Add(startNode); // ȿ������ Node�� ������ ���´�.
        while (openSet.Count > 0)
        {
            Node currentNode = openSet.RemoveFirst();
            closeSet.Add(currentNode);

            //���� ���� Ÿ�� ��尡 ������ ���������Ƿ� Ž���� �׸��д�.
            if (currentNode == targetNode)
            {
                stopwatch.Stop();
                print($"Time : {stopwatch.ElapsedMilliseconds} ms");

                GetPath(startNode, targetNode);
                break;
            }

            //���� ����� �ֺ� ��带 List�� �޾� Ȯ���Ѵ�.
            foreach (Node nearByNode in grid.GetNearByNode(currentNode))
            {
                if (closeSet.Contains(nearByNode) || !nearByNode.IsWalkable)
                    continue;

                // ��� ���
                int newGCost = currentNode.gCost + GetDistance(currentNode, nearByNode);
                //���� openSet�� �ִ� �����
                if (openSet.Contains(nearByNode))
                {
                    if (newGCost < nearByNode.gCost) // ����� gcost�� ���ٸ�
                    {
                        nearByNode.gCost = newGCost; // ���Ž����ش�.
                        nearByNode.parent = currentNode; // �θ� ��带 �����Ų��.

                    }

                }
                else
                {
                    nearByNode.gCost = newGCost;
                    nearByNode.hCost = GetDistance(nearByNode, targetNode);
                    nearByNode.parent = currentNode;
                    openSet.Add(nearByNode);
                }
            }
        }
    }

    int GetDistance(Node Node_A, Node Node_B)
    {
        int dist_X = Mathf.Abs(Node_A.gridX - Node_B.gridX);
        int dist_Y = Mathf.Abs(Node_A.gridY - Node_B.gridY);

        //���̰� ª�� ���� ���� �밢������ �� ��,
        //���̰� ���ʿ��� ª�� ���� �� ��ŭ�� �� ���� �̵� ��Ų��.
        if (dist_X > dist_Y)
            return 14 * dist_Y + 10 * (dist_X - dist_Y);
        else
            return 14 * dist_X + 10 * (dist_Y - dist_X);
    }

    void GetPath(Node startNode, Node targetNode)
    {
        path= new List<Node>();

        Node currentNode = targetNode;

        while(true)
        {
            path.Add(currentNode);

            if (currentNode == startNode)
                break;

            currentNode = currentNode.parent;
        }
    }

    private void OnDrawGizmos()
    {
        if(path !=null)
            foreach (var node in path)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(node.worldPosition, Vector3.one);

            }
    }
}
