using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;

public class ObjectPool : MonoBehaviour
{
    public GameObject Prefab;
    public int InitialSize;

    //private readonly Stack<GameObject> instances = new Stack<GameObject>();
    private readonly Queue<GameObject> instances = new Queue<GameObject>();

    private void Awake()
    {
        //Assert.IsNotNull(Prefab);
    }

    public void Initialize()
    {
        for (var i = 0; i < InitialSize; i++)
        {
            var obj = CreateInstance();
            obj.SetActive(false);
            instances.Enqueue(obj);
        }
    }

    public Queue<GameObject> GetQueue()
    {
        return instances;
    }

    virtual public GameObject GetObject()
    {
        var obj = instances.Count > 0 ? instances.Dequeue() : CreateInstance();
        if(obj == null)
            return null;

        obj.GetComponent<PooledObject>().isInPool = false; //debug 용
        obj.SetActive(true);
        return obj;
    }

    virtual public void ReturnObject(GameObject obj, bool returnParent = true)
    {
        PooledObject pooledObject = null;
        try
        {
            pooledObject = obj.GetComponent<PooledObject>();
        }
        catch(System.Exception e)
        {
            LMJ.LogError(e.Message);
            return;
        }
        Assert.IsNotNull(pooledObject);

        if(pooledObject.Pool != this)
        {
            LMJ.LogError("pooledObject.Pool != this");
            pooledObject.Pool.ReturnObject(obj, returnParent);
            return;
        }
        Assert.IsTrue(pooledObject.Pool == this);

        obj.SetActive(false);
        if (!instances.Contains(obj))
        {
            pooledObject.isInPool = true;
            if (returnParent)
                obj.transform.parent = this.transform;
            
            instances.Enqueue(obj);
        }
    }

    public void Reset()
    {
        var objectsToReturn = new List<GameObject>();
        foreach (var instance in transform.GetComponentsInChildren<PooledObject>())
        {
            if (instance.gameObject.activeSelf)
            {
                instance.transform.localScale = new Vector3(1, 1, 1);
                objectsToReturn.Add(instance.gameObject);
            }
        }
            
        foreach (var instance in objectsToReturn)
            ReturnObject(instance, true);
    }

    public void Init()
    {
        Reset();

        foreach(var obj in instances)
        {
            GameObject.Destroy(obj);
        }
        instances.Clear();
    }

    private GameObject CreateInstance()
    { 
        if(Prefab == null)
        {
            LMJ.LogError("Prefab == null " + gameObject.name );
            return null;
        }
        var obj = Instantiate(Prefab);
        var pooledObject = obj.AddComponent<PooledObject>();
        pooledObject.Pool = this;
        obj.transform.SetParent(transform);
        return obj;
    }
}

public class PooledObject : MonoBehaviour
{
    public bool isInPool = true; //debug 용
    public ObjectPool Pool;
}
