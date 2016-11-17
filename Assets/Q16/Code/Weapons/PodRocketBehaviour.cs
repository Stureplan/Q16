using UnityEngine;
using System.Collections;

public class PodRocketBehaviour : MonoBehaviour
{
    Rigidbody rb;
    public GameObject explosion;
    ParticleSystem debrisPS;
    Cooldown initialDelay;

    Vector3 randomDir;
    Vector3 centerPoint;
    Quaternion rot;
    Transform initialTransform;
    Transform target;
    float acquisitionRange = 35.0f;
    float force;
    float targetForce = 20.0f;
    bool isColliding;
    bool goToTarget = false;
    bool goReady = false;

    int bitLayer = 12;
    int layerMask;

	void Start ()
    {
        initialTransform = transform;

        rb = GetComponent<Rigidbody>();

        rb.velocity = transform.forward * 5.0f;
        initialDelay = new Cooldown(0.5f);

        randomDir = Utility.RandomVector3(-transform.right, transform.right);
        randomDir += Utility.RandomVector3(Vector3.zero, transform.up);
        randomDir.Normalize();

        layerMask = 1 << 12;

        float f = Stats.info.timesLoaded * 500;
        //force = 2000.0f + f;
        force = 12.0f;

        //new
        rb.useGravity = true;
        rb.AddForce((transform.forward + (Utility.RandomVector3() * 0.1f)) * 10.0f, ForceMode.Impulse);
        rb.AddTorque(Utility.RandomVector3(-Vector3.one, Vector3.one), ForceMode.Impulse);
    }

    void Update ()
    {
        isColliding = false;
        initialDelay.UpdateTimer();



        if (initialDelay.ActionReady())
        {
            initialDelay.StopCooldown();

            //Find target
            target = FindNearestTarget();


            //new
            rb.useGravity = false;
            rb.freezeRotation = true;


            if (target != null)
            {
                goToTarget = true;
            }
            else
            {
                goReady = true;
            }

        }

        if (target != null)
        {
            Vector3 finalPos = target.position;
            finalPos.y = centerPoint.y;
            Vector3 dir = finalPos - transform.position;
            dir.Normalize();
            rot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, 400.0f * Time.deltaTime);

            rb.velocity = transform.forward * force;
        }
        if (target == null && goReady)
        {
            rot = Quaternion.LookRotation(randomDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, 400.0f * Time.deltaTime);

            //Accelerate towards random target
            rb.velocity = transform.forward * force;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Killbox")
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (isColliding) { return; }
        isColliding = true;

        ContactPoint[] pts = other.contacts;

        Collider[] explosionHits = Physics.OverlapSphere(transform.position, 2.0f);
        for (int i = 0; i < explosionHits.Length; i++)
        {
            if (explosionHits[i].tag == "Enemy")
            {
                EnemyBehaviour enemy = explosionHits[i].GetComponent<EnemyBehaviour>();
                enemy.Damage(50, DAMAGE_TYPE.EXPLOSION);

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

        Explode();  
    }


    Transform FindNearestTarget()
    {
        Transform t = null;

        Collider[] colliders;
        colliders = Physics.OverlapSphere(transform.position, acquisitionRange, layerMask);

        int choice = Random.Range(0, colliders.Length);


        float distance = 10000.0f;
        for (int i = 0; i < colliders.Length; i++)
        {
            float d = Vector3.Distance(transform.position, colliders[i].transform.position);
            if (d < distance && i == choice)
            {
                t = colliders[i].transform;
                centerPoint = colliders[i].bounds.center;
                distance = d;
            }
        }

        return t;
    }

    void Explode()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);

        Destroy(this.gameObject);
    }
}
