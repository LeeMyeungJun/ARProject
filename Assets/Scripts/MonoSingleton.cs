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
                if (isQuit) //���� �״���..
                    return null;

                // �ϴ� ���� ��ü�� �ִٶ�� �����ϰ� ó���� ã�´�~     
                // ���ٸ� ���ӿ�����Ʈ �����ϰ� T�� Add�ؼ� �̱��� ��ü�� �����.
                instance = (T)FindObjectOfType(typeof(T));

                if (!instance)
                {
                    //if( ResourceDB.IsApplicationQuit == true )
                    //{
                    //	TODebug.LogWarning( string.Format( "[{0}]Application ���� �� MonoSingleton�� ������ �õ��߽��ϴ�.", typeof( T ) ) );
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