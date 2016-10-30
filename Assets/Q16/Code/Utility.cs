using UnityEngine;
using System.Collections;

public static class Utility
{
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
}
