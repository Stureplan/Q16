using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour, IDamageable
{
    public virtual void DamageI(int dmg, DAMAGE_TYPE type) { }

    public enum ENEMY_TYPE
    {
        GOLEM,
        CULTIST,
        TENTACLE_WARRIOR
    }


    public Transform player;
    public GameObject deathFX;
    public GameObject enemyGibs;

    public ParticleSystem chunkPS;

    public SkinnedMeshRenderer mr;


    public float rotateSpeed;
    public float moveSpeed;
    public int maxHealth;
    public int health;
    public Cooldown fireRate;

    // FOR RAGDOLL
    protected Vector3 deathDirection = Vector3.zero;
    protected float deathPower = 0.0f;

    public void SetupReferences()
    {
        deathFX = GameObject.Find("BloodChunks");
        chunkPS = deathFX.GetComponent<ParticleSystem>();
        mr = GetComponentInChildren<SkinnedMeshRenderer>();
    }
    
    public virtual void Damage(int amt, DAMAGE_TYPE type) { }
    public virtual void Headshot(int dmg, DAMAGE_TYPE type) { }
    public virtual void Kill(int overkill) { }
    public virtual void Push(Vector3 dir, float force) { }

    public void EmitParticles()
    {
        chunkPS.transform.position = mr.bounds.center;
        chunkPS.transform.LookAt(player.transform.position);

        int chunks = Stats.info.timesLoaded * 3;
        chunkPS.Emit(10 + chunks);

        GameObject go = (GameObject)Instantiate(enemyGibs, transform.position, transform.rotation);
        ThrowGibs tg = go.GetComponent<ThrowGibs>();
        tg.Throw(deathDirection);
    }

    public void SetPlayer(Transform t)
    {
        player = t;
    }

    public void SetDeathDirection(Vector3 dir, float power)
    {
        deathDirection = dir;
        deathPower = power;
    }

    public int GetHealth()
    {
        return health;
    }
}
