using UnityEngine;
using System.Text;
using System.Collections;
using System.Collections.Generic;


public class LMJ
{
#if UNITY_EDITOR
    static public System.Action<object> Log = Debug.Log ;
    static public System.Action<object> LogWarning = Debug.LogWarning;
    static public System.Action<object> LogError = Debug.LogError;

#else
    static public void Log(object message)
    {
        if(_isLog)
            Debug.Log(message);
    }

    static public void LogWarning(object message)
    {
        if(_isLog)
		    Debug.LogWarning(message);
    }
    
    static public void LogError(object message)
    {
        if(_isLog)
            Debug.LogError(message);
    }
#endif

    static public void LogFormat(string format, params object[] args)
    {
        LMJ.Log(string.Format(format, args));
    }

    static public void LogWarningFormat(string format, params object[] args)
    {
        LMJ.LogWarning(string.Format(format, args));
    }

    static public void LogErrorFormat(string format, params object[] args)
    {
        LMJ.LogError(string.Format(format, args));
    }

    static bool _isLog = false;
    static public void SetLog(bool isOn)
    {
        //if(isOn)
        //    ExtendPlayerPrefs.SetBool("log", isOn);
        _isLog = isOn;
    }
    
    static public bool isLog()
    {

//#if UNITY_EDITOR
//        return true;
//#endif
        return _isLog;
    }
}

//#if UNITY_EDITOR
//public static class Debug
//{
//    [System.Diagnostics.Conditional("UNITY_EDITOR")]
//    public static void Log(object message) { }
//}
//#endif