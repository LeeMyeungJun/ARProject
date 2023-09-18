using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class Util 
{
    public static void GlobalTime(float _scale)
    {
        Time.timeScale = _scale;
    }
    public static bool IsTextAsset(string path)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        if (textAsset == null)
            return false;
        return true;
    }

    static public void SavePrefab(string _path,GameObject _obj)
    {
        UnityEditor.PrefabUtility.SaveAsPrefabAsset(_obj, _path); //¿˙¿Â
        UnityEditor.AssetDatabase.Refresh();
    }

    static public void SaveText(string path, string contents)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(Application.dataPath);
        sb.Append(path);// "/Resources/TranslateDic.txt");

        FileStream file = new FileStream(sb.ToString(), FileMode.Create, FileAccess.Write);
        StreamWriter sw = new StreamWriter(file, System.Text.Encoding.UTF8);

        sw.Write(contents);
        sw.Close();
        file.Close();
    }
}
