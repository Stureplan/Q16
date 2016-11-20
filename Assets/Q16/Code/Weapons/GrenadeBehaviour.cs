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
        if (other.gameObject.tag == "World" || other.gameObject.tag == "WorldProp")
        {
            //We hit World (or something that doesn't explode on impact)
            return;
        }

        if (other.gameObject.tag == "Killbox")
        {
            Explode();
            return;
        }

        IDamageable entity;
        if (other.gameObject.IsDamageable(out entity))
        {
            //Direct hit doesn't affect player
            if (entity.Type() != SENDER_TYPE.PLAYER)
            {
                entity.Explosion(75, DAMAGE_TYPE.EXPLOSION, SenderInfo.Player(), transform.position, 15.0f);

                Explode();
            }
        }
    }

    void Explode()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);



        Collider[] explosionHits = Physics.OverlapSphere(transform.position, 3.0f);
        for (int i = 0; i < explosionHits.Length; i++)
        {
            IDamageable entity;
            if (explosionHits[i].gameObject.IsDamageable(out entity))
            {
                entity.Explosion(50, DAMAGE_TYPE.EXPLOSION, SenderInfo.Player(), transform.position, 15.0f);

                if (entity.Health() <= 0)
                {
                    Vector3 hitDir = (explosionHits[i].transform.position - transform.position).normalized;

                    entity.DeathDirection(hitDir, 15.0f);
                }
            }
        }

        Destroy(this.gameObject);
    }
}
