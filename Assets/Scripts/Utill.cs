using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using Newtonsoft.Json;

public class Util 
{
    public static T LoadData<T>(string _path)
    {
        //string path = Application.persistentDataPath + "/save.dat";
        string path = Application.persistentDataPath + _path;

        FileStream fs = null;
        try
        {
            fs = new FileStream(path, FileMode.Open);
        }
        catch (Exception e)
        {
            Debug.LogError("path : "+path +"저장된Load파일없음");
            return default(T);
        }

        BinaryFormatter bt = new BinaryFormatter();
        T Data = (T)bt.Deserialize(fs);
        //money = Data.money;
        //att_power = Data.power;
        //cur_Stage = Data.curStage;
        //base_life = Data.base_life;
        //maxCreate = Data.maxCreate;
        fs.Close();
        return Data;
    }
    public static void SaveData<T>(T _info,string _path)
    {
        //string path = Application.persistentDataPath + "/save.dat";
        string path = Application.persistentDataPath + _path;

        FileStream fs = new FileStream(path, FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();

        bf.Serialize(fs, _info);
        fs.Close();
    }
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
    public static TextAsset LoadTextAsset(string path)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        if (textAsset == null)
            return null;

        return textAsset;
    }

    public static bool LoadJsonData<T>(TextAsset textAsset,out T data)
    {
        data = JsonConvert.DeserializeObject<T>(textAsset.text, new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            TypeNameHandling = TypeNameHandling.All
        });
        return true;
    }
    static public void SavePrefab(string _path,GameObject _obj)
    {
        UnityEditor.PrefabUtility.SaveAsPrefabAsset(_obj, _path); //저장
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
