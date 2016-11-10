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