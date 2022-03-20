using System.Collections.Generic;
using UnityEngine;

public static class ResouceManager
{
    static Dictionary<string, GameObject> resouces = new Dictionary<string, GameObject>();
    public static GameObject Load_Prefab(string path)
    {
        GameObject obj;
        if (resouces.TryGetValue(path, out obj))
            return obj;
        else
        {
            obj = Resources.Load<GameObject>(path);
            if (obj == null)
            {
                Debug.Log($"Load Missing !!! : From {path} _ Load_Prefab()");
                return null;
            }
            resouces.Add(path,obj);
            return obj;
        }
    }

}
