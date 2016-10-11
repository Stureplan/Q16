using UnityEngine;
using System.Collections;

public static class ExtensionMethods
{
   // public static void PushCharacter(this PlayerInput player)
   // {
        
   // }

    public static bool CheckTag(GameObject obj, string tag)
    {
        bool hasTag = false;

        if (obj.GetComponent<TagScript>().HasTag(tag))
        {
            hasTag = true;
        }

        return hasTag;
    }
}
