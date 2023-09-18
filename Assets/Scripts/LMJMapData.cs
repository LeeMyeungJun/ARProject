using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LMJMapData
{
    public string meshName;
    public Vector3 pos;
    public Quaternion rot;
    public Vector3 scale;

    public void Setup(GameObject obj) 
    {
        string[] subs = obj.name.Split(' ','('); // ���� �޺κ����� , ( clone �̶���̸������� �߰�
        string fileName = subs[0];

        this.meshName = fileName;
        this.pos = obj.transform.position;
        this.rot = obj.transform.rotation;
        this.scale = obj.transform.localScale;
    }
}
