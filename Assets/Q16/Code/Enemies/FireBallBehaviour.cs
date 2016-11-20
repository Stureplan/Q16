using UnityEngine;
using System.Collections;

public class FireBallBehaviour : ProjectileBehaviour
{
    public GameObject psPrefab;

    Rigidbody rb;
    float force = 5.0f;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force, ForceMode.Impulse);
	}
	
	void Update ()
    {
	
	}

    void OnTriggerEnter(Collider other)
    {
        IDamageable entity;
        if (other.gameObject.IsDamageable(out entity))
        {
            entity.Damage(25, DAMAGE_TYPE.FIRE, sender);

            if (entity.Type() == SENDER_TYPE.PLAYER)
            {
                Vector3 force = transform.forward;
                force.y = 0.5f;
                other.gameObject.GetComponent<PlayerInput>().AddForce(force, 15.0f);
            }

            Explode();
            return;
        }


        if (other.tag == "Killbox")
        {
            Explode();
            return;
        }





        if (other.tag != "EnemyShot")
        {
            Explode();
            return;
        }
    }

    void Explode()
    {
        Instantiate(psPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
