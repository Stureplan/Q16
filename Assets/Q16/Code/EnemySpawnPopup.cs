using UnityEditor;
using UnityEngine;
using System.Collections;

public class EnemySpawnPopup : EditorWindow
{
    static GameObject parent;
    static GameObject[] prefabs;
    static Texture2D[] images;
    static bool isDisplayed;

    bool hasSelected = true;

    GameObject selected;
    Vector2 scrollPos;
    static InteractableObject interactableObject;


    void OnDestroy()
    {
        isDisplayed = false;
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("Enemy Spawns", EditorStyles.boldLabel);
        GUILayout.Space(10.0f);

        ShowPrefabs();

        GUILayout.Space(10.0f);

        GUILayout.FlexibleSpace();

        if (!hasSelected) { EditorGUILayout.HelpBox("You haven't selected any prefab to use.", MessageType.Warning, true); }

        if (selected != null)
        {
            EditorGUILayout.LabelField("Selected: " + selected.name, EditorStyles.boldLabel);
        }
        else
        {
            EditorGUILayout.LabelField("Selected: " + "none", EditorStyles.boldLabel);
        }

        if (GUILayout.Button("Create Spawn Point"))
        {
            if (selected != null)
            {
                EnemySpawn es = CreateObject();
                AddToList(es);

                hasSelected = true;
            }
            else
            {
                hasSelected = false;
            }

        }
    }

    EnemySpawn CreateObject()
    {
        GameObject go = new GameObject("EnemySpawn(" + selected.name + ")");
        go.transform.parent = parent.transform;
        go.tag = "EnemySpawn";
        go.transform.localPosition = Vector3.zero;

        go.AddComponent<ActionSpawnEnemy>();

        EnemySpawn es;
        es.e_Prefab = selected;
        es.e_Transform = go.transform;
        es.e_Point = go.transform.localPosition;

        return es;
    }
    
    void AddToList(EnemySpawn enemySpawn)
    {
        EnemySpawn[] temp = new EnemySpawn[interactableObject.aEnemySpawns.Length + 1];
        interactableObject.aEnemySpawns.CopyTo(temp, 0);
        interactableObject.aEnemySpawns = temp;

        interactableObject.aEnemySpawns[interactableObject.aEnemySpawns.Length-1] = enemySpawn;
    }

    void ShowPrefabs()
    {
        //scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.Width(150.0f), GUILayout.Height(200.0f));

        //GUILayout.BeginHorizontal();
        
        for (int i = 0; i < prefabs.Length; i++)
        {
            GUILayout.BeginVertical();

            GUILayout.Label(prefabs[i].name);

            if(GUILayout.Button(images[i], GUILayout.Width(images[i].width), GUILayout.Height(images[i].height)))
            {
                selected = prefabs[i];
                hasSelected = true;
            }


            GUILayout.EndVertical();
        }
        //GUILayout.EndHorizontal();

        //GUILayout.EndScrollView();
    }

    void OnInspectorUpdate()
    {
        Repaint();
    }
    
    public static void Display(GameObject p, InteractableObject io)
    {
        if (isDisplayed == false)
        {
            EnemySpawnPopup window = CreateInstance<EnemySpawnPopup>();
            window.maxSize = new Vector2(200, 600);
            window.minSize = window.maxSize;

            GUIContent content = new GUIContent("Enemy Spawner");
            
            parent = p;
            interactableObject = io;

            window.titleContent = content;
            window.ShowUtility();
            
            Load();

            isDisplayed = true;
        }
    }

    private static void Load()
    {
        prefabs = Resources.LoadAll<GameObject>("Prefabs/");
        images = new Texture2D[prefabs.Length];

        for (int i = 0; i < prefabs.Length; i++)
        {
            images[i] = AssetPreview.GetAssetPreview(prefabs[i]);

        }
    }
}
