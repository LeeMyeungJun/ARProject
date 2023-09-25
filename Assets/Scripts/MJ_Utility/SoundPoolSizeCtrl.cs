// Copyright (C) 2018-2019 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;


public class SoundPoolSizeCtrl : ObjectPool
{
    public int MaxInstance = 20;
    private readonly List<GameObject> activeObj = new List<GameObject>();

    override public GameObject GetObject()
    {
        return GetObject(true);
    }

    public List<GameObject> GetPoolObjectList() { return activeObj; }

    public GameObject GetObject(bool checkMax)
    {
        var obj = base.GetObject();
        if (obj == null)
            return null;

        if (checkMax)
        {
            activeObj.Add(obj);
            if (activeObj.Count > MaxInstance)
            {
                GameObject go = activeObj[0];
                activeObj.RemoveAt(0);
                SoundFx snd = go.GetComponent<SoundFx>();
                if (snd != null)
                    snd.KillSoundFxManual();
            }
        }
        return obj;
    }

    override public void ReturnObject(GameObject obj, bool returnParent = false)
    {
        if (activeObj.Contains(obj))
            activeObj.Remove(obj);
        base.ReturnObject(obj, returnParent);
    }
}
