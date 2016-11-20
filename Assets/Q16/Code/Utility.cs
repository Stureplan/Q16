using UnityEngine;
using System.Collections;

public static class Utility
{
    public static GameObject InstantiateProjectile(GameObject prefab, Vector3 pt, Quaternion rot, SenderInfo sender)
    {
        GameObject go = (GameObject)GameObject.Instantiate(prefab, pt, rot);
        go.GetComponent<ProjectileBehaviour>().sender = sender;

        return go;
    }

    public static Vector3 RandomVector3(Vector3 a, Vector3 b)
    {
        Vector3 ret;
        
        ret.x = Random.Range(a.x, b.x);
        ret.y = Random.Range(a.y, b.y);
        ret.z = Random.Range(a.z, b.z);

        return ret;
    }

    public static Vector3 RandomVector3()
    {
        Vector3 ret;

        ret.x = Random.Range(-1.0f, 1.0f);
        ret.y = Random.Range(-1.0f, 1.0f);
        ret.z = Random.Range(-1.0f, 1.0f);
        ret.Normalize();

        return ret;
    }

    public static Vector3 RoundVector3(Vector3 vec)
    {
        vec.x = Mathf.RoundToInt(vec.x);
        vec.y = Mathf.RoundToInt(vec.y);
        vec.z = Mathf.RoundToInt(vec.z);

        return vec;
    }

    public static bool IsDamageable(this GameObject obj, out IDamageable entity)
    {
        bool hasComponent = false;

        entity = obj.GetComponent<IDamageable>();

        if (entity != null)
        {
            hasComponent = true;
        }

        return hasComponent;
    }

    public static SenderInfo Sender(this MonoBehaviour mb, SENDER_TYPE type)
    {
        SenderInfo sender;

        sender.s_Tag = mb.tag;
        sender.s_Transform = mb.transform;
        sender.s_Type = type;

        return sender;
    }

    static Transform p_Transform;
    public static void SetPlayer(Transform p)
    {
        p_Transform = p;
    }
    public static Transform FindPlayer()
    {
        p_Transform = GameObject.Find("Guy").transform;
        return p_Transform;
    }
    public static Transform Player()
    {
        if (p_Transform != null)
        {
            return p_Transform;
        }
        else
        {
            return FindPlayer();
        }
    }
}
