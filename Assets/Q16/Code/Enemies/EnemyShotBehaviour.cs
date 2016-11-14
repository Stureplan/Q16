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
            PlayerInput p = other.gameObject.GetComponent<PlayerInput>();

            h.Damage(15);

            Vector3 force = transform.forward;
            force.y = 0.5f;
            p.AddForce(force, 7.5f);

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
