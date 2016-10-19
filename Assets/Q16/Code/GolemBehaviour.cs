using UnityEngine;
using System.Collections;

public class GolemBehaviour : EnemyBehaviour 
{
    public enum STATE
    {
        IDLE,
        WALKING,
        CHOPPING,
        SHOOTING,
        STAGGERING
    }

    STATE CURRENT_STATE;
    int number = 0;
    int cTimesShot = 0;

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

		hand = transform.Find ("hips/neck/arm_r/elbow_r/hand_r");
		anim = GetComponent<Animator>();

        shootTimer = new Cooldown(Random.Range(4.0f, 6.0f));


        CURRENT_STATE = STATE.IDLE;
	}
	
	void Update () 
	{
        shootTimer.UpdateTimer();
        number++;

        float distance  = Vector3.Distance(player.transform.position, transform.position);
        float dot       = Vector3.Dot(transform.forward, (player.transform.position - transform.position).normalized);

        CheckState(CURRENT_STATE, distance, dot);










		if (health <= 0)
		{
			Kill (Mathf.Abs(health));
		}

        if (cc.enabled == true)
        {
            ApplyPhysics();
        }
    }

    void CheckState(STATE cState, float dist, float dot)
    {
        switch (cState)
        {
            case STATE.IDLE:
                Idle(dist, dot);
                break;

            case STATE.WALKING:
                Walk(dist, dot);
                break;

            case STATE.CHOPPING:
                Chop();
                break;

            case STATE.SHOOTING:
                Shoot();
                break;

            case STATE.STAGGERING:
                Stagger();
                break;
        }
    }

    void Idle(float dist, float dot)
    {
        //If we're not playing Idle anim, play it
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            anim.SetTrigger("tIdle");
        }

        //If we were close enough to the player
        //and angle is close enough
        if (dist < 20.0f)
        {
            //Change to walking
            GoToState(STATE.WALKING);
        }
    }

    void Walk(float dist, float dot)
    {
        //Only set a new desination every X frames
        //TODO: Change this to a timer instead, way safer for slow computers
        if (number % 20 == 0)
        {
            if (dist < 3.0f && dot > 0.5f)
            {
                //Chop
                GoToState(STATE.CHOPPING);
                return;
            }


            //If we're 20 units away
            if (dist < 20.0f)
            {
                //Decide if we're gonna shoot or approach
                int decision = Random.Range(0, 4);
                if (decision < 3 && dot > 0.75f && shootTimer.ActionReady())
                {
                    //50% chance
                    GoToState(STATE.SHOOTING);
                    return;
                }
                else
                {
                    agent.SetDestination(player.transform.position);

                    if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                    {
                        anim.SetTrigger("tWalk");
                    }

                    return;
                }
            }
            else
            {
                //If we weren't within detection range, go back to Idle
                //and clear the path we were previously taking

                //TODO: Move ResetPath to Idle()
                agent.ResetPath();
                GoToState(STATE.IDLE);
            }
        }
    }

    void Chop()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Chop"))
        {
            anim.SetTrigger("tChop");
        }

        agent.ResetPath();
        agent.velocity = Vector3.zero;
    }

    void Shoot()
    {
        if (shootTimer.ActionReady())
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
            {
                anim.SetTrigger("tShoot");
            }

            agent.ResetPath();
            agent.velocity = Vector3.zero;

            shootTimer.ResetTimer();
        }
    }

    public void ActuallyShoot()
    {
        //Shoot towards player
        Quaternion rot = Quaternion.LookRotation(player.transform.position - hand.position);
        Instantiate(shot, hand.position, rot);
        cTimesShot++;
    }

    void Stagger()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Stagger"))
        {
            anim.SetTrigger("tStagger");
        }

        agent.ResetPath();
        agent.velocity = Vector3.zero;
    }

    public void GoToState(STATE toState)
    {
        CURRENT_STATE = toState;
    }

    void Ragdoll()
    {
        Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();
        Collider col;

        for (int i = 0; i < rbs.Length; i++)
        {
            rbs[i].isKinematic = false;
            col = rbs[i].GetComponent<Collider>();
            col.enabled = true;

            rbs[i].AddForce(deathDirection * deathPower, ForceMode.Impulse);
            rbs[i].gameObject.layer = 14;


            if (rbs[i].name == "Axe")
            {
                rbs[i].gameObject.AddComponent<DestroyInSeconds>().SetTime(10.0f);
                rbs[i].transform.parent = null;
            }
        }
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

	public void PlayAnimation(string name)
	{
        anim.Stop();
        anim.Play(name);
	}

    public override void Damage(int amt)
    {
        health -= amt;
        //TODO: Find player OR "target" (sender, should be a parameter)
        GoToState(STATE.STAGGERING);
    }

    public override void Push(Vector3 dir, float force)
    {
        cc.enabled = true;

        dir.Normalize();

        dir.y = 0.0f;
        forces = dir * force / 1.0f;
    }

    public override void Kill(int overkill)
    {
        if (overkill >= maxHealth) //If the enemy was dealt 100% of max health under 0
        {
            EmitParticles();
            Destroy(this.gameObject);

        }
        else
        {
            //EmitParticles();
            gameObject.AddComponent<DestroyInSeconds>().SetTime(10.0f);
            Collider col = GetComponent<Collider>();
            col.enabled = false;

            Ragdoll();
            Destroy(anim);
            Destroy(agent);
            Destroy(cc);
            Destroy(col);
            Destroy(this);
        }

        Stats.info.enemiesKilled++;
    }
}
