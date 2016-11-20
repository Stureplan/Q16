using UnityEngine;
using System.Collections;

public class LavaProjectileBehaviour : MonoBehaviour
{
    public GameObject fireHitPrefab;

    LavaGun lg;
    Rigidbody rb;
    Cooldown cd;

    float startForce;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        cd = new Cooldown(2.0f);

        rb.AddForce(transform.forward * 7.0f, ForceMode.Impulse);
	}
	
	void Update ()
    {
        cd.UpdateTimer();

        if (cd.ActionReady())
        {
            DestroyAndRemove();
        }
	}

    void OnCollisionEnter(Collision other)
    {
        IDamageable entity;
        if (other.gameObject.IsDamageable(out entity))
        {
            entity.Damage(25, DAMAGE_TYPE.FIRE, SenderInfo.Player());

            if (entity.Health() <= 0)
            {
                Vector3 hitDir = (other.contacts[0].normal - transform.position).normalized;

                entity.DeathDirection(hitDir, 10.0f);
            }
        }
        
        Instantiate(fireHitPrefab, transform.position, Quaternion.LookRotation(other.contacts[0].normal), transform);
    }

    void DestroyAndRemove()
    {
        lg.RemoveLavaObject(gameObject);

        Destroy(this.gameObject);
    }

    public void Initialize(LavaGun l, float strength)
    {
        lg = l;
        startForce = strength;
    }
}
