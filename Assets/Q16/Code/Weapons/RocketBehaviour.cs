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
            Explode();
        }
	}

	void OnCollisionEnter(Collision other)
	{
		if (isColliding){ return; }
		isColliding = true;

		ContactPoint[] pts = other.contacts;

        //Direct enemy hit
        IDamageable dmg;
        if (other.gameObject.IsDamageable(out dmg))
        {
            dmg.Explosion(100, DAMAGE_TYPE.EXPLOSION, SenderInfo.Player(), transform.position, 20.0f);
        }
        else
        {
            EmitParticles(pts[0]);

        }


        Explode();
    }

    void Explode()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);


        //TODO: Check if explosionHits is larger than 1 on direct impact (could be several impacts per collider..)
        //TODO: Make sure world destroyable objects can be detected
        Collider[] explosionHits = Physics.OverlapSphere(transform.position, 2.5f);
        for (int i = 0; i < explosionHits.Length; i++)
        {
            IDamageable entity;
            if (explosionHits[i].gameObject.IsDamageable(out entity))
            {
                entity.Explosion(75, DAMAGE_TYPE.EXPLOSION, SenderInfo.Player(), transform.position, 20.0f);
                if (entity.Health() <= 0)
                {
                    Vector3 hitDir = explosionHits[i].transform.position - transform.position;
                    hitDir = hitDir.normalized;

                    entity.DeathDirection(hitDir, 25.0f);
                }
            }
        }



        Destroy(this.gameObject);
    }

	void EmitParticles(ContactPoint pt)
	{
        debrisPS.transform.position = pt.point;
        debrisPS.transform.rotation = Quaternion.LookRotation (pt.normal);
        debrisPS.Emit (5);
	}
}
