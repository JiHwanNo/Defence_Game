using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    public PathRequestManager pathRequestMgr;
    public Transform target;

    private void Awake()
    {
        pathRequestMgr = FindObjectOfType<PathRequestManager>();
    }
    private void Start()
    {
        if (target)
        {
            pathRequestMgr.RequestPath(transform.position, target.position, FindPath);
        }
    }

    void FindPath(Vector3[] path,bool success)
    {
        if (success)
            StartCoroutine(StartMovingObj(new Stack<Vector3> (path)));

    }

    IEnumerator StartMovingObj(Stack<Vector3> Path)
    {
        Vector3 nextPosition = Path.Pop();

        while(Path.Count>0)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, 0.1f);
            
            if (transform.position == nextPosition)
                nextPosition = Path.Pop();

            yield return new WaitForFixedUpdate();
        }

        Debug.Log(nextPosition);

        yield return null;
    }
}
