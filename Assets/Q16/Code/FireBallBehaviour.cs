using UnityEngine;
using System.Collections;

public class FireBallBehaviour : MonoBehaviour
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
        if (other.tag == "Killbox")
        {
            Explode();
            return;
        }

        if (other.tag == "Player")
        {
            Health h = other.gameObject.GetComponent<Health>();
            PlayerInput p = other.gameObject.GetComponent<PlayerInput>();
            h.Damage(25);

            Vector3 force = transform.forward;
            force.y = 0.5f;
            p.AddForce(force, 15.0f);

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
