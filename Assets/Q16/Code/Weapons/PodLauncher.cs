using UnityEngine;
using System.Collections;

public class PodLauncher : Weapon
{
    public GameObject rocketPod;
    Animation animations;

    public GameObject psObject;
    ParticleSystem ps;

    public Transform podReleasePoint;
    int amountOfShotPods = 0;

    public Transform[] pods;

    bool gunIsEmpty = false;
    bool gunIsPrepared = false;
    float timeBetweenPods = 0.10f;

	void Start ()
    {
        //Switched to PL
        animations = GetComponent<Animation>();
        ps = psObject.GetComponent<ParticleSystem>();

        //INHERITED VALUES
        CD = 0.2f;
        power = 15.0f;

        cooldown = new Cooldown(CD, false);
	}

    void OnDisable()
    {
        //TODO: REMEMBER TO RESTORE THE PODS IF RELOADED
        FixPods();
        animations.Stop();
    }
	
	void Update ()
    {
        cooldown.UpdateTimer();

        if (Input.GetButtonDown("Reload") && gunIsEmpty && !animations.IsPlaying("PodLauncherReload"))
        {
            Reload();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (gunIsPrepared)
            {
                animations.Stop();
                animations.Play("PodLauncherFire");

                //Released the button and fires
                Release();

                gunIsPrepared = false;
            }
            else
            {
                //Released the button beforehand
                animations.Stop();
                animations.CrossFade("PodLauncherReset", 0.2f);
            }
        }
	}

    public override void Fire(Vector3 pos, Vector3 dir)
    {
        if (!gunIsEmpty)
        {
            if (!animations.IsPlaying("PodLauncherPrepare"))
            {

                animations.Stop();
                animations["PodLauncherPrepare"].speed = 1.0f;
                animations.Play("PodLauncherPrepare");



            }
            cooldown.ResetTimer();
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

    IEnumerator WaitAndRelease(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        
        //wait completed
        Release();
    }

    IEnumerator WaitAndFixPods(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        //wait completed

        FixPods();
        gunIsEmpty = false;
    }

    void Release()
    {
        Instantiate(rocketPod, podReleasePoint.position, podReleasePoint.rotation);

        ps.Emit(10);

        //We shot a pod
        amountOfShotPods++;

        if (amountOfShotPods < 6)
        {
            StartCoroutine(WaitAndRelease(timeBetweenPods));
        }
        else
        {
            //Finished shooting pods
            gunIsEmpty = true;
            amountOfShotPods = 0; 
        }
    }



    void Reload()
    {
        for (int i = 0; i < pods.Length; i++)
        {
            ReleasePod(i);
        }

        animations.Stop();
        animations.Play("PodLauncherReload");
        StartCoroutine(WaitAndFixPods(1.0f));
    }

    void ReleasePod(int i)
    {
        Vector3 f;
        if (i > 2)
        {
            //Right side
            f = pods[i].right + pods[i].up;
        }
        else
        {
            //Left side
            f = -pods[i].right + pods[i].up;
        }

        ParticleSystem.EmissionModule p = pods[i].GetComponent<ParticleSystem>().emission;
        p.enabled = true;
        Rigidbody r = pods[i].gameObject.AddComponent<Rigidbody>();
        r.AddForce(f, ForceMode.Impulse);
        r.AddTorque(Utility.RandomVector3() * 50.0f, ForceMode.Impulse);
        pods[i].gameObject.AddComponent<BoxCollider>();
    }

    void FixPods()
    {
        for (int i = 0; i < pods.Length; i++)
        {
            Destroy(pods[i].GetComponent<Rigidbody>());
            Destroy(pods[i].GetComponent<BoxCollider>());
            pods[i].localPosition = Vector3.zero;
            pods[i].localRotation = Quaternion.identity;
            pods[i].localScale = Vector3.one;

            ParticleSystem.EmissionModule p = pods[i].GetComponent<ParticleSystem>().emission;
            p.enabled = false;
        }

    }

    public void GunIsPrepared()
    {
        gunIsPrepared = true;

        animations["PodLauncherPrepare"].speed = 1.0f;
        animations["PodLauncherPrepare"].time = animations["PodLauncherPrepare"].length;
    }
}
