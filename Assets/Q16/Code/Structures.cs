using UnityEditor;
using UnityEngine;
using System.Collections;

public interface IDamageable
{
    SENDER_TYPE Type();
    void DeathDirection(Vector3 dir, float power);
    void Damage(int dmg, DAMAGE_TYPE type, SenderInfo sender);
    void Explosion(int dmg, DAMAGE_TYPE type, SenderInfo sender, Vector3 point, float force);
    int Health();
}

public enum WPN_TYPE
{
    SHOTGUN = 0,
    SUPERSHOTGUN,
    GRENADELAUNCHER,
    ROCKETLAUNCHER,
    PLASMAGUN,
    RAILGUN,
    PODLAUNCHER,
    LAVAGUN,
    SUNSTAFF
}

public enum ENEMY_TYPE
{
    GOLEM,
    CULTIST,
    TENTACLE_WARRIOR
}

public enum INTERACTION_TYPE
{
    TRIGGER,
    USE,
    PUSH
}

public enum CONDITION_TYPE
{
    NONE,
    ITEM,
    ACTIVATED,
    CUSTOM
}

public enum ACTION_TYPE
{
    MOVE,
    ROTATE,
    HIDE,
    SHOW,
    SPAWN_ENEMY,
    SECRET,
    CUSTOM
}

public enum SENDER_TYPE
{
    PLAYER,
    ENEMY,
    OBJECT
}

public enum DAMAGE_TYPE
{
    PROJECTILE,
    BUCKSHOT,
    EXPLOSION,
    FIRE,
    PLASMA,
    MELEE
}

public enum SPLASH_TYPE
{
    SMALL,
    MEDIUM,
    LARGE
}

public struct Item
{
    public string name;

    public static Item Get(string n)
    {
        Item item;
        item.name = n;

        return item;
    }

/*    public static Item SkeletonKey()
    {
        Item item;
        item.name = "Skeleton Key";

        return item;
    }

    public static Item FleshKey()
    {
        Item item;
        item.name = "Flesh Key";

        return item;
    }
   
    public static Item QuadDamage()
    {
        Item item;
        item.name = "Quad Damage";

        return item;
    }
*/

    public static string[] ItemNames()
    {
        string[] names = 
        {
            "Skeleton Key",
            "Flesh Key",
            "Silver Key",
            "Quad Damage"
        };

        return names;
    }
}

public struct SenderInfo
{
    public Transform s_Transform;
    public SENDER_TYPE s_Type;
    public string s_Tag;

    public static SenderInfo Player()
    {
        SenderInfo p;

        p.s_Tag = "Player";
        p.s_Transform = Utility.Player();
        p.s_Type = SENDER_TYPE.PLAYER;

        return p;
    }

    public static SenderInfo Object()
    {
        SenderInfo p;

        p.s_Tag = "World";
        p.s_Transform = null;
        p.s_Type = SENDER_TYPE.OBJECT;

        return p;
    }

    public static int ID = 1;
    public static SenderInfo Create(string tag, Transform t, SENDER_TYPE type)
    {
        SenderInfo sender;

        sender.s_Tag = tag;
        sender.s_Transform = t;
        sender.s_Type = type;

        return sender;
    }
}

[System.Serializable]
public struct Move
{
    public Transform transformToMove;
    public Vector3 toPosition;
    public bool snapMove;
    public float duration;
}


[System.Serializable]
public struct EnemySpawn
{
    public GameObject e_Prefab;
    public Transform e_Transform;
    public Vector3 e_Point;
}