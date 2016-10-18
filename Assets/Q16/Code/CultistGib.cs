using UnityEngine;
using System.Collections;

public class CultistGib : MonoBehaviour
{
    Rigidbody rb;
    float force;
	void Start ()
    {


        int shouldSpawn = Random.Range(0, 5);
        if (shouldSpawn >= 3)
        {
            //disable GameObject
        }

        force = Random.Range(1.0f, 8.0f);

        rb = GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * (force/5.0f), ForceMode.Impulse);
        rb.AddForce(Vector3.up * force, ForceMode.Impulse);

        rb.AddTorque(Utility.RandomVector3() * 5.0f, ForceMode.Impulse);
	}
	
	void Update ()
    {
	
	}
}
