using UnityEngine;
using System.Collections;

public class ActionObject : MonoBehaviour
{
    public virtual void Action(SenderInfo sender)
    {
        //TODO: sender should not be Transform
        //but should be Character or
        //something similar. Inherited by Player
        //and EnemyBehaviour
    }
}
