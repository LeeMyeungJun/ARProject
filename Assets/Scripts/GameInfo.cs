using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  저장관련 
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;



[Serializable]
public class GameInfo
{
    //  세이브 할 정보
    //  해당 객체에 들어갈 자료 세팅
    public float castleHP;
    public int curmoney;
    public int currLv;
    public float atkSpeed;
    public float attackDmg;
}
