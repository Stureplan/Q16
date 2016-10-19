using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RocketBehaviour : MonoBehaviour {

	GameObject FX;
	ParticleSystem debrisPS;
	public GameObject explosion;
	Rigidbody rb;


    float force = 15.0f;
	bool isColliding;

    	

	void Start () 
	{
		rb = GetComponent<Rigidbody>();
		rb.AddForce (transform.forward * force, ForceMode.Impulse);

		
		FX = GameObject.Find("RocketDebris");
        debrisPS = FX.GetComponent<ParticleSystem>();
	}
	
	void Update () 
	{
		isColliding = false;
	}

	void OnTriggerEnter (Collider col)
	{
        if (col.tag == "Killbox")
        {
            Destroy(this.gameObject);
        }
	}

	void OnCollisionEnter(Collision other)
	{
		if (isColliding){ return; }
		isColliding = true;

		ContactPoint[] pts = other.contacts;

		if (other.gameObject.tag == "Killbox")
		{
			Destroy (this.gameObject);
		}

		if (other.gameObject.tag == "World" || other.gameObject.tag == "WorldProp")
		{
			EmitParticles (pts[0]);
			Instantiate (explosion, transform.position, Quaternion.identity);
			
			Destroy (this.gameObject);
		}
		if (other.gameObject.tag == "Enemy")
		{
			//Direct enemy hit
			Instantiate (explosion, transform.position, Quaternion.identity);

			EnemyBehaviour enemy = other.gameObject.GetComponent<EnemyBehaviour>();
			enemy.Damage (125);

            Stats.info.amountHit++;
			Destroy (this.gameObject);
		}

        //TODO: Check if explosionHits is larger than 1 on direct impact (could be several impacts per collider..)
        //TODO: Make sure world destroyable objects can be detected
		Collider[] explosionHits = Physics.OverlapSphere (transform.position, 2.5f);
		for (int i = 0; i < explosionHits.Length; i++)
		{
			if (explosionHits[i].tag == "Enemy")
			{
				//Hit enemy in vicinity
				EnemyBehaviour enemy = explosionHits[i].GetComponent<EnemyBehaviour>();
				enemy.Damage (75);
              //  enemy.Push()


                // PUSH RAGDOLL
                if (enemy.GetHealth() < 0)
                {
                    Vector3 hitDir = explosionHits[i].transform.position - transform.position;
                    hitDir = hitDir.normalized;

                    enemy.SetDeathDirection(hitDir, 25.0f);
                }


                Stats.info.amountHit++;
			}

			if (explosionHits[i].tag == "Player")
			{
                PlayerInput m = explosionHits[i].gameObject.GetComponent<PlayerInput>();
                //m.Jump(15.0f);
                m.AddForce(explosionHits[i].transform.position - transform.position, 20.0f);
			}
		}

	}


	void EmitParticles(ContactPoint pt)
	{
        debrisPS.transform.position = pt.point;
        debrisPS.transform.rotation = Quaternion.LookRotation (pt.normal);
        debrisPS.Emit (5);
	}
}
