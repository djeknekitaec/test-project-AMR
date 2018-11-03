using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolManager<T> : MonoBehaviour where T : MonoBehaviour
{
    public GameObject ItemPrefab;
    public Transform parentContainer;

    List<T> pool;

    public void Initialize(int count)
    {
        if (pool == null)
            pool = new List<T>();
        else
        {
            Debug.LogError("You tried initialize pool second time!");
            return;
        }

        for (int i = 0; i < count; i++)
        {
            InstantiateObj();
        }
    }

    public T Pull()
    {
        if (pool.Count == 0)
        {
            InstantiateObj();
        }

        T obj = pool[0];
        pool.Remove(obj);

        return obj;
    }

    public void Push(T obj)
    {
        pool.Add(obj);
        obj.transform.SetParent(parentContainer);
        obj.transform.localPosition = Vector3.zero;
    }

    private void InstantiateObj()
    {
        GameObject obj = Instantiate(ItemPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        obj.transform.SetParent(parentContainer);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        pool.Add(obj.GetComponent<T>());
    }
}
