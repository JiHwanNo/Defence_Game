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
        Node startNode = grid.GetNodeFromWorldPosition(seeker); // ��� Node[x,y]
        Node targetNode = grid.GetNodeFromWorldPosition(target); // ���� Node[x,y]
        
        // openSet�� ��������� ȿ������ Node�� ��� ���� List�̴�.
        List<Node> openSet = new List<Node>();
        // closeSet�� ������ �Դ� Node �� �Դ� ��,Ȥ�� ��������� ��ȿ������ Node�� ��� ���� HashSet�̴�.
        HashSet<Node> closeSet = new HashSet<Node>();

        openSet.Add(startNode); // ȿ������ Node�� ������ ���´�.
        while (openSet.Count > 0)
        {
            //openSet List���� ���� ���� ����� Node�� ã�´�.
            Node currentNode = openSet[0];
            for (int i = 0; i < openSet.Count; i++)
            {
                // �ӽ÷� ����ִ� Node currentNode���� ����� ���� Node�� �̾� �װ����� �ٲ��ش�.
                // ���� Fcost ����� ���� ��� ������ �� ��� �� ���� ���� Node�� �켱������ �д�.
                if (openSet[i].Fcost < currentNode.Fcost || 
                   (openSet[i].Fcost == currentNode.Fcost && openSet[i].hCost < currentNode.hCost))
                    currentNode = openSet[i];
            }
            // ���� Node�� openSet�� �����ְ�, CloseSet�� ����ش�.
            openSet.Remove(currentNode);
            closeSet.Add(currentNode);

            //���� ���� Ÿ�� ��尡 ������ ���������Ƿ� Ž���� �׸��д�.
            if (currentNode == targetNode)
                break;

            //���� ����� �ֺ� ��带 List�� �޾� Ȯ���Ѵ�..
            foreach(Node nearByNode in grid.GetNearByNode(currentNode))
            {
               
                if (closeSet.Contains(nearByNode) || !nearByNode.IsWalkable)
                    continue;
            }
        }
    }
}
