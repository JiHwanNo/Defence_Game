using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PathRequestManager : MonoBehaviour
{
    private PathManager pathMgr;
    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    PathRequest currentPathRequest;

    //지금 경로 탐색중인지 확인, true 계산중 / false 계산끝
    bool isProcessingPath;
    private void Awake()
    {
        pathMgr = GetComponent<PathManager>();
        isProcessingPath = false;
    }
    public void RequestPath(Vector3 Start, Vector3 End, Action<Vector3[], bool> Callback)
    {
        PathRequest pathRequest = new PathRequest(Start, End, Callback);
        pathRequestQueue.Enqueue(pathRequest);

        TryProcessNext();
    }
    void TryProcessNext()
    {

        if (!isProcessingPath && pathRequestQueue.Count >0)
        {
            isProcessingPath = true;
            currentPathRequest = pathRequestQueue.Dequeue();
        }

        pathMgr.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        //경로 탐색 to do
    }
    public void FinishedProcessingPath(Vector3[] path, bool success)
    {
        isProcessingPath = false;

        currentPathRequest.pathCallback(path, success);

        //StartCoroutine(Restart());
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(10f);

        TryProcessNext();
    }

    struct PathRequest
    {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[], bool> pathCallback;

        public PathRequest(Vector3 Start, Vector3 End, Action<Vector3[], bool> Callback)
        {
            pathStart = Start;
            pathEnd = End;
            pathCallback = Callback;
        }
    }

}
