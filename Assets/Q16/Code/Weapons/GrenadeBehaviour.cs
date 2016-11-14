using UnityEngine;
using System.Collections;

public class GrenadeBehaviour : MonoBehaviour
{
    public GameObject explosion;
    Rigidbody rb;
    Cooldown explodeCD;

    float force = 10.0f;
    bool isColliding;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 direction = transform.forward * 1.5f;
        direction += transform.up * 0.25f;
        Vector3 rotation = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f));

        rb.AddForce(direction * force, ForceMode.Impulse);
        rb.AddTorque(rotation);
        
        explodeCD = new Cooldown(2.5f, true);
	}
	
	void Update ()
    {
        isColliding = false;

        explodeCD.UpdateTimer();
        if (explodeCD.ActionReady())
        {
            Explode();
        }
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Killbox")
        {
            Explode();
        }


        
        if (other.gameObject.tag == "Enemy")
        {
            //Direct Enemy hit
            EnemyBehaviour enemy = other.gameObject.GetComponent<EnemyBehaviour>();
            enemy.Damage(125);

            Stats.info.amountHit++;
            Explode();
        }

        if (other.gameObject.tag == "World" || other.gameObject.tag == "WorldProp")
        {
            //We hit World (or something that doesn't explode on impact)
            return;
        }
    }

    void Explode()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);



        Collider[] explosionHits = Physics.OverlapSphere(transform.position, 3.0f);
        for (int i = 0; i < explosionHits.Length; i++)
        {
            if (explosionHits[i].tag == "Enemy")
            {
                //Hit enemy in vicinity
                EnemyBehaviour enemy = explosionHits[i].GetComponent<EnemyBehaviour>();
                enemy.Damage(75);


                // PUSH RAGDOLL
                if (enemy.GetHealth() < 0)
                {
                    Vector3 hitDir = explosionHits[i].transform.position - transform.position;
                    hitDir = hitDir.normalized;

                    enemy.SetDeathDirection(hitDir, 15.0f);
                }


                Stats.info.amountHit++;
            }

            if (explosionHits[i].tag == "Player")
            {
                PlayerInput m = explosionHits[i].gameObject.GetComponent<PlayerInput>();
                m.AddForce(explosionHits[i].transform.position - transform.position, 20.0f);
            }
        }

        Destroy(this.gameObject);
    }
}
