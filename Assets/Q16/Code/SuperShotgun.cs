using UnityEngine;
using System.Collections;

public class SuperShotgun : Weapon
{
    public GameObject psObject;
    public GameObject shellEject;

    Animation animations;
    ParticleSystem psWorld;
    ParticleSystem psImpact;
    ParticleSystem psGun;
    ParticleSystem psShell;


    Light fireLight;
    Animation fireLightAnim;


    int amountOfPellets = 30;
    float spread = 0.1f;

	void Start ()
    {
        animations = GetComponent<Animation>();
        psGun = psObject.GetComponent<ParticleSystem>();
        psWorld  = GameObject.Find("ShotgunHit").GetComponent<ParticleSystem>();
        psImpact = GameObject.Find("ShotgunImpact").GetComponent<ParticleSystem>();
        psShell = shellEject.GetComponent<ParticleSystem>();


        fireLight = GetComponentInChildren<Light>();
        fireLightAnim = fireLight.gameObject.GetComponent<Animation>();


        // INHERITED VALUES
        CD = 1.5f;
        power = 30.0f;

        float newCD = Stats.info.timesLoaded / 20.0f;
        CD = CD - newCD;
        if (CD < 0.25f) { CD = 0.25f; }

        cooldown = new Cooldown(CD, false);
        index = 1;
	}

    void OnDisable()
    {
        fireLightAnim.Stop();
        animations.Stop();
    }

    void Update ()
    {
        cooldown.UpdateTimer();
	}

    public override void Fire(Vector3 pos, Vector3 dir)
    {
        EnemyBehaviour enemy = null;

        animations.Stop();
        animations.Play("SuperShotgunFire");

        fireLightAnim.Stop();
        fireLightAnim.Play();


        psGun.Emit(50);
        psShell.Emit(2);

        //TODO: Make spread better and more realistic in both SSG and SG
        for (int i = 0; i < amountOfPellets; i++)
        {
            int pelletsHit = 0;
            RaycastHit hit;

            float weight = (float)i / (float)amountOfPellets;
            weight = weight / 5;

            Vector3 offset = Random.insideUnitSphere * weight;

            dir += offset;


            if (Physics.Raycast(pos, dir, out hit))
            {
                psWorld.transform.position = hit.point;

                if (hit.collider.tag == "Enemy")
                {
                    psImpact.transform.position = hit.point;
                    psImpact.transform.rotation = Quaternion.LookRotation(dir);

                    psImpact.Emit(2);

                    enemy = hit.collider.GetComponent<EnemyBehaviour>();
                    enemy.Damage(15);

                    // PUSH RAGDOLL
                    if (enemy.GetHealth() < 0)
                    {
                        enemy.SetDeathDirection(dir, power);
                    }

                    pelletsHit++;
                }

                else if (hit.collider.tag == "CultistWeapon")
                {
                    CultistFireFX fx = hit.collider.transform.GetComponentInParent<CultistFireFX>();
                    fx.StartFadingShader();
                    fx.SpawnImpactPS(hit.transform.position);
                }

                else if (hit.collider.tag == "World")
                {

                    psWorld.Emit(5);
                }



            }

            if (pelletsHit > 0)
            {
                //We hit something
                //stats.info.hit++;
            }
        }



        // PUSH PLAYER
        Transform player = transform.root;
        PlayerInput input = player.GetComponent<PlayerInput>();
        Vector3 direction;
        direction = -transform.forward * 1.0f;
        direction = direction + transform.up * 0.1f;

        input.AddAirborneForce(direction, 25.0f);

        // RESET CD
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
}
