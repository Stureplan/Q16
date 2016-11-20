using UnityEngine;
using System.Collections;

public class CultistBehaviour : EnemyBehaviour
{
    public override void Damage(int dmg, DAMAGE_TYPE type, SenderInfo sender)
    {
        health -= dmg;

        if (CurrentState != STATE.ONFIRE && CurrentState != STATE.DYING)
        {
            SetCanMove(false);
            SetWeaponHitbox(false);
            anim.SetTrigger("stagger");
        }
    }

    public override void Explosion(int dmg, DAMAGE_TYPE type, SenderInfo sender, Vector3 point, float force)
    {
        Damage(dmg, type, sender);
    }

    enum STATE
    {
        IDLE,
        WALKING,
        ATTACKING,
        CASTING,
        DYING,
        HEADSHOT,
        ONFIRE
    }

    STATE CurrentState;
    Animator anim;
    CharacterController cc;
    Transform currentTarget;
    bool playerFound = false;
    bool inAttackRange = false;
    bool inSpellRange = false;
    bool canMove = true;
    float attackRange = 1.8f;
    float spellRange = 7.5f;
    Cooldown spellRate;

    public GameObject weaponObject; //TODO: enable/disable GO on certain frames for fire col
    public GameObject ragdoll;
    public GameObject headshotPS;
    public GameObject headshotSplode;
    public GameObject headGO;
    public GameObject fireFX;
    public GameObject fireballPrefab;


    // PANIC VARIABLES
    Vector3 panicSpot;
    float panicSpeed = 3.0f;


    Vector3 forces;

	void Start ()
    {
        SetupReferences();

        forces = Vector3.zero;


        rotateSpeed = 250.0f;
        moveSpeed = 1.2f;
        moveSpeed += (float)Stats.info.timesLoaded / 5.0f;

        maxHealth = 100;
        health = 100;
        fireRate = new Cooldown(2.0f);
        spellRate = new Cooldown(7.0f);

        anim = GetComponentInChildren<Animator>();
        cc = GetComponent<CharacterController>();

        CurrentState = STATE.IDLE;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //If he collides with a wall or anything,
        //calculate new panic point
        if (CurrentState == STATE.ONFIRE)
        {
            if (Vector3.Dot(hit.normal, transform.forward) < -0.25f)
            {
                panicSpot = GenerateSpot();
            }
        }
    }

