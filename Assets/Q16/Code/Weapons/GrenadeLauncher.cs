using UnityEngine;
using System.Collections;

public class GrenadeLauncher : Weapon
{
    public GameObject grenade;
    public ParticleSystem ps;

    Animation animations;


	void Start ()
    {
        //Switched to GL
        animations = GetComponent<Animation>();
        
        CD = 0.75f;
        power = 30.0f;

        float newCD = Stats.info.timesLoaded / 10.0f;
        CD = CD - newCD;
        if (CD < 0.15f) { CD = 0.15f; }

        cooldown = new Cooldown(CD, false);

        //animations.Stop();
    }

    void OnDisable()
    {
        animations.Stop();
    }

    void Update ()
    {
        cooldown.UpdateTimer();
	}

    public override void Fire(Vector3 pos, Vector3 dir)
    {
        animations.Stop();
        animations.Play("GrenadeFire");

        Quaternion rot = Quaternion.LookRotation(dir);
        Instantiate(grenade, pos, rot);

        ps.Emit(20);

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
