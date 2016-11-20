using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RocketLauncher : Weapon 
{
	public GameObject rocket;
	Animation animations;

    public GameObject psObject;
	ParticleSystem ps;
    Text textCD;



    void Start () 
	{
		//Switched to RL
		animations = GetComponent<Animation>();
		ps = psObject.GetComponent<ParticleSystem>();


        // INHERITED VARIABLES
        CD = 0.5f;
        power = 35.0f;

        float newCD = Stats.info.timesLoaded / 10.0f;
        CD = CD - newCD;
        if (CD < 0.1f) { CD = 0.1f; }


        cooldown = new Cooldown(CD);
        index = 3;

        textCD = GameObject.Find("RocketCooldown").GetComponent<Text>();
        textCD.text = "RCKT CD: " + CD.ToString();
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
		animations.Play("RocketFire");
        
        Quaternion rot = Quaternion.LookRotation(dir);
        Instantiate(rocket, pos, rot);

		ps.Emit (20);

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
