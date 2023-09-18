#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using System.IO;
using System;

public class MapEditor : EditorWindow
{
    private string mapName;
    private LevelInfo levelInfo;
    private TextAsset jsnLevelObj;
    [MenuItem("LMJEditor/MapEditor")] // 메뉴 등록
    private static void Init()
    {
        MapEditor window = (MapEditor)GetWindow(typeof(MapEditor));
        window.Show();

        window.titleContent.text = "MapEditor";

        window.minSize = new Vector2(340f, 150f);
        window.maxSize = new Vector2(340f, 200f);
    }

    void OnGUI()
    {
        Draw();
    }

    void Draw()
    {

        // Header =====================================================================
        GUILayout.Space(10f);
        GUILayout.Label("SAVE MAP", EditorStyles.boldLabel);

        mapName = EditorGUILayout.TextField("MapName", mapName);
        if (GUILayout.Button("Save", GUILayout.Width(128)))
        {
            bool check = Util.IsTextAsset("JsnLevels/" + mapName);
            if (!check)
            {
                Debug.Log("맵저장중... 기다려주세요");
                SaveMap();
                AssetDatabase.Refresh();
            }
            else
            {
                Debug.LogError("중복된 맵이름 파일이있습니다");
            }
        }
    

        // ============================================================================
        GUILayout.Space(30f);
        GUILayout.Label("LOAD MAP", EditorStyles.boldLabel);

        // Horizontal =================================================================
        GUILayout.BeginVertical();
        jsnLevelObj = (TextAsset)EditorGUILayout.ObjectField("Map Json File", jsnLevelObj, typeof(TextAsset), false, GUILayout.Width(300));
        if (GUILayout.Button("Load", GUILayout.Width(128)))
        {
            LoadMap();
        }
        GUILayout.EndVertical();

        if (GUILayout.Button("Destroy", GUILayout.Width(128)))
        {
            DestroyMap();
        }


    }

    private void DestroyMap()
    {
        GameObject mapParent = GameObject.Find("MapParent");
        DestroyImmediate(mapParent);
        GameObject baseObj = new GameObject("MapParent");
        Undo.RegisterCreatedObjectUndo(baseObj, "Create Map");
        Selection.activeObject = baseObj;

    }

    void SaveMap()
    {
        GameObject mapParent =  GameObject.Find("MapParent");
        if(mapParent == null)
        {
            Debug.LogError("Find Misiing Create Please GameObject Name 'MapParent'");
            return;
        }

        levelInfo = new LevelInfo();
        mapParent.transform.position = Vector3.zero;
        mapParent.transform.rotation = Quaternion.identity;

        for (int i = 0; i < mapParent.transform.childCount; i++)
        {
            GameObject dataObj = mapParent.transform.GetChild(i).gameObject;
            LMJResource check = dataObj.GetComponent<LMJResource>();
            LMJMapData mapData = new LMJMapData();
            if (check != null)
            {
                mapData.Setup(dataObj);
                levelInfo.Objets.Add(mapData);
            }
            else
            {//널이면 mapdata add해서 값넣어주고.. 리소스폴더에 저장
                check = dataObj.AddComponent<LMJResource>();
                mapData.Setup(dataObj);
                string path = "Assets/Resources/Prefabs/Map/" + mapData.meshName + ".prefab";
                Util.SavePrefab(path, dataObj);
                levelInfo.Objets.Add(mapData);
            }
        }


        levelInfo.SaveToJson(mapName);
    }

    void LoadMap()
    {
        if (jsnLevelObj == null)
            return;

        //GameObject baseObj = new GameObject("Map");
        //for (int x = 0; x < 10; x++)
        //{
        //    for (int y = 0; y < 10; y++)
        //    {
        //        GameObject childObj = new GameObject(string.Format("Child-{0}-{1}", x, y));
        //        SpriteRenderer sr = childObj.AddComponent<SpriteRenderer>();
        //        sr.sprite = Resources.Load("Square", typeof(Sprite)) as Sprite;
        //        sr.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        //        childObj.transform.position = new Vector3(x, y, 0);
        //        childObj.transform.SetParent(baseObj.transform);
        //    }
        //}

        //Undo.RegisterCreatedObjectUndo(baseObj, "Create Map");

        //Selection.activeObject = baseObj;
        GameObject baseObj = GameObject.Find("MapParent");
        if(baseObj == null)
        {
            Debug.LogError("Missing MapParent Obj :: Please Create EmptyObject Name: MapParent");
            return;
        }
        baseObj.transform.position = Vector3.zero;
        baseObj.transform.rotation = Quaternion.identity;
        baseObj.transform.localScale = Vector3.one;

        //Transform[] transforms = new Transform[baseObj.transform.childCount];
        //for(int i = 0; i <baseObj.transform.childCount;i++)
        //    transforms[i] = baseObj.transform.GetChild(i);

        //for (int i = 0; i < transforms.Length; i++)
        //{
        //    Destroy(transforms[i]);
        //}

        levelInfo = JsonConvert.DeserializeObject<LevelInfo>(jsnLevelObj.text, new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            TypeNameHandling = TypeNameHandling.All
        });

        for(int i = 0; i < levelInfo.Objets.Count; i++)
        {
            string name =  levelInfo.Objets[i].meshName;
            GameObject obj =  Instantiate(Resources.Load<GameObject>("Prefabs/Map/" + name));
            obj.transform.position = levelInfo.Objets[i].pos;
            obj.transform.rotation = levelInfo.Objets[i].rot;
            obj.transform.localScale = levelInfo.Objets[i].scale;
            obj.transform.SetParent(baseObj.transform);
            //levelInfo.Objets[i].
        }
        Undo.RegisterCreatedObjectUndo(baseObj, "Create Map");
        Selection.activeObject = baseObj;
    }
}


#endif