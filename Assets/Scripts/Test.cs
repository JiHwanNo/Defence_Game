using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    
    private void Start()
    {
        Vector3 rect = transform.GetComponent<MeshCollider>().bounds.size;    
        Debug.Log(rect);
    }
}
