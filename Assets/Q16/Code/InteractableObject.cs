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
    public Transform aTransformMove;
    public Transform aTransformToMove;
    public float aDistancePerSecondsMove;
    // MOVE AT CONSTANT SPEED....V
    // transform.position = Vector3.MoveTowards(transform.position, v3TargetPosition, maxDistPerSecond * Time.deltaTime);


    //FOR ROTATE
    public Transform aTransformRotate;
    public Vector3 aAxisRotate;
    public float aAnglesToRotate;
    public float aAnglesPerSecondRotate;
    // ROTATE...V
    // float step = aAnglesPerSecondRotate * Time.deltaTime;
    // transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, step);

    //FOR HIDE
    public Transform aHide;

    //FOR SHOW
    public Transform aShow;
    /* ------------------------------------------------------ */

    void Start ()
    {

	}
	
	void Update ()
    {
	    
	}
}

[CustomEditor(typeof(InteractableObject))]
public class InteractableObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        InteractableObject iObject = target as InteractableObject;

        iObject.iType = (INTERACTION_TYPE)EditorGUILayout.EnumPopup("Interaction Type:", iObject.iType);
        iObject.aType = (ACTION_TYPE)EditorGUILayout.EnumPopup("Action Type:", iObject.aType);

        ShowInteractionGUI(iObject.iType);
        ShowActionGUI(iObject.aType);
    }

    void ShowInteractionGUI(INTERACTION_TYPE iTypeGUI)
    {
        switch (iTypeGUI)
        {
            case INTERACTION_TYPE.TRIGGER:
                HideAndClearVariables();
                serializedObject.Update();
                GUILayout.Space(30.0f);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("iColliderTrigger"), new GUIContent("Trigger Object"));
                serializedObject.ApplyModifiedProperties();
                break;

            case INTERACTION_TYPE.USE:
                break;

            case INTERACTION_TYPE.PUSH:
                break;

        }
    }

    void ShowActionGUI(ACTION_TYPE aTypeGUI)
    {
        switch (aTypeGUI)
        {
            case ACTION_TYPE.MOVE:
                HideAndClearVariables();
                serializedObject.Update();

                GUILayout.Space(30.0f);
                GUILayout.Label("Movement Options", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("aTransformMove"), new GUIContent("Move Object"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("aTransformToMove"), new GUIContent("Move To"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("aDistancePerSecondsMove"), new GUIContent("Distance Per Second"));

                serializedObject.ApplyModifiedProperties();
                break;

            case ACTION_TYPE.ROTATE:
                break;

            case ACTION_TYPE.HIDE:
                break;

            case ACTION_TYPE.SHOW:
                break;

        }
    }

    void HideAndClearVariables()
    {

    }
}
