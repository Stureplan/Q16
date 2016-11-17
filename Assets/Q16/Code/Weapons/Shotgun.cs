using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Shotgun : Weapon
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

    // VARIABLES
    int amountOfPellets = 10;
    float spread = 0.05f;

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
        CD = 0.5f;
        power = 10.0f;

        cooldown = new Cooldown(CD);
        index = 0;
    }

    void Update ()
    {
        cooldown.UpdateTimer();
	}

    void OnDisable()
    {
        fireLightAnim.Stop();
        animations.Stop();
    }

    public override void Fire(Vector3 pos, Vector3 dir)
    {
        animations.Stop();
        animations.Play("ShotgunFire");

        psGun.Emit(20);

        fireLightAnim.Stop();
        fireLightAnim.Play();

        
        psShell.Emit(1);

        for (int i = 0; i < amountOfPellets; i++)
        {
            int pelletsHit = 0;
            RaycastHit hit;

            float weight = (float)i / (float)amountOfPellets;
            weight = weight / 10;

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

                    EnemyBehaviour enemy = hit.collider.GetComponent<EnemyBehaviour>();
                    enemy.Damage(5, DAMAGE_TYPE.BUCKSHOT);
                    enemy.SetDeathDirection(dir, power);


                    pelletsHit++;
                }
                else if (hit.collider.tag == "CultistWeapon")
                {
                    CultistFireFX fx = hit.collider.transform.GetComponentInParent<CultistFireFX>();
                    fx.StartFadingShader();
                    fx.SpawnImpactPS(hit.transform.position);
                }

                else if (hit.collider.tag == "World" || hit.collider.tag == "WorldProp")
                {
                    psWorld.Emit(2);

                }

            }

            if (pelletsHit > 0)
            {
                //We hit something
                //stats.info.hit++;
            }
        }


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
