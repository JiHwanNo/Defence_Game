using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PathManager : MonoBehaviour
{
    [SerializeField] PathRequestManager pathrequestMgr;
    [SerializeField] private _Grid grid;

    private void Awake()
    {
        pathrequestMgr = GetComponent<PathRequestManager>();
        grid = GetComponent<_Grid>();
    }
    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        StartCoroutine(FindPath(startPos, targetPos));
    }
    
    private IEnumerator FindPath(Vector3 seeker, Vector3 target)
    {

        Vector3[] wayPoints = new Vector3[0];
        bool pathSuccess = false; // 길 탐색 성공여부

        Node startNode = grid.GetNodeFromWorldPosition(seeker); // 출발 Node[x,y]
        Node targetNode = grid.GetNodeFromWorldPosition(target); // 도착 Node[x,y]

        Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
        HashSet<Node> closeSet = new HashSet<Node>();

        openSet.Add(startNode); // 효율적인 Node면 저장해 놓는다.

        while (openSet.Count > 0)
        {
            Node currentNode = openSet.RemoveFirst();
            closeSet.Add(currentNode);

            //뽑은 노드와 타켓 노드가 같으면 도착했으므로 탐색을 그만둔다.
            if (currentNode == targetNode)
            {
                pathSuccess = true;
                break;
            }

            //뽑은 노드의 주변 노드를 List로 받아 확인한다.
            foreach (Node nearByNode in grid.GetNearByNode(currentNode))
            {
                if (closeSet.Contains(nearByNode) || !nearByNode.IsWalkable)
                    continue;

                int newGCost = currentNode.gCost + GetDistance(currentNode, nearByNode);
                
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

        if (pathSuccess)
            wayPoints = GetPath(startNode, targetNode);

        pathrequestMgr.FinishedProcessingPath(wayPoints, pathSuccess);
        yield return  null;
        
    }

    int GetDistance(Node Node_A, Node Node_B)
    {
        int dist_X = Mathf.Abs(Node_A.gridX - Node_B.gridX);
        int dist_Y = Mathf.Abs(Node_A.gridY - Node_B.gridY);

        if (dist_X > dist_Y)
            return 14 * dist_Y + 10 * (dist_X - dist_Y);
        else
            return 14 * dist_X + 10 * (dist_Y - dist_X);
    }

    Vector3[] GetPath(Node startNode, Node targetNode)
    {
        List<Vector3> path = new List<Vector3>();

        Node currentNode = targetNode;

        path.Add(currentNode.worldPosition);

        while(currentNode != startNode)
        {
            currentNode = currentNode.parent;
            path.Add(currentNode.worldPosition);
        }
        return path.ToArray();
    }
}