    void Update ()
    {
        fireRate.UpdateTimer();
        spellRate.UpdateTimer();

        if (health <= 0 && CurrentState != STATE.DYING)
        {
            Kill(Mathf.Abs(health));
        }

        if (CurrentState == STATE.DYING)
        {
            return;
        }

        ApplyPhysics();






        if (CurrentState != STATE.ONFIRE &&
            CurrentState != STATE.DYING &&
            CurrentState != STATE.HEADSHOT)
        {
            if (CurrentState != STATE.CASTING)
            {
                FindPlayer();
            }

            if (playerFound && canMove)
            {
                Walk();
            }
            else if (!playerFound && canMove)
            {
                Idle();
            }

            if (inAttackRange && fireRate.ActionReady())
            {
                Attack();
            }

            if (playerFound && inSpellRange && spellRate.ActionReady()
                && CurrentState != STATE.ATTACKING
                && CurrentState != STATE.CASTING)
            {
                CastSpell();
            }

            if (CurrentState == STATE.CASTING)
            {
                RotateTowards(currentTarget.position);
            }
        }
        else if (CurrentState == STATE.ONFIRE)
        {
            PanicTowards();
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

    void FindPlayer()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (player != null && distance < 25.0f)
        {
            playerFound = true;
            currentTarget = player.transform;
        }
        else
        {
            playerFound = false;
        }
    }

    void Walk()
    {
        anim.SetTrigger("walking");
        RotateTowards(currentTarget.position);
        MoveTowards(currentTarget);

        CurrentState = STATE.WALKING;
    }

    void Idle()
    {
        anim.SetTrigger("idle");

        CurrentState = STATE.IDLE;
    }

    void Attack()
    {
        SetCanMove(false);

        int choice = Random.Range(0, 2);
        if (choice == 0)
        {
             anim.SetTrigger("attack1");
        }
        if (choice == 1)
        {
            anim.SetTrigger("attack2");
        }

        CurrentState = STATE.ATTACKING;
        fireRate = new Cooldown(Random.Range(2.0f, 4.0f));
    }

    void CastSpell()
    {
        int chance = Random.Range(0, 5);
        if (chance >= 0 && chance <= 2)
        {
            SetCanMove(false);
            anim.SetTrigger("spell");
            CurrentState = STATE.CASTING;
            currentTarget = player.transform;
        }
        spellRate = new Cooldown(Random.Range(6.0f, 10.0f));
    }


    public void SetCanMove(bool active)
    {
        canMove = active;
    }
    
    public void SetWeaponHitbox(bool active)
    {
        weaponObject.SetActive(active);
    }

    public Vector3 GetWeaponPos()
    {
        Vector3 colPos = weaponObject.transform.position + new Vector3(0, 0.75f, 0);
        return colPos;
    }

    public void SpawnWeaponFireFX()
    {
        GameObject go = (GameObject)Instantiate(fireFX, Vector3.zero, Quaternion.identity);
        go.transform.SetParent(weaponObject.transform, false);
        go.transform.localPosition = new Vector3(0, 0.6f, 0);
    }

    public void SpawnWeaponFireball()
    {
        Vector3 forwardPos = weaponObject.transform.position;
        forwardPos += transform.forward;

        Quaternion rot = Quaternion.LookRotation(player.transform.position - forwardPos);
        
        Utility.InstantiateProjectile(fireballPrefab, forwardPos, rot, this.Sender(SENDER_TYPE.ENEMY));
    }

    void RotateTowards(Vector3 t)
    {
        Vector3 vRot;
        Quaternion qRot;

        vRot = t - transform.position;
        vRot.y = 0.0f;

        qRot = Quaternion.LookRotation(vRot, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, qRot, rotateSpeed * Time.deltaTime);

    }

    void MoveTowards(Transform t)
    {
        Vector3 offset = t.position - transform.position;
        offset.y = 0.0f;

        if (offset.magnitude > attackRange)
        {
            offset = offset.normalized;
            cc.Move(offset * moveSpeed * Time.deltaTime);
            inAttackRange = false;
        }
        else
        {
            inAttackRange = true;
        }

        // Check for spell range
        if (offset.magnitude > spellRange)
        {
            inSpellRange = false;
        }
        {
            inSpellRange = true;
        }
    }

    IEnumerator WaitForAnimation(float animLength, bool active)
    {
        while(anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.98f)
        {
            yield return null;
        }
        
        //yield return new WaitForSeconds(animLength);

        SetCanMove(active);
    }

    public override void Headshot(int dmg, DAMAGE_TYPE type, SenderInfo sender)
    {
        Damage(dmg, type, sender);

        if (health < 1)
        {
            //TODO: instantiate PS
            ResetTriggers("headshot");
            anim.SetTrigger("headshot");
            CurrentState = STATE.HEADSHOT;

            Instantiate(headshotSplode, headGO.transform.position, Quaternion.Euler(-90.0f, 0.0f, 0.0f));
            GameObject go = (GameObject)Instantiate(headshotPS, Vector3.zero, Quaternion.identity);
            go.transform.SetParent(headGO.transform, false);
        }
    }

    public override void Push(Vector3 dir, float force)
    {
        dir.Normalize();
        
        dir.y = 0.0f;
        forces = dir * force / 1.0f;
    }

    public override void Kill(int overkill)
    {
        if (CurrentState == STATE.HEADSHOT)
        {
            //handle headshot
            CurrentState = STATE.DYING;
            gameObject.AddComponent<DestroyInSeconds>().SetTime(10.0f);
            Collider col = cc.GetComponent<Collider>();
            col.enabled = false;

            Stats.info.enemiesKilled++;
            return;
        }

        if (CurrentState == STATE.ONFIRE)
        {
            SetDeathDirection(transform.forward, 7.5f);
        }


        if (overkill >= maxHealth /* / 2 */) //If the enemy was dealt 100% of max health or more beyond kill
        {
            EmitParticles();

        }
        else
        {
            int choice = Random.Range(0, 2);
            if (choice == 0)
            {
                CurrentState = STATE.DYING;
                ResetTriggers("death");
                anim.SetTrigger("death");
                gameObject.AddComponent<DestroyInSeconds>().SetTime(10.0f);
                Collider col = cc.GetComponent<Collider>();
                col.enabled = false;

                Stats.info.enemiesKilled++;
                return;
            }
            else if (choice == 1)
            {
                //ragdoll
                InstantiateAndRotate();
            }

        }

        Stats.info.enemiesKilled++;
        Destroy(this.gameObject);
    }

    void InstantiateAndRotate()
    {
        GameObject go = (GameObject)Instantiate(ragdoll, transform.position, transform.rotation);

        Transform original = transform.Find("Cultist/Character1_Hips");
        Transform destination = go.transform.Find("Character1_Hips");

        Transform[] o_transforms = original.GetComponentsInChildren<Transform>();
        Transform[] d_transforms = destination.GetComponentsInChildren<Transform>();

        for (int i = 0; i < o_transforms.Length; i++)
        {
            if ((o_transforms[i].name != "WeaponCollider") && 
                (o_transforms[i].name != "chestPS") && 
                (o_transforms[i].name != "fireFX"))
            {
                Transform o = o_transforms[i];
                Transform d = d_transforms[i];


                d.localPosition = o.localPosition;
                d.localRotation = o.localRotation;

                Rigidbody rb = d.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(deathDirection * deathPower, ForceMode.Impulse);
                }
            }
        }

        if (CurrentState == STATE.ONFIRE)
        {
            SkinnedMeshRenderer smr = go.GetComponentInChildren<SkinnedMeshRenderer>();
            smr.material.SetFloat("_FireSlider", 1.0f);
        }
    }
    
    public void SetOnFire()
    {
        if (CurrentState != STATE.ONFIRE)
        {
            gameObject.AddComponent<OnFire>();
        }

        SetWeaponHitbox(false);
        anim.SetTrigger("onfire");
        CurrentState = STATE.ONFIRE;
        rotateSpeed = 500.0f;

        panicSpot = GenerateSpot();
    }

    void PanicTowards()
    {
        Vector3 offset = panicSpot - transform.position;
        if (offset.magnitude < 1.5f)
        {
            panicSpot = GenerateSpot();
        }
        else
        {
            offset = offset.normalized;
            RotateTowards(panicSpot);
            cc.Move(offset * panicSpeed * Time.deltaTime);
        }
    }

    Vector3 GenerateSpot()
    {
        RaycastHit hit;

        Vector3 randomDir = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f));
        Physics.Raycast(transform.position, randomDir, out hit);

        return hit.point;
    }

    void ResetTriggers(string ignoreTrigger)
    {
        for (int i = 0; i < anim.parameterCount; i++)
        {
            AnimatorControllerParameter p = anim.GetParameter(i);
            if (p.name != ignoreTrigger)
            {
                anim.ResetTrigger(p.name);
            }
        }
    }
}
