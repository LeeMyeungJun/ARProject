using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
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
}
