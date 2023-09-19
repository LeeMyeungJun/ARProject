using UnityEngine;

public class MonoSingle<T> : MonoBehaviour where T : MonoSingle<T>
{
    static public T Instance;

    virtual protected void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Instance != null " + gameObject.name);
            return;
        }
        Instance = (T)this;
    }


    public void DestroyImmediate()
    {
        if (Instance)
            GameObject.DestroyImmediate(Instance);

       // LmjEventManager.removeEventListenerByTag(GetHashCode());
        Instance = null;
    }

    virtual protected void OnDestroy()
    {
        Instance = null;
    }

}