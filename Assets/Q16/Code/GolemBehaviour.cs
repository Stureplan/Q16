using UnityEngine;
using System.Collections;

public class GolemBehaviour : EnemyBehaviour 
{
    public enum STATE
    {
        IDLE,
        WALKING,
        CHOPPING,
        SHOOTING
    }

    STATE CURRENT_STATE;
    int number = 0;

    public GameObject explosion;
	public GameObject shot;
	Transform hand;
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

        shootTimer = new Cooldown(0.5f);


        CURRENT_STATE = STATE.IDLE;
	}
	
	void Update () 
	{
        number++;

        float distance = Vector3.Distance(player.transform.position, transform.position);
        CheckState(CURRENT_STATE, distance);










		if (health <= 0)
		{
			Kill (Mathf.Abs(health));
		}

        ApplyPhysics();
	}

    void CheckState(STATE cState, float d)
    {
        switch (cState)
        {
            case STATE.IDLE:
                Idle(d);
                break;

            case STATE.WALKING:
                Walk(d);
                break;

            case STATE.CHOPPING:
                break;

            case STATE.SHOOTING:
                break;
        }
    }

    void Idle(float d)
    {
        //If we're not playing Idle anim, play it
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            anim.SetTrigger("tIdle");
        }

        //If we were close enough to the player..
        if (d < 15.0f)
        {
            //Change to walking
            GoToState(STATE.WALKING);
        }
    }

    void Walk(float d)
    {
        //Only set a new desination every X frames
        if (number % 20 == 0)
        {
            if (d < 15.0f)
            {
                agent.SetDestination(player.transform.position);
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                {
                    anim.SetTrigger("tWalk");
                }
            }
            else
            {
                agent.ResetPath();
                GoToState(STATE.IDLE);
            }
        }



    }

    public void GoToState(STATE toState)
    {
        CURRENT_STATE = toState;
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
