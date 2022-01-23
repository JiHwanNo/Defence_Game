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

        // openSet은 비용적으로 효율적인 Node를 모아 놓는 List이다.
        Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
        // closeSet은 여지껏 왔던 Node 중 왔던 길,혹은 비용적으로 비효율적인 Node를 모아 놓은 HashSet이다.
        HashSet<Node> closeSet = new HashSet<Node>();

        Node startNode = grid.GetNodeFromWorldPosition(seeker); // 출발 Node[x,y]
        Node targetNode = grid.GetNodeFromWorldPosition(target); // 도착 Node[x,y]

        openSet.Add(startNode); // 효율적인 Node면 저장해 놓는다.
        while (openSet.Count > 0)
        {
            Node currentNode = openSet.RemoveFirst();
            closeSet.Add(currentNode);

            //뽑은 노드와 타켓 노드가 같으면 도착했으므로 탐색을 그만둔다.
            if (currentNode == targetNode)
            {
                stopwatch.Stop();
                print($"Time : {stopwatch.ElapsedMilliseconds} ms");

                GetPath(startNode, targetNode);
                break;
            }

            //뽑은 노드의 주변 노드를 List로 받아 확인한다.
            foreach (Node nearByNode in grid.GetNearByNode(currentNode))
            {
                if (closeSet.Contains(nearByNode) || !nearByNode.IsWalkable)
                    continue;

                // 비용 계산
                int newGCost = currentNode.gCost + GetDistance(currentNode, nearByNode);
                //기존 openSet에 있는 노드라면
                if (openSet.Contains(nearByNode))
                {
                    if (newGCost < nearByNode.gCost) // 계산한 gcost가 낮다면
                    {
                        nearByNode.gCost = newGCost; // 갱신시켜준다.
                        nearByNode.parent = currentNode; // 부모 노드를 저장시킨다.

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

        //길이가 짧은 곳을 먼저 대각선으로 간 후,
        //길이가 긴쪽에서 짧은 쪽을 뺀 만큼을 더 직직 이동 시킨다.
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
