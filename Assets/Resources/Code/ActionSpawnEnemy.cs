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
        GameObject go = (GameObject)Instantiate(prefab, transform.position, transform.rotation);

        EnemyBehaviour eb = go.GetComponent<EnemyBehaviour>();
        eb.SetPlayer(sender.s_Transform);
    }
}
