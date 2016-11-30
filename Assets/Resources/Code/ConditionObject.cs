using UnityEngine;
using System.Collections;

public class ConditionObject : MonoBehaviour
{
    public virtual bool Condition(SenderInfo sender)
    {
        bool met = false;

        return met;
    }
}
