using UnityEditor;
using UnityEngine;
using System.Collections;

public class InteractableObject : MonoBehaviour
{
    /* --------------------- INTERACTION -------------------- */
    public INTERACTION_TYPE iType;

    //FOR TRIGGER
    public Collider iColliderTrigger;

    //FOR USE
    public Collider iColliderUse;

    //FOR PUSH
    public Collider iColliderPush;
    /* ------------------------------------------------------ */




    /* ---------------------- ACTION ------------------------ */
    public ACTION_TYPE aType;

    //FOR MOVE
    public Transform aTransformToMove;
    public Vector3 aPositionMove;
    public bool aSnapMove = false;
    public float aDistancePerSecondsMove;


    //FOR ROTATE
    public Transform aTransformToRotate;
    public Quaternion aRotationRotate = Quaternion.identity;
    public Vector3 aAnglesToRotate;
    public float aDurationRotate;

    //FOR HIDE
    public Transform aHide;

    //FOR SHOW
    public Transform aShow;

    //FOR SPAWN ENEMY
    public EnemySpawn[] aEnemySpawns;

    //FOR CUSTOM
    public ActionObject aCustom;

    //FOR EXTRA ACTIONS
    public ActionObject[] aExtraCustomActions;
    /* ------------------------------------------------------ */

    private bool activated = false;


    public void Interact(SenderInfo sender)
    {
        if (!activated)
        {
            switch (aType)
            {
                case ACTION_TYPE.MOVE:
                    Move();
                    break;

                case ACTION_TYPE.ROTATE:
                    Rotate();
                    break;

                case ACTION_TYPE.HIDE:
                    aHide.gameObject.SetActive(false);
                    break;

                case ACTION_TYPE.SHOW:
                    aShow.gameObject.SetActive(true);
                    break;

                case ACTION_TYPE.SPAWN_ENEMY:
                    SpawnEnemy();
                    break;

                case ACTION_TYPE.CUSTOM:
                    aCustom.Action(sender);
                    break;
            }

            if (aExtraCustomActions != null && aExtraCustomActions.Length > 0)
            {
                for (int i = 0; i < aExtraCustomActions.Length; i++)
                {
                    aExtraCustomActions[i].Action(sender);
                }
            }
            activated = true;
        }

    }

    void Move()
    {
        if (aSnapMove)
        {
            aPositionMove = Utility.RoundVector3(aPositionMove);
        }

        StartCoroutine(IEMove(aTransformToMove.position + aPositionMove, aDistancePerSecondsMove));
    }

    void Rotate()
    {
        StartCoroutine(IERotate(aRotationRotate, aDurationRotate));
    }

    void SpawnEnemy()
    {
        //GameObject e = (GameObject)Instantiate(aEnemyPrefab, aEnemySpawnPoint.position, aEnemySpawnPoint.rotation);
        //EnemyBehaviour eb = e.GetComponent<EnemyBehaviour>();
        //eb.SetPlayer(GameObject.Find("Guy").transform);
    }

    IEnumerator IEMove(Vector3 to, float distance)
    {

        //here goes repeatable code?
        /*while(true)
        {
            aTransformToMove.position = Vector3.MoveTowards(aTransformToMove.position, aTransformToMove.position + aPositionMove, aDistancePerSecondsMove * Time.deltaTime);
            yield return new WaitForSeconds(distance);
        }*/

        float delta = distance * Time.deltaTime;
        float t = 0.0f;
        while (t < distance)
        {
            t += Time.deltaTime;

            aTransformToMove.position = Vector3.MoveTowards(aTransformToMove.position, to, delta);

            yield return null;
        }



        //done
    }

    IEnumerator IERotate(Quaternion to, float duration)
    {
        float maxAngles = Mathf.Max(Mathf.Abs(aAnglesToRotate.x), Mathf.Abs(aAnglesToRotate.y), Mathf.Abs(aAnglesToRotate.z));

        float t = 0.0f;
        
        while (t < duration)
        {
            t += Time.deltaTime;
            
            float step = (maxAngles * Time.deltaTime) / duration;

            aTransformToRotate.rotation = Quaternion.RotateTowards(aTransformToRotate.rotation, aRotationRotate, step);
            
            yield return null;
        }

    }
}

[CustomEditor(typeof(InteractableObject))]
public class InteractableObjectEditor : Editor
{
    InteractableObject iObject;
    MeshFilter iObjectMesh;
    SkinnedMeshRenderer[] iObjectMeshes;
    
