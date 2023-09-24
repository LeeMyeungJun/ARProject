using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

[System.Serializable]
public class LevelInfo : ScriptableObject
{
	//public int Lv;
	public List<LMJMapData> Objets = new List<LMJMapData>();

	//public void SaveToJson(int _name)
	//{
	//	SaveToJson(_name);
	//	//SaveToJson(Lv.ToString());
	//}

	public void SaveToJson(string name)
	{
		string str = JsonConvert.SerializeObject(this, Formatting.Indented,
			new JsonSerializerSettings()
			{
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
				TypeNameHandling = TypeNameHandling.All
			});

		Util.SaveText("/Resources/JsnLevels/" + name + ".json", str);
	}
	public void LoadData()
	{
        //levelInfo = JsonConvert.DeserializeObject<LevelInfo>(jsnLevelObj.text, new JsonSerializerSettings()
        //{
        //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        //    TypeNameHandling = TypeNameHandling.All
        //});

        GameObject baseObj = new GameObject("MapParent");
		
        for (int i = 0; i < Objets.Count; i++)
        {
            string name = Objets[i].meshName;
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/Map/" + name));
            obj.transform.position = Objets[i].pos;
            obj.transform.rotation = Objets[i].rot;
            obj.transform.localScale = Objets[i].scale;
            obj.transform.SetParent(baseObj.transform);
            //levelInfo.Objets[i].
        }
		NavMeshSurface nav = baseObj.AddComponent<NavMeshSurface>();
		nav.collectObjects = CollectObjects.Children;
		nav.BuildNavMesh();
        
    }
}
