using UnityEngine;
using System.Collections;

public class EnemyShotBehaviour : MonoBehaviour 
{
	Rigidbody rb;
	ParticleSystem impactFX;

	float force = 7.5f;
	bool isColliding;
	
	

	void Start () 
	{
		rb = GetComponent<Rigidbody>();
		rb.AddForce (transform.forward * force, ForceMode.Impulse);
		impactFX = GameObject.Find ("ImpactPS").GetComponent<ParticleSystem>();
	}
	
	void Update () 
	{
	
	}

    void OnTriggerEnter(Collider other)
    {
        //if (isColliding) { return; }
        //isColliding = true;

        if (other.tag == "Killbox")
        {
            Destroy(this.gameObject);
            return;
        }

        if (other.tag == "Player")
        {
            //TODO: UI effect?

            Health h = other.gameObject.GetComponent<Health>();
            h.Damage(10);
            SelfDestruct();
        }

        if (other.tag != "EnemyShot")
        {
            SelfDestruct();

        }
    }

	void OnCollisionEnter(Collision other)
	{

    }

	void SelfDestruct()
	{
		impactFX.transform.position = transform.position;
		impactFX.Emit (15);
		Destroy (this.gameObject);
	}
}
