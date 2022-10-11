using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public class ObjectPoolData
    {
        public GameObject go;
        public bool isUsing;
    }

    private List<List<ObjectPoolData>> objectTypes;

    private void Awake()
    {
        objectTypes = new List<List<ObjectPoolData>>();
    }

    List<ObjectPoolData> QueryEmptyList()
    {
        List<ObjectPoolData> list = new List<ObjectPoolData>();
        objectTypes.Add(list);
        return list;
    }

    public List<ObjectPoolData> InitObjectPoolData(Object o,int count)
    {
        List<ObjectPoolData> list = QueryEmptyList();

        for(int i=0; i < count; i++)
        {
            GameObject go = Instantiate(o) as GameObject;
            ObjectPoolData data = new ObjectPoolData();
            data.isUsing = false;
            data.go = go;
            go.SetActive(false);
            list.Add(data);
        }

        return list;
    }

    public void AddObjectPoolData(List<ObjectPoolData> list, Object o, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject go = Instantiate(o) as GameObject;
            ObjectPoolData data = new ObjectPoolData();
            data.isUsing = false;
            data.go = go;
            go.SetActive(false);
            list.Add(data);
        }
    }

    public void ClearObjectPoolList(List<ObjectPoolData> list)
    {
        list.Clear();
        objectTypes.Remove(list);
    }

    public GameObject LoadObjectFromPool(List<ObjectPoolData> list)
    {
        int count = list.Count;
        for(int i = 0; i < count; i++)
        {
            if (!list[i].isUsing)
            {
                list[i].isUsing = true;
                return list[i].go;
            }
        }

        return null;
    }

    public void UnloadObjectToPool(List<ObjectPoolData> list, GameObject go)
    {
        int count = list.Count;
        for (int i = 0; i < count; i++)
        {
            if (!list[i].go == go)
            {
                go.SetActive(false);
                list[i].isUsing = false;
                break;
            }
        }
    }
}
