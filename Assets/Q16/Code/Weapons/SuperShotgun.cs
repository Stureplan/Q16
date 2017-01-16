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
    LayerMask layerMask;

    void Start ()
    {
        animations = GetComponent<Animation>();
        psGun = psObject.GetComponent<ParticleSystem>();
        psWorld  = GameObject.Find("ShotgunHit").GetComponent<ParticleSystem>();
        psImpact = GameObject.Find("ShotgunImpact").GetComponent<ParticleSystem>();
        psShell = shellEject.GetComponent<ParticleSystem>();


        fireLight = GetComponentInChildren<Light>();
        fireLightAnim = fireLight.gameObject.GetComponent<Animation>();

        layerMask = 1 << LayerMask.GetMask("Interactable");

        // INHERITED VALUES
        CD = 1.5f;
        power = 30.0f;

        float newCD = Stats.info.timesLoaded / 20.0f;
        CD = CD - newCD;
        if (CD < 0.25f) { CD = 0.25f; }

        cooldown = new Cooldown(CD, false);
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





                IDamageable entity;
                if (hit.collider.gameObject.IsDamageable(out entity))
                {
                    if (entity.Type() == SENDER_TYPE.ENEMY)
                    {
                        psImpact.transform.position = hit.point;
                        psImpact.transform.rotation = Quaternion.LookRotation(dir);
                        psImpact.Emit(2);
                    }
                    else
                    {
                        psWorld.Emit(5);
                    }


                    entity.Damage(15, DAMAGE_TYPE.BUCKSHOT, SenderInfo.Player());
                    if (entity.Health() <= 0)
                    {
                        entity.DeathDirection(dir, power);
                    }

                    pelletsHit++;
                }
                else if (hit.collider.tag == "CultistWeapon")
                {
                    CultistFireFX fx = hit.collider.transform.GetComponentInParent<CultistFireFX>();
                    fx.StartFadingShader();
                    fx.SpawnImpactPS(hit.transform.position);
                }
                else if (hit.collider.tag == "Water")
                {
                    hit.collider.GetComponent<WaterBehaviour>().Splash(hit.point, SPLASH_TYPE.SMALL);
                }
                else if (hit.collider.tag == "EnemyHead")
                {
                    EnemyBehaviour eb = hit.collider.gameObject.GetComponentInParent<EnemyBehaviour>();
                    eb.Damage(20, DAMAGE_TYPE.BUCKSHOT, SenderInfo.Player());
                }
                else
                {
                    psWorld.Emit(5);
                }
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
