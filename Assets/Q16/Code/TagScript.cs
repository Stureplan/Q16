using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TagScript : MonoBehaviour
{
    public List<string> Tags;
	
    void Start()
    {
        Tags = new List<string>();
    }

    public bool HasTag(string tag)
    {
        if (Tags.Contains(tag))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
