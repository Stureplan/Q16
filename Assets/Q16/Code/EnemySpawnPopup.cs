using UnityEditor;
using UnityEngine;
using System.Collections;

public class EnemySpawnPopup : EditorWindow
{
    static GameObject[] prefabs;
    static Texture2D[] images;
    static bool isDisplayed;

    GameObject selected;

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

        if (selected != null)
        {
            EditorGUILayout.LabelField("Selected: " + selected.name, EditorStyles.boldLabel);
        }
        else
        {
            EditorGUILayout.LabelField("Selected: " + "none", EditorStyles.boldLabel);
        }

        GUILayout.FlexibleSpace();

        GUILayout.Label("Spawn");

    }

    void ShowPrefabs()
    {
        GUILayout.BeginHorizontal();
        for (int i = 0; i < prefabs.Length; i++)
        {
            GUILayout.BeginVertical();

            GUILayout.Label(prefabs[i].name);

            if(GUILayout.Button(images[i], GUILayout.Width(images[i].width), GUILayout.Height(images[i].height)))
            {
                selected = prefabs[i];
            }


            GUILayout.EndVertical();
        }
        GUILayout.EndHorizontal();
    }

    void OnInspectorUpdate()
    {
        Repaint();
    }

    [MenuItem("Tools/Enemy Spawner")]
    public static void Display()
    {
        if (isDisplayed == false)
        {
            EnemySpawnPopup window = CreateInstance<EnemySpawnPopup>();
            GUIContent content = new GUIContent("Enemy Spawner");
            

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
