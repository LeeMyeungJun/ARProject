using UnityEngine;
using System.Collections.Generic;
using System.Reflection;


public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    protected static T instance;
    protected string Id;
    protected bool isDestroy;
    static protected bool isQuit = false;

    /*public static T Get()
    {
        return Instance;
    }*/

    public static T Instance
    {
        get
        {
            if (!instance)
            {
                if (isQuit) //게임 죽는중..
                    return null;

                // 일단 씬에 객체가 있다라고 가정하고 처음에 찾는다~     
                // 없다면 게임오브젝트 생성하고 T를 Add해서 싱글톤 객체를 만든다.
                instance = (T)FindObjectOfType(typeof(T));

                if (!instance)
                {
                    //if( ResourceDB.IsApplicationQuit == true )
                    //{
                    //	TODebug.LogWarning( string.Format( "[{0}]Application 종료 후 MonoSingleton의 생성을 시도했습니다.", typeof( T ) ) );
                    //	return null;
                    //}

                    instance = new GameObject(typeof(T).Name).AddComponent<T>();
                    instance.Init();
                }
                else
                    instance.Init();
            }

            return instance;
        }

        protected set { }
    }

    protected virtual void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("instance != null " + this.gameObject.name);
            return;
        }

        instance = this as T;
        Instance.gameObject.name = typeof(T).Name;
        DontDestroyOnLoad(Instance);
        Debug.Log("Singleton : " + Instance.gameObject.name);
    }

    public static void DestroySingleton()
    {
        if (instance == null)
            instance = (T)FindObjectOfType(typeof(T));

        if (instance != null)
            GameObject.DestroyImmediate(instance.gameObject);
        instance = null;
    }

    public static bool isDestroyed()
    {
        return (instance == null);
    }

    void OnApplicationQuit()
    {
        instance = null;
        Instance = null;
        isQuit = true;
    }

    protected virtual void Init()
    {
    }

}