    Material editorMat;
    int updatedFrames = 0;

    void OnEnable()
    {
        //Setup
        iObject = target as InteractableObject;
        editorMat = (Material)AssetDatabase.LoadAssetAtPath("Assets/Resources/Editor/TransparentEditorMesh.mat", typeof(Material));

        
    }

    void OnSceneGUI()
    {
        if (iObject != null)
        {
            CustomActionGizmos(iObject.aType);
            updatedFrames++;
            if (updatedFrames > 100)
            {
                updatedFrames = 0;
            }
        }
    }

    public override void OnInspectorGUI()
    {
        iObject = target as InteractableObject;
        
        iObject.iType = (INTERACTION_TYPE)EditorGUILayout.EnumPopup("Interaction Type:", iObject.iType);
        iObject.aType = (ACTION_TYPE)EditorGUILayout.EnumPopup("Action Type:", iObject.aType);

        HideAndClearVariables();
        serializedObject.Update();

        ShowInteractionGUI(iObject.iType);
        ShowActionGUI(iObject.aType);
        

        serializedObject.ApplyModifiedProperties();
    }

    void ShowInteractionGUI(INTERACTION_TYPE iTypeGUI)
    {
        GUILayout.Space(25.0f);
        GUILayout.Label("Interaction Options", EditorStyles.boldLabel);

        switch (iTypeGUI)
        {
            case INTERACTION_TYPE.TRIGGER:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("iColliderTrigger"), new GUIContent("Trigger Collider"));
                break;

            case INTERACTION_TYPE.USE:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("iColliderUse"), new GUIContent("Use Collider"));
                break;

