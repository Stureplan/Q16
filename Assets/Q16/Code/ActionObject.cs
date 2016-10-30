using UnityEngine;
using System.Collections;

public class ActionObject : MonoBehaviour
{
    public virtual void Action(Transform sender)
    {
        //TODO: sender should not be Transform
        //but should be Character or
        //something similar. Inherited by Player
        //and EnemyBehaviour
    }
}
