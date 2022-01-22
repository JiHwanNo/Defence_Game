using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public Transform player, target;
    [SerializeField] private Grid grid;

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }
    private void Start()
    {
        FindPath(player.position, target.position);
    }

    private void FindPath(Vector3 seeker, Vector3 target)
    {
        Node startNode = grid.GetNodeFromWorldPosition(seeker); // 출발 Node[x,y]
        Node targetNode = grid.GetNodeFromWorldPosition(target); // 도착 Node[x,y]
        
        // openSet은 비용적으로 효율적인 Node를 모아 놓는 List이다.
        List<Node> openSet = new List<Node>();
        // closeSet은 여지껏 왔던 Node 중 왔던 길,혹은 비용적으로 비효율적인 Node를 모아 놓은 HashSet이다.
        HashSet<Node> closeSet = new HashSet<Node>();

        openSet.Add(startNode); // 효율적인 Node면 저장해 놓는다.
        while (openSet.Count > 0)
        {
            //openSet List에서 가장 적은 비용의 Node를 찾는다.
            Node currentNode = openSet[0];
            for (int i = 0; i < openSet.Count; i++)
            {
                // 임시로 담겨있는 Node currentNode에서 비용이 작은 Node를 뽑아 그것으로 바꿔준다.
                // 만약 Fcost 비용이 같은 경우 앞으로 갈 비용 중 가장 적은 Node를 우선순위로 둔다.
                if (openSet[i].Fcost < currentNode.Fcost || 
                   (openSet[i].Fcost == currentNode.Fcost && openSet[i].hCost < currentNode.hCost))
                    currentNode = openSet[i];
            }
            // 뽑은 Node는 openSet에 없애주고, CloseSet에 담겨준다.
            openSet.Remove(currentNode);
            closeSet.Add(currentNode);

            //뽑은 노드와 타켓 노드가 같으면 도착했으므로 탐색을 그만둔다.
            if (currentNode == targetNode)
                break;

            //뽑은 노드의 주변 노드를 List로 받아 확인한다..
            foreach(Node nearByNode in grid.GetNearByNode(currentNode))
            {
               
                if (closeSet.Contains(nearByNode) || !nearByNode.IsWalkable)
                    continue;
            }
        }
    }
}
