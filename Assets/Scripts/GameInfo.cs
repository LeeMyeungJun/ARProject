using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  ������� 
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;



[Serializable]
public class GameInfo
{
    //  ���̺� �� ����
    //  �ش� ��ü�� �� �ڷ� ����
    public float castleHP;
    public int curmoney;
    public int currLv;
    public float atkSpeed;
    public float attackDmg;
}
