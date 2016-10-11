using UnityEngine;
using System.Collections;

public class OnFire : MonoBehaviour
{
    EnemyBehaviour enemy;
    Cooldown cd;
    float cdTimer = 0.2f;

	void Start ()
    {
        enemy = GetComponent<EnemyBehaviour>();
        cd = new Cooldown(cdTimer);
	}
	
	// Update is called once per frame
	void Update ()
    {
        cd.UpdateTimer();

        if (cd.ActionReady())
        {
            enemy.Damage(5);
            cd.ResetTimer();
        }
	}
}
