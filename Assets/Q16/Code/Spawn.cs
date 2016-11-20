using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour 
{
    public GameObject ui;
	public GameObject guy;
	public GameObject spawn;

	GameObject player;
	GameObject[] enemies;

	void Start () 
	{
		player = (GameObject)Instantiate (guy, spawn.transform.position, Quaternion.identity);
        Utility.SetPlayer(player.transform);

		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		for (int i = 0; i < enemies.Length; i++)
		{
			EnemyBehaviour eb = enemies[i].GetComponent<EnemyBehaviour>();
			eb.SetPlayer(player.transform);
		}

        MessageLog.InitializeLog(ui.GetComponentInChildren<UILog>());
	}
}
