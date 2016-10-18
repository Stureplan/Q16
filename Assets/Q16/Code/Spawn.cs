using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour 
{
	public GameObject guy;
	public GameObject spawn;

	GameObject player;
	GameObject[] enemies;

	// Use this for initialization
	void Start () 
	{
		player = (GameObject)Instantiate (guy, spawn.transform.position, Quaternion.identity);

		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		for (int i = 0; i < enemies.Length; i++)
		{
			EnemyBehaviour eb = enemies[i].GetComponent<EnemyBehaviour>();
			eb.SetPlayer(player.transform);
		}

        MessageLog.InitializeLog();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
