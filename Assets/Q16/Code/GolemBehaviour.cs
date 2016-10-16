using UnityEngine;
using System.Collections;

public class GolemBehaviour : EnemyBehaviour 
{
    public GameObject explosion;
	public GameObject shot;
	Transform hand;
	Cooldown walkTimer;
    Cooldown chopTimer;
    Cooldown shootTimer;
	Animator anim;

    NavMeshAgent agent;
    CharacterController cc;
    Vector3 forces;


	void Start () 
	{
        SetupReferences();

        maxHealth = 100;
        health = 100;

        //               |
        //Golem specific |
        //               v
        cc = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        forces = Vector3.zero;

		hand = transform.Find ("hip/chest/r_arm/r_hand");
		anim = GetComponent<Animator>();


        float CD = 1.0f;
        walkTimer = new Cooldown(CD);
        chopTimer = new Cooldown(3.0f);
        shootTimer = new Cooldown(0.5f);
	}
	
	void Update () 
	{
        walkTimer.UpdateTimer();

		if (health <= 0)
		{
			Kill (Mathf.Abs(health));
		}

		float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance < 15.0f && walkTimer.ActionReady())
        {
            agent.SetDestination(player.transform.position);
            anim.Play("Walk");

            walkTimer.ResetTimer();
        }

        if (distance < 5.0f)
        {
            chopTimer.UpdateTimer();

            if (chopTimer.ActionReady())
            {
                //chop
                anim.CrossFade("Chop", 0.2f);
                chopTimer.ResetTimer();
            }
        }
        
        
        /*
		if (player != null && distance < 20.0f)
		{
			Vector3 vRot;
			Quaternion qRot;

			vRot = player.transform.position - transform.position;
			vRot.y = 0.0f;
			qRot = Quaternion.LookRotation(vRot, Vector3.up);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, qRot, rotateSpeed * Time.deltaTime);


			if (cd.ActionReady())
			{
				Shoot (player.transform.position);
			}
		}*/


        ApplyPhysics();
	}

    void ApplyPhysics()
    {
        if (!cc.isGrounded)
        {
            forces.y += Physics.gravity.y * 2.0f * Time.deltaTime;
        }
        else
        {
            forces.y = 0.0f;
        }

        if (forces.magnitude > 0.01f)
        {
            forces.x = Mathf.Lerp(forces.x, 0.0f, Time.deltaTime);
            forces.z = Mathf.Lerp(forces.z, 0.0f, Time.deltaTime);

            if (forces.y > 0.0f)
            {
                forces.y = Mathf.Lerp(forces.y, 0.0f, Time.deltaTime);
            }

            cc.Move(forces * Time.deltaTime);
        }

    }

	void Shoot(Vector3 pos)
	{
		//Shoot towards player

		Quaternion rot = Quaternion.LookRotation(player.transform.position - hand.position);
		Instantiate (shot, hand.position, rot);
		PlayAnimation("Shoot");

        shootTimer.ResetTimer();
	}

	public void PlayAnimation(string name)
	{
        anim.Stop();
        anim.Play(name);
	}

    public override void Damage(int amt)
    {
        health -= amt;
        //TODO: Stagger anim
    }

    public override void Push(Vector3 dir, float force)
    {
        dir.Normalize();

        dir.y = 0.0f;
        forces = dir * force / 1.0f;
    }

    public override void Kill(int overkill)
    {
        if (overkill >= maxHealth) //If the enemy was dealt 100% of max health under 0
        {
            EmitParticles();
        }
        else
        {
            EmitParticles();
            //TODO: Play death animation
            //TODO: Delay destroy gameobject
        }

        Stats.info.enemiesKilled++;
        Destroy(this.gameObject);
    }
}
