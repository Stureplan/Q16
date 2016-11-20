using UnityEngine;
using System.Collections;

public class PlasmaBallBehaviour : MonoBehaviour
{
    public GameObject psPrefab;
    Rigidbody rb;


    float force = 30.0f;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force, ForceMode.Impulse);
	}
	
	void Update ()
    {
        //transform.Rotate(Vector3.up, 2.0f);
	}

    void OnTriggerEnter(Collider other)
    {
        Instantiate(psPrefab, (transform.position - transform.forward), Quaternion.identity);

        //TODO: Triggers aren't "damabeable" but still stop shots.
        //Be aware of walls, floors etc that aren't "damageable" but are
        //SUPPOSED to stop shots!

        IDamageable entity;
        if (other.gameObject.IsDamageable(out entity))
        {
            entity.Damage(25, DAMAGE_TYPE.PLASMA, SenderInfo.Player());

            if (entity.Type() == SENDER_TYPE.ENEMY)
            {
                EnemyBehaviour enemy = other.gameObject.GetComponent<EnemyBehaviour>();
                enemy.Push(transform.forward, 2.5f);
            }

            if (entity.Health() <= 0)
            {
                entity.DeathDirection(transform.forward, 10.0f);
            }
        }
        else if (other.tag == "EnemyHead")
        {
            entity = other.GetComponentInParent<IDamageable>();
            entity.Damage(25, DAMAGE_TYPE.PLASMA, SenderInfo.Player());

            if (entity.Type() == SENDER_TYPE.ENEMY)
            {
                EnemyBehaviour enemy = other.gameObject.GetComponentInParent<EnemyBehaviour>();
                enemy.Push(transform.forward, 2.5f);
            }

            if (entity.Health() <= 0)
            {
                entity.DeathDirection(transform.forward, 10.0f);
            }
        }

        else if (other.tag == "CultistWeapon")
        {
            CultistFireFX fx = other.transform.GetComponentInParent<CultistFireFX>();
            fx.StartFadingShader();
            fx.SpawnImpactPS(other.transform.position);
        }

        Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision other)
    {
        //TODO: Check if this even works (???)


        /*
        ContactPoint[] pts = other.contacts;
        Quaternion normal = Quaternion.LookRotation(pts[0].normal);
        
        Instantiate(psPrefab, transform.position, normal);


        
        if (other.gameObject.tag == "Enemy")
        {
            EnemyBehaviour enemy = other.gameObject.GetComponent<EnemyBehaviour>();
            enemy.Damage(25);

            Vector3 direction = transform.forward;
            enemy.Push(direction, 2.5f);

            // PUSH RAGDOLL
            if (enemy.GetHealth() < 1)
            {
                enemy.SetDeathDirection(direction, 10.0f);
            }
        }


        Destroy(this.gameObject);*/
    }
}
