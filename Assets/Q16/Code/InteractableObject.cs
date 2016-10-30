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
    public float aAnglesPerSecondRotate;
    // ROTATE...V
    // float step = aAnglesPerSecondRotate * Time.deltaTime;
    // transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, step);

    //FOR HIDE
    public Transform aHide;

    //FOR SHOW
    public Transform aShow;

    //FOR CUSTOM
    public ActionObject aCustom;
    /* ------------------------------------------------------ */

    private bool activated = false;


    public void Interact(Transform sender)
    {
        if (!activated)
        {
            switch (aType)
            {
                case ACTION_TYPE.MOVE:
                    Move();
                    break;

                case ACTION_TYPE.ROTATE:
                    break;

                case ACTION_TYPE.HIDE:
                    break;

                case ACTION_TYPE.SHOW:
                    break;

                case ACTION_TYPE.CUSTOM:
                    aCustom.Action(sender);
                    break;
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

        StartCoroutine(IEMove(aTransformToMove.position, aTransformToMove.position + aPositionMove, aDistancePerSecondsMove));
    }

    IEnumerator IEMove(Vector3 a, Vector3 b, float distance)
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

            aTransformToMove.position = Vector3.MoveTowards(aTransformToMove.position, b, delta);

            yield return null;
        }



        //done
    }
}

[CustomEditor(typeof(InteractableObject))]
public class InteractableObjectEditor : Editor
{
    InteractableObject iObject;
    Mesh iObjectMesh;
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
        if (iObject)
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
                EditorGUILayout.PropertyField(serializedObject.FindProperty("aRotationRotate"), new GUIContent("Rotate Object"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("aAnglesToRotate"), new GUIContent("Angles to Rotate"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("aAnglesPerSecondRotate"), new GUIContent("Angles per Second"));
                break;

            case ACTION_TYPE.HIDE:
                break;

            case ACTION_TYPE.SHOW:
                break;
                
            case ACTION_TYPE.CUSTOM:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("aCustom"), new GUIContent("Custom Code"));
                break;
        }
    }
    
    void CustomActionGizmos(ACTION_TYPE type)
    {
        switch(type)
        {
            case ACTION_TYPE.MOVE:
                MoveGizmos();
                break;

            case ACTION_TYPE.ROTATE:
                RotateGizmos();
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


            if (updatedFrames % 120 == 0) iObjectMesh = iObject.GetComponent<MeshFilter>().sharedMesh;
            Graphics.DrawMesh(iObjectMesh, tPos, iObject.transform.rotation, editorMat, 0);
            EditorUtility.SetDirty(iObjectMesh);
        }
    }

    void RotateGizmos()
    {
        if (iObject.aTransformToRotate != null)
        {
            Quaternion rot;
            rot = Quaternion.Euler(iObject.aAnglesToRotate);




            Vector3 forward, up, right;
            Vector3 pos;


            //t = iObject.aTransformToRotate;
            //t.rotation = rot;

            
            pos = iObject.aTransformToRotate.position;

            forward = rot * iObject.aTransformToRotate.forward + pos;
            up = rot * iObject.aTransformToRotate.up + pos;
            right = rot * iObject.aTransformToRotate.right + pos;


            /*forward = t.position + t.forward;
            up = t.position + t.up;
            right = t.position + t.right;
            */
            Handles.color = Color.white;
            Handles.DrawLine(pos, forward);
            Handles.DrawLine(pos, up);
            Handles.DrawLine(pos, right);

            Handles.color = new Vector4(0.0f, 0.0f, 1.0f, 0.5f);
            Handles.ConeCap(0, forward, Quaternion.LookRotation((forward - pos).normalized), 0.1f);

            Handles.color = new Vector4(0.0f, 1.0f, 0.0f, 0.5f);
            Handles.ConeCap(0, up, Quaternion.LookRotation((up - pos).normalized), 0.1f);

            Handles.color = new Vector4(1.0f, 0.0f, 0.0f, 0.5f);
            Handles.ConeCap(0, right, Quaternion.LookRotation((right - pos).normalized), 0.1f);
        }
    }

    void HideAndClearVariables()
    {

    }
}
