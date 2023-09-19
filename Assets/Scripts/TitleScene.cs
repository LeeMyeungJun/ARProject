using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//  ������� 
using System;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

public class TitleScene : MonoBehaviour
{

    public void Btn_LoadGame()
    {
        LoadData();
    }

    void LoadData()
    {
        GameInfo data = new GameInfo(); // ������ ������� .
        data = Util.LoadData<GameInfo>("/ save.dat");
    }

    public void Btn_SaveGame()
    {
        SaveData();
    }
    void SaveData()
    {
        GameInfo data = new GameInfo();
        data.atkSpeed = 2.0f;
        data.attackDmg = 50.0f;
        Util.SaveData<GameInfo>(data, "/ save.dat");
    }
}
