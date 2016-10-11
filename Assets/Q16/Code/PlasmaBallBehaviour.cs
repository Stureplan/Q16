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

        if (other.tag == "Enemy")
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