            case INTERACTION_TYPE.PUSH:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("iColliderPush"), new GUIContent("Push Collider"));
                break;
        }
    }

    void ShowActionGUI(ACTION_TYPE aTypeGUI)
    {
        GUILayout.Space(25.0f);
        GUILayout.Label("Action Options", EditorStyles.boldLabel);
        
        switch (aTypeGUI)
        {
            case ACTION_TYPE.MOVE:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("aTransformToMove"), new GUIContent("Move Object"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("aPositionMove"), new GUIContent("Move to Position"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("aSnapMove"), new GUIContent("Snap Movement"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("aDistancePerSecondsMove"), new GUIContent("Distance Per Second"));
                break;

            case ACTION_TYPE.ROTATE:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("aTransformToRotate"), new GUIContent("Rotate Object"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("aAnglesToRotate"), new GUIContent("Angles to Rotate"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("aDurationRotate"), new GUIContent("Rotation Duration"));
                break;

            case ACTION_TYPE.HIDE:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("aHide"), new GUIContent("Hide Object"));

                break;

            case ACTION_TYPE.SHOW:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("aShow"), new GUIContent("Show Object"));

                break;

            case ACTION_TYPE.SPAWN_ENEMY:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("aEnemySpawns"), new GUIContent("Enemy Spawns"), true);
                if (GUILayout.Button("Add Enemy Spawn"))
                {
                    EnemySpawnPopup.Display();
                }
                break;
                
            case ACTION_TYPE.CUSTOM:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("aCustom"), new GUIContent("Custom Code"));
                break;
        }

        GUILayout.Space(15.0f);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("aExtraCustomActions"), new GUIContent("Extra Actions"), true);
    }

    void CustomActionGizmos(ACTION_TYPE type)
    {
        ClearGizmos();

        switch (type)
        {
            case ACTION_TYPE.MOVE:
                MoveGizmos();
                break;

            case ACTION_TYPE.ROTATE:
                RotateGizmos();
                break;

            case ACTION_TYPE.SPAWN_ENEMY:
                SpawnEnemyGizmos();
                break;
        }
    }

    void MoveGizmos()
    {
        if (iObject.aTransformToMove != null)
        {
            Vector3 fPos, tPos;
            Vector3 a = iObject.aTransformToMove.position;


            fPos = iObject.aTransformToMove.position;
            tPos = iObject.aPositionMove + fPos;
            if (iObject.aSnapMove)
            {
                tPos = Utility.RoundVector3(tPos);
            }



            Vector3 dir = (tPos - fPos).normalized;
            Quaternion rot = Quaternion.identity;
            if (dir != Vector3.zero) rot = Quaternion.LookRotation(dir);



            Handles.color = Color.white;
            Handles.DrawLine(fPos, tPos);
            Handles.CircleCap(0, tPos, rot, 0.25f);

            
            iObjectMesh = iObject.GetComponent<MeshFilter>();
            if (iObjectMesh != null)
            {
                Graphics.DrawMesh(iObjectMesh.sharedMesh, tPos, iObject.transform.rotation, editorMat, 0);
            }
        }
    }

    void RotateGizmos()
    {
        if (iObject.aTransformToRotate != null)
        {
            Quaternion rot = Quaternion.Euler(iObject.aAnglesToRotate);




            Vector3 forward, up, right;
            Vector3 pos;


            //t = iObject.aTransformToRotate;
            //t.rotation = rot;

            
            pos = iObject.aTransformToRotate.position;

            forward = rot * iObject.aTransformToRotate.forward  + pos;
            up      = rot * iObject.aTransformToRotate.up       + pos;
            right   = rot * iObject.aTransformToRotate.right    + pos;

            iObject.aRotationRotate = rot;


            Handles.color = Color.white;
            Handles.DrawLine(pos, forward);
            Handles.DrawLine(pos, up);
            Handles.DrawLine(pos, right);



            Handles.color = new Vector4(1.0f, 0.0f, 0.0f, 0.25f);
            Handles.ConeCap(0, right, Quaternion.LookRotation((right - pos).normalized), 0.1f);
            Handles.DrawSolidArc(pos, rot * iObject.aTransformToRotate.forward, rot * iObject.aTransformToRotate.right, 90.0f, 0.25f);


            Handles.color = new Vector4(0.0f, 1.0f, 0.0f, 0.25f);
            Handles.ConeCap(0, up, Quaternion.LookRotation((up - pos).normalized), 0.1f);
            Handles.DrawSolidArc(pos, rot * iObject.aTransformToRotate.right, rot * iObject.aTransformToRotate.up, 90.0f, 0.25f);


            Handles.color = new Vector4(0.0f, 0.0f, 1.0f, 0.25f);
            Handles.ConeCap(0, forward, Quaternion.LookRotation((forward - pos).normalized), 0.1f);
            Handles.DrawSolidArc(pos, rot * iObject.aTransformToRotate.up, rot * iObject.aTransformToRotate.forward, 90.0f, 0.25f);


            


            Handles.color = Color.white;

            Handles.DrawWireArc(pos, rot * iObject.aTransformToRotate.forward, rot * iObject.aTransformToRotate.right, 90.0f, 0.25f);
            Handles.DrawWireArc(pos, rot * iObject.aTransformToRotate.right, rot * iObject.aTransformToRotate.up, 90.0f, 0.25f);
            Handles.DrawWireArc(pos, rot * iObject.aTransformToRotate.up, rot * iObject.aTransformToRotate.forward, 90.0f, 0.25f);

            iObjectMesh = iObject.GetComponent<MeshFilter>();
            if (iObjectMesh != null)
            {
                Graphics.DrawMesh(iObjectMesh.sharedMesh, pos, rot, editorMat, 0);
            }
        }
    }

    void SpawnEnemyGizmos()
    {
        for (int i = 0; i < iObject.aEnemySpawns.Length; i++)
        {
            Vector3 pos;
            Vector3 forward;
            Vector3 up;
            Quaternion rot;

            pos = iObject.aEnemySpawns[i].e_Point.position;
            rot = iObject.aEnemySpawns[i].e_Point.rotation;
            forward = iObject.aEnemySpawns[i].e_Point.forward;
            up = iObject.aEnemySpawns[i].e_Point.up;

            Handles.color = Color.white;
            Handles.DrawLine(pos, pos + forward);
            Handles.ConeCap(0, pos + forward, Quaternion.LookRotation(forward), 0.1f);
            Handles.CircleCap(0, pos, Quaternion.LookRotation(up), 0.5f);


            iObjectMeshes = iObject.aEnemySpawns[i].e_Prefab.GetComponentsInChildren<SkinnedMeshRenderer>();
            if (iObjectMeshes != null)
            {
                for (int j = 0; j < iObjectMeshes.Length; j++)
                {
                    Graphics.DrawMesh(iObjectMeshes[j].sharedMesh, pos, rot, editorMat, 0);
                }
            }
        }
    }

    void ClearGizmos()
    {
        EditorUtility.SetDirty(target);
    }

    void HideAndClearVariables()
    {

    }
}
