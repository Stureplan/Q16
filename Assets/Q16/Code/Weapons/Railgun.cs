using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Railgun : Weapon
{
    public GameObject lineObject;
    public GameObject psObject;
    public GameObject pixelateObject;
    public Camera cam;
    Animation animations;
    ParticleSystem psWorld;
    ParticleSystem psGun;
    LineRenderer lr;
    MeshRenderer mr;
    Text textCD;


    Color b;
	Color g;
	float alpha = 255.0f;


    


	void Start () 
	{
		b = new Color(0, 0.5f, 1.0f, 1.0f);
		g = new Color(0, 0.8f, 1.0f, 1.0f);

        animations = GetComponent<Animation>();
		psGun = psObject.GetComponent<ParticleSystem>();
        textCD = GameObject.Find("RailgunCooldown").GetComponent<Text>();

        lr = lineObject.GetComponent<LineRenderer>();
        mr = GetComponent<MeshRenderer>();
        psWorld = GameObject.Find("RailgunHit").GetComponent<ParticleSystem>();


        // INHERITED VARIABLES
        CD = 1.0f;

        float newCD = Stats.info.timesLoaded / 10.0f;
        CD = CD - newCD;
        if (CD < 0.1f) { CD = 0.1f; }

        cooldown = new Cooldown(CD);

        textCD.text = "RLGN CD: " + CD.ToString();
    }

    void OnDisable()
    {
        animations.Stop();
        SetZoom(false);


        b.a = 0.0f;
        g.a = 0.0f;
        lr.SetColors(b, g);
        lr.material.color = b;
    }
	
	void Update () 
	{
        cooldown.UpdateTimer();


		if (alpha > 0.0f)
		{
			alpha -= 3.0f * Time.deltaTime;
			
			b.a = alpha;
			g.a = alpha;
			
			lr.SetColors (b, g);

			lr.material.color = b;
		}

        
        if (Input.GetMouseButtonDown(1))
        {
            SetZoom(true);
        }
        if (Input.GetMouseButtonUp(1))
        {
            SetZoom(false);
        }
	}

	public override void Fire(Vector3 pos, Vector3 dir)
	{
        animations.Stop();
        animations.Play("RailgunFire");
		psGun.Emit (20);

		RaycastHit hit;
		if (Physics.Raycast (pos, dir, out hit))
		{
			lr.SetPosition(0, pos);
			lr.SetPosition(1, hit.point);


			psWorld.transform.position = hit.point;
			psWorld.transform.rotation = Quaternion.LookRotation(dir);

            IDamageable entity;
            if (hit.collider.gameObject.IsDamageable(out entity))
            {
                psWorld.Emit(75);

                entity.Damage(125, DAMAGE_TYPE.PLASMA, SenderInfo.Player());

                if (entity.Health() <= 0) { entity.DeathDirection(dir, 15.0f); }
            }
            else if (hit.collider.tag == "CultistWeapon")
            {
                CultistFireFX fx = hit.collider.transform.GetComponentInParent<CultistFireFX>();
                fx.StartFadingShader();
                fx.SpawnImpactPS(hit.transform.position);
            }
            else if (hit.collider.tag == "EnemyHead")
            {
                psWorld.Emit(75);
                EnemyBehaviour enemy = hit.collider.transform.GetComponentInParent<CultistBehaviour>();
                enemy.Headshot(150, DAMAGE_TYPE.PLASMA, SenderInfo.Player());
            }
            else
            {
                lr.SetPosition(0, pos);
                lr.SetPosition(1, pos + (dir * 100.0f));
            }
        }






		alpha = 1.0f;
        SetZoom(false);

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

    void SetZoom(bool state)
    {
        if (state)
        {
            cam.fieldOfView = 20;
            pixelateObject.SetActive(true);
            mr.enabled = false;
        }
        else
        {
            cam.fieldOfView = 70;
            pixelateObject.SetActive(false);
            mr.enabled = true;
        }
    }
}
