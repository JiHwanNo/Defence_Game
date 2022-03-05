using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{

    private Dictionary<GameObject, List<GameObject>> objectPools = new Dictionary<GameObject, List<GameObject>>(10);

    public bool CreatePool(GameObject objToPool, int initialPoolSize)   // 만들고자하는 게임 오브젝트 및 개수
    {
        if (objToPool == null)
            return false;

        if (objectPools.ContainsKey(objToPool))
            return false;

        List<GameObject> nPool = new List<GameObject>(10);
        objectPools.Add(objToPool, nPool);

        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject nObj = Instantiate(objToPool, Vector3.zero, Quaternion.identity);
            nObj.SetActive(false);
            nObj.transform.SetParent(transform);
            nPool.Add(nObj);
        }

#if UNITY_EDITOR
        Update_PoolInfo();
#endif

        return true;
    }

    public GameObject Get_Object(GameObject objToPool, Transform parent) // 오브젝트 풀에서 게임오브젝트를 확인하고 불러옴
    {
        if (objectPools.ContainsKey(objToPool) == false)
            CreatePool(objToPool, 1);

        List<GameObject> tempPool = objectPools[objToPool];

        for (int i = 0; i < tempPool.Count; i++)
        {
            if (tempPool[i] != null)
            {
                if (tempPool[i].activeSelf == false)
                {
                    tempPool[i].transform.SetParent(parent);
                    tempPool[i].SetActive(true);
                    return tempPool[i];
                }
            }
            else
            {
                tempPool.Remove(null);
            }
        }

        GameObject nObj = Instantiate(objToPool, Vector3.zero, Quaternion.identity);

        nObj.transform.SetParent(parent);
        nObj.SetActive(true);
        tempPool.Add(nObj);

#if UNITY_EDITOR
        Update_PoolInfo();
#endif

        return nObj;
    }

    public T Get_Object<T>(GameObject objToPool, Transform parent) // 해당 오브젝트 풀에서 원하는 컴포넌트를 가져올 때 사용
    {
        return Get_Object(objToPool, parent).GetComponent<T>();
    }

    public void Restore(GameObject objToPool)
    {
        objToPool.SetActive(false);
        objToPool.transform.SetParent(transform);
        objToPool.transform.localScale = Vector3.one;
    }


    public void Restore_Obj(GameObject objToPool)
    {
        if (objectPools.ContainsKey(objToPool) == false)
            return;

        List<GameObject> listobj = objectPools[objToPool];

        for (int i = 0; i < listobj.Count; i++)
        {
            listobj[i].SetActive(false);
            listobj[i].transform.parent = transform;
        }
    }

#if UNITY_EDITOR
    [System.Serializable]
    public class PoolInfo
    {
        public string m_poolName;
        public int m_poolCount;

        public PoolInfo(string poolName, int poolCount)
        {
            m_poolName = poolName;
            m_poolCount = poolCount;
        }

    }

    public List<PoolInfo> m_poolInfoList = new List<PoolInfo>();

    void Update_PoolInfo()
    {
        m_poolInfoList.Clear();
        if (objectPools != null)
        {
            var IE = objectPools.GetEnumerator();
            while (IE.MoveNext())
            {
                m_poolInfoList.Add(new PoolInfo(IE.Current.Key.name, IE.Current.Value.Count));
            }
        }
    }
#endif
}

