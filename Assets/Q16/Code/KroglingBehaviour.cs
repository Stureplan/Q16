using UnityEngine;
using System.Collections;

public class KroglingBehaviour : EnemyBehaviour
{

	void Start ()
    {
        SetupReferences();

        rotateSpeed = 45.0f;
        maxHealth = 75;
        health = 75;


        float CD = 1.5f;
        if (Stats.info.amountHit > 0 && Stats.info.amountShot > 0)
        {
            CD = CD * Stats.info.amountHit / Stats.info.amountShot;
            if (CD < 0.4f) { CD = 0.4f; }
        }
        fireRate = new Cooldown(CD);
	}
	
	void Update ()
    {
        fireRate.UpdateTimer();

        if (health <= 0)
        {
            Kill(Mathf.Abs(health));
        }





	}


    public override void Kill(int overkill)
    {
        if (overkill >= maxHealth)
        {
            EmitParticles();
        }
        else
        {
            //TODO: death animations or ragdoll
        }

        Stats.info.enemiesKilled++;
        Destroy(this.gameObject);
    }
}
