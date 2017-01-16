using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDestroyEffect : ActionObject
{
    public GameObject effectPrefab;

    public override void Action(SenderInfo sender)
    {
        GameObject go = Instantiate(effectPrefab, transform.position, Quaternion.identity);
        Destroy(go, 5.0f);

        Destroy(gameObject);
    }

}
