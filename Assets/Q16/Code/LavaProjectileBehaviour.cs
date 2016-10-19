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
        if (other.gameObject.tag == "Enemy")
        {
            EnemyBehaviour enemy = other.gameObject.GetComponent<EnemyBehaviour>();
            enemy.Damage(25);

            /*CultistFireFX fx = other.transform.GetComponentInParent<CultistFireFX>();

            if (fx != null)
            {
                fx.StartFadingShader();
                fx.SpawnImpactPS(other.transform.position);
            }*/


            if (enemy.GetHealth() < 0)
            {
                Vector3 hitDir = other.contacts[0].normal - transform.position;
                hitDir.Normalize();

                enemy.SetDeathDirection(hitDir, 10.0f);
                
            }
        }

        if (other.gameObject.tag == "World" || other.gameObject.tag == "WorldProp")
        {
            Instantiate(fireHitPrefab, transform.position, Quaternion.LookRotation(other.contacts[0].normal), transform);
        }
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
