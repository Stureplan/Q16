using UnityEngine;
using System.Collections;

public class Sword : Weapon
{
    Animation animations;
    public Collider swordCollider;

    int bloodAmount = 0;
    public MeshRenderer mr;
    public Texture[] bloodTexs;
    public ParticleSystem ps;
    ParticleSystem.EmissionModule em;
    ParticleSystem.MinMaxCurve cu;

	void Start ()
    {
        animations = GetComponent<Animation>();
        ps.enableEmission = false;

        CD = 0.7f;
        power = 50.0f;

        float newCD = Stats.info.timesLoaded / 10.0f;
        CD = CD - newCD;
        if (CD < 0.2f) { CD = 0.1f; }

        cooldown = new Cooldown(CD);
        index = 7;
	}

    void OnDisable()
    {
        animations.Stop();
    }
	
	void Update ()
    {
        cooldown.UpdateTimer();
	}

    public void DisableCollider()
    {
        swordCollider.enabled = false;
    }

    public void DamageTarget(Collider other)
    {
        if (other.tag == "Enemy")
        {
            EnemyBehaviour enemy = other.gameObject.GetComponent<EnemyBehaviour>();
            enemy.Damage(50);

            // PUSH RAGDOLL
            if (enemy.GetHealth() < 1)
            {
                if (animations.IsPlaying("SwordFire1"))
                {
                    enemy.SetDeathDirection(-transform.right, 7.5f);
                }
                if (animations.IsPlaying("SwordFire2"))
                {
                    enemy.SetDeathDirection(transform.right, 7.5f);
                }
            }

            if (bloodAmount < 3)
            {
                mr.material.SetTexture("_MainTex", bloodTexs[bloodAmount]);
                bloodAmount++;

                //+= 10;
            }
            else
            {
                ps.enableEmission = true;
            }
        }
    }
    
    public override void Fire(Vector3 pos, Vector3 dir)
    {
        animations.Stop();

        int rng = Random.Range(1, 3);

        if (rng == 1)
        {
            animations.Play("SwordFire1");
        }
        else if (rng == 2)
        {
            animations.Play("SwordFire2");
        }

        swordCollider.enabled = true;
        cooldown.ResetTimer();
    }

    public override void PlayAnimation(string anim)
    {
        if (animations == null)
        {
            animations = GetComponent<Animation>();
        }

        animations.Stop();
        if (anim == "WeaponSwitch")
        {
            animations.Play("SwordSwitch");
        }
        else
        {
            animations.Play(anim);
        }
    }
}
