﻿using UnityEngine;
using System.Collections;

public class SunStaff : Weapon
{
    public Transform spinningCircle;
    public Camera cam;
    public MeshRenderer mr;
    LineRenderer lr;
    Color col = Color.white;
    float a = 0.0f;

    Animation animations;
    Rigidbody rb;
    ParticleSystem psGun;
    
    float originalNoise = 0.25f;
    float currentNoise = 0.0f;

    bool isFiring = false;

    void Start ()
    {
        animations = GetComponent<Animation>();
        rb = GetComponentInChildren<Rigidbody>();
        lr = GetComponentInChildren<LineRenderer>();
        psGun = GetComponentInChildren<ParticleSystem>();

        CD = 0.1f;
        col.a = 0.0f;
        a = 0.0f;
        lr.SetColors(col, col);

        rb.maxAngularVelocity = 20.0f;

        cooldown = new Cooldown(CD);
        index = 7;
	}

    void OnDisable()
    {
        animations.Stop();
        isFiring = false;

        col.a = 0.0f;
        lr.SetColors(col, col);
    }
	
	void Update ()
    {
        cooldown.UpdateTimer();


        if (a > 0.0f)
        {
            a -= 4.0f * Time.deltaTime;

            col.a = a;
            lr.SetColors(col, col);
        }


        if (Input.GetButtonUp("Fire"))
        {
            isFiring = false;
            //play particle effect trail
        }

        if (isFiring)
        {
            FindHarmlessVectors(transform.position, cam.transform.forward);

        }

        currentNoise = Mathf.Lerp(currentNoise, originalNoise, Time.deltaTime * 3.0f);

        mr.material.SetFloat("_NoiseAmount", currentNoise);
    }

    void FixedUpdate()
    {
        spinningCircle.localEulerAngles = new Vector3(0, 0, spinningCircle.localEulerAngles.z); 
    }

    public override void Fire(Vector3 pos, Vector3 dir)
    {
        isFiring = true;
        psGun.Emit(1);


        AddRotation(-spinningCircle.forward, 7.5f);
        currentNoise = 1.1f;

        FindVectors(pos, dir);
        a = 1.0f;

        cooldown.ResetTimer();
    }

    public override void PlayAnimation(string anim)
    {
        if (animations == null)
        {
            animations = GetComponent<Animation>();
        }

        animations.Stop();
        animations.Play(anim);
    }

    void FindVectors(Vector3 pos, Vector3 dir)
    {
        RaycastHit hit;
        RaycastHit hit2;

        if (Physics.Raycast(pos, dir, out hit))
        {
            lr.SetPosition(0, spinningCircle.position);
            lr.SetPosition(1, hit.point);

            if (hit.collider.tag == "Enemy")
            {
                EnemyBehaviour enemy = hit.transform.GetComponent<EnemyBehaviour>();
                enemy.Damage(20);
                enemy.SetDeathDirection(dir, 10.0f);
            }

            if (hit.collider.tag == "World")
            {

            }

            if (Physics.Raycast(hit.point, Vector3.Reflect(dir, hit.normal), out hit2))
            {
                lr.SetPosition(2, hit2.point);


                if (hit2.collider.tag == "Enemy")
                {
                    EnemyBehaviour enemy = hit2.transform.GetComponent<EnemyBehaviour>();
                    enemy.Damage(20);
                    enemy.SetDeathDirection((hit2.point - hit.point).normalized, 10.0f);
                }
            }
        }
    }

    void FindHarmlessVectors(Vector3 pos, Vector3 dir)
    {

        RaycastHit hit;
        RaycastHit hit2;

        if (Physics.Raycast(pos, dir, out hit))
        {
            lr.SetPosition(0, spinningCircle.position);
            lr.SetPosition(1, hit.point);

            if (Physics.Raycast(hit.point, Vector3.Reflect(dir, hit.normal), out hit2))
            {
                lr.SetPosition(2, hit2.point);
            }
        }
    }

    void AddRotation(Vector3 v, float f)
    {
        rb.AddTorque(v * f, ForceMode.Impulse);
    }
}
