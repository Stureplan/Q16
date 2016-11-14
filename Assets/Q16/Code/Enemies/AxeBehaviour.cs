using UnityEngine;
using System.Collections;

public class AxeBehaviour : MonoBehaviour
{
    Collider c;
    Cooldown cd;

	void Start ()
    {
        c = GetComponent<Collider>();
        cd = new Cooldown(0.5f);
	}
	
	void Update ()
    {
        if (c.enabled)
        {
            cd.UpdateTimer();

            if (cd.ActionReady())
            {
                DeactivateAxe();
            }
        }

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Health hp = col.GetComponent<Health>();
            PlayerInput p = col.GetComponent<PlayerInput>();

            hp.Damage(15);

            Vector3 dir = transform.GetComponentInParent<GolemBehaviour>().transform.forward;
            dir.y = 0.2f;
            p.AddForce(dir, 10.0f);
        }
    }

    public void ActivateAxe()
    {
        c.enabled = true;
        cd = new Cooldown(0.3f);
    }

    void DeactivateAxe()
    {
        c.enabled = false;
    }
}
