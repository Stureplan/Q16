using UnityEngine;
using System.Collections;

public class ActionSpawnEnemy : ActionObject
{
    private GameObject prefab;

    public void SetPrefab(GameObject go)
    {
        prefab = go;
    }

    public override void Action(SenderInfo sender)
    {
        Instantiate(prefab, transform.position, transform.rotation);
    }
}
