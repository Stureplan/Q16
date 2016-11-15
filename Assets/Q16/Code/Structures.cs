using UnityEditor;
using UnityEngine;
using System.Collections;

public enum INTERACTION_TYPE
{
    TRIGGER,
    USE,
    PUSH
}

public enum ACTION_TYPE
{
    MOVE,
    ROTATE,
    HIDE,
    SHOW,
    SPAWN_ENEMY,
    CUSTOM
}

public enum SENDER_TYPE
{
    PLAYER,
    ENEMY,
    OBJECT
}

public enum SPLASH_TYPE
{
    SMALL,
    MEDIUM,
    LARGE
}

public struct SenderInfo
{
    public Transform s_Transform;
    public SENDER_TYPE s_Type;
    public string s_Tag;
}


[System.Serializable]
public struct EnemySpawn
{
    public GameObject e_Prefab;
    public Transform e_Transform;
    public Vector3 e_Point;
}