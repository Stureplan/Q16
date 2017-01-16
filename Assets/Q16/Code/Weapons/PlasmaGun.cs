using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlasmaGun : Weapon
{
    public GameObject[] LEDlights;

    public GameObject ball;
    public GameObject eject;
    public GameObject smokepsObject;
    public GameObject textObject;

    Animation animations;

    public GameObject fireLightObject;
    Animation fireLightAnim;

    public GameObject psObject;
    ParticleSystem ps;
    ParticleSystem smokePS;
    TextMesh ammo;
    
    int maxAmmo = 30;
    int ammoCounter;

	void Start ()
    {
        animations = GetComponent<Animation>();
        ps = psObject.GetComponent<ParticleSystem>();
        smokePS = smokepsObject.GetComponent<ParticleSystem>();
        ammo = textObject.GetComponent<TextMesh>();
        fireLightAnim = fireLightObject.GetComponent<Animation>();


        smokePS.Stop();
        ammoCounter = maxAmmo;
        ammo.text = ammoCounter.ToString();


        for (int i = 0; i < LEDlights.Length; i++)
        {
            MeshRenderer mr = LEDlights[i].GetComponent<MeshRenderer>();
            mr.material.color = Color.cyan;
        }


        CD = 0.15f;
        power = 5.0f;

        float newCD = Stats.info.timesLoaded / 100.0f;
        CD = CD - newCD;
        if (CD < 0.05f) { CD = 0.05f; }

        cooldown = new Cooldown(CD);
    }

    void OnDisable()
    {
        fireLightAnim.Stop();
        animations.Stop();
    }
    
    void Update ()
    {
        cooldown.UpdateTimer();

        ammo.text = ammoCounter.ToString();


        if (Input.GetButtonDown("Reload") && ammoCounter != maxAmmo && !animations.IsPlaying("PlasmaGunReload"))
        {
            Reload();
        }

        if (ammoCounter < 1 && !animations.IsPlaying("PlasmaGunReload"))
        {
            Reload();
        }
    }


    public override void Fire(Vector3 pos, Vector3 dir)
    {
        for (int i = 0; i < LEDlights.Length; i++)
        {
            MeshRenderer mr = LEDlights[i].GetComponent<MeshRenderer>();

            int check = maxAmmo - ammoCounter;
            if ((i + 1) * 5 <= check)
            {
                mr.material.color = Color.black;
            }
            else
            {
                mr.material.color = Color.cyan;
            }
        }


        if (ammoCounter > 0)
        {
            smokePS.Stop();

            animations.Stop();
            animations.Play("PlasmaGunFire");
            
            fireLightAnim.Stop();
            fireLightAnim.Play();

            ps.Emit(1);


            Quaternion rot = Quaternion.LookRotation(dir);
            Instantiate(ball, eject.transform.position, rot);


            cooldown.ResetTimer();
            ammoCounter--;
        }
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

    void Reload()
    {
        animations.Stop();
        animations.Play("PlasmaGunReload");
        smokePS.Play();

        ammoCounter = 0;

        for (int i = 0; i < LEDlights.Length; i++)
        {
            MeshRenderer mr = LEDlights[i].GetComponent<MeshRenderer>();
            mr.material.color = Color.black;
        }
    }

    public void RefillGun()
    {
        ammoCounter = maxAmmo;
        smokePS.Stop();

        for (int i = 0; i < LEDlights.Length; i++)
        {
            MeshRenderer mr = LEDlights[i].GetComponent<MeshRenderer>();
            mr.material.color = Color.cyan;
        }
    }
}
