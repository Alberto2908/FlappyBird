using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    public GameObject objectToPool;
    public uint size;
    public bool shouldExpand = false;
    private List<GameObject> pool;
   
    void Start()
    {
        pool = new List<GameObject>();

        for (int i = 0; i < size; i++)
        {
            AddGameOjectToPool();
        }
    }

    public GameObject GetAvailableObject()
    {
        for (int i = pool.Count - 1; i >= 0; i--)
        {
            if (pool[i] == null)
            {
                pool.RemoveAt(i);
                continue;
            }

            if (!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }

        if (shouldExpand)
        {
            return AddGameOjectToPool();
        }
        return null;
    }

    private GameObject AddGameOjectToPool()
    {
        GameObject o = Instantiate(objectToPool);
        pool.Add(o);
        o.SetActive(false);

        return o;
    }
}